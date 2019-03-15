import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { PaisesServiceProxy, PaisDto, CreateOrEditPaisDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPaisModalComponent } from './create-or-edit-pais-modal.component';
import { ViewPaisModalComponent } from './view-pais-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import { FileImportService } from '@shared/utils/file-import.service';
import * as XLSX from 'xlsx';
import { finalize } from 'rxjs/operators';

@Component({
    templateUrl: './paises.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PaisesComponent extends AppComponentBase {

    @ViewChild('createOrEditPaisModal') createOrEditPaisModal: CreateOrEditPaisModalComponent;
    @ViewChild('viewPaisModalComponent') viewPaisModal: ViewPaisModalComponent;
    @ViewChild('entityTypeHistoryModal') entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('file') file;

    advancedFiltersAreShown = false;
    filterText = '';
    iD_PAISFilter = '';
    nombrE_PAISFilter = '';
    saving
    getting
    initializing
    _entityTypeFullName = 'PALMASoft.Paises.Pais';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _paisesServiceProxy: PaisesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _fileImportService: FileImportService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return customSettings.EntityHistory && customSettings.EntityHistory.isEnabled && _.filter(customSettings.EntityHistory.enabledEntities, entityType => entityType === this._entityTypeFullName).length === 1;
    }

    getPaises(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._paisesServiceProxy.getAll(
            this.filterText,
            this.iD_PAISFilter,
            this.nombrE_PAISFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createPais(): void {
        this.createOrEditPaisModal.show();
    }

    showHistory(pais: PaisDto): void {
        this.entityTypeHistoryModal.show({
            entityId: pais.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deletePais(pais: PaisDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._paisesServiceProxy.delete(pais.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._paisesServiceProxy.getPaisesToExcel(
            this.filterText,
            this.iD_PAISFilter,
            this.nombrE_PAISFilter,
        ).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }
    isInitAndInsert = false;
    onFileChange(evt: any) {
        /* wire up file reader */
        const target: DataTransfer = <DataTransfer>(evt.target);
        if (target.files.length !== 1) return new Error('Cannot use multiple files');
        if(this.isInitAndInsert) this.initPasis();
        const reader: FileReader = new FileReader();
        reader.onload = (e: any) => {
            /* read workbook */
            const bstr: string = e.target.result;
            const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

            /* grab first sheet */
            const wsname: string = wb.SheetNames[0];
            const ws: XLSX.WorkSheet = wb.Sheets[wsname];

            /* save data */
            let data = (XLSX.utils.sheet_to_json(ws, { header: 1 }));
            let sendJsonData = this._fileImportService.parseJsonFromArray(data);
            this._paisesServiceProxy.importcsv(sendJsonData).subscribe(result => {
                console.log(result);
            });
            sendJsonData.values.forEach((x, i) => {
                var pais: CreateOrEditPaisDto = new CreateOrEditPaisDto();
                pais.iD_PAIS = x[0];
                pais.nombrE_PAIS = x[1];
                this.insert(pais);
            });


        };
        reader.readAsBinaryString(target.files[0]);
    }
    initPasis(): void {
        this.initializing = true;
        this._paisesServiceProxy.getAll("", "", "", "",undefined,undefined)
            .pipe(finalize(() => { this.initializing = false; this.reloadPage() }))
            .subscribe(result => {
                result.items.forEach((x) => {
                    this._paisesServiceProxy.delete(x.pais.id)
                        .subscribe(() => {
                            // this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                })
            });
    }
    insert(pais: CreateOrEditPaisDto): void {
        this.saving = true;

        this._paisesServiceProxy.createOrEdit(pais)
            .pipe(finalize(() => { this.saving = false; this.reloadPage(); }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
            });
    }

    addFiles() {
        this.file.nativeElement.click();
    }
}
