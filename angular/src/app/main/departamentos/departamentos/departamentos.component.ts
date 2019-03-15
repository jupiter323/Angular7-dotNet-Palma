import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { DepartamentosServiceProxy, DepartamentoDto, CreateOrEditDepartamentoDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDepartamentoModalComponent } from './create-or-edit-departamento-modal.component';
import { ViewDepartamentoModalComponent } from './view-departamento-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as XLSX from 'xlsx';
import { finalize } from 'rxjs/operators';
import { FileImportService } from '@shared/utils/file-import.service';
@Component({
    templateUrl: './departamentos.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DepartamentosComponent extends AppComponentBase {

    @ViewChild('createOrEditDepartamentoModal') createOrEditDepartamentoModal: CreateOrEditDepartamentoModalComponent;
    @ViewChild('viewDepartamentoModalComponent') viewDepartamentoModal: ViewDepartamentoModalComponent;
    @ViewChild('entityTypeHistoryModal') entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    iD_DEPARTAMENTOFilter = '';
    nombrE_DEPARTAMENTOFilter = '';
    paisNOMBRE_PAISFilter = '';


    _entityTypeFullName = 'PALMASoft.Departamentos.Departamento';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _departamentosServiceProxy: DepartamentosServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _fileImportService: FileImportService,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
        console.log("here is department")
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return customSettings.EntityHistory && customSettings.EntityHistory.isEnabled && _.filter(customSettings.EntityHistory.enabledEntities, entityType => entityType === this._entityTypeFullName).length === 1;
    }

    getDepartamentos(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._departamentosServiceProxy.getAll(
            this.filterText,
            this.iD_DEPARTAMENTOFilter,
            this.nombrE_DEPARTAMENTOFilter,
            this.paisNOMBRE_PAISFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            console.log(result)
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createDepartamento(): void {
        this.createOrEditDepartamentoModal.show();
    }

    showHistory(departamento: DepartamentoDto): void {
        this.entityTypeHistoryModal.show({
            entityId: departamento.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteDepartamento(departamento: DepartamentoDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._departamentosServiceProxy.delete(departamento.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._departamentosServiceProxy.getDepartamentosToExcel(
            this.filterText,
            this.iD_DEPARTAMENTOFilter,
            this.nombrE_DEPARTAMENTOFilter,
            this.paisNOMBRE_PAISFilter,
        )
            .subscribe(result => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    //

    @ViewChild('file') file;
    initializing
    saving
    isInitAndInsert = false;
    onFileChange(evt: any) {
        /* wire up file reader */
        const target: DataTransfer = <DataTransfer>(evt.target);
        if (target.files.length !== 1) return new Error('Cannot use multiple files');
        if (this.isInitAndInsert) this.initEntityRecords();
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
            // this._paisesServiceProxy.importcsv(sendJsonData).subscribe(result => {
            //     console.log(result);
            // });
            sendJsonData.values.forEach((x, i) => {
                var depatmento: CreateOrEditDepartamentoDto = new CreateOrEditDepartamentoDto();
                depatmento.iD_DEPARTAMENTO = x[0];
                depatmento.nombrE_DEPARTAMENTO = x[1];
                depatmento.paisId = x[3];               
                this.insert(depatmento);
            });


        };
        reader.readAsBinaryString(target.files[0]);
    }
    initEntityRecords(): void {
        this.initializing = true;
        this._departamentosServiceProxy.getAll("", "", "", "", "", undefined,undefined)
            .pipe(finalize(() => { this.initializing = false; this.reloadPage() }))
            .subscribe(result => {
                result.items.forEach((x) => {
                    this._departamentosServiceProxy.delete(x.departamento.id)
                        .subscribe(() => {
                            // this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                })
            });
    }
    insert(depatmento: DepartamentoDto): void {
        this.saving = true;

        this._departamentosServiceProxy.createOrEdit(depatmento)
            .pipe(finalize(() => { this.saving = false; this.reloadPage(); }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
            });
    }

    addFiles() {
        this.file.nativeElement.click();
    }
}
