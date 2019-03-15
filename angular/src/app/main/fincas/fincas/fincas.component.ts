import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { FincasServiceProxy, FincaDto, CreateOrEditFincaDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditFincaModalComponent } from './create-or-edit-finca-modal.component';
import { ViewFincaModalComponent } from './view-finca-modal.component';
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
    templateUrl: './fincas.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class FincasComponent extends AppComponentBase {

    @ViewChild('createOrEditFincaModal') createOrEditFincaModal: CreateOrEditFincaModalComponent;
    @ViewChild('viewFincaModalComponent') viewFincaModal: ViewFincaModalComponent;
    @ViewChild('entityTypeHistoryModal') entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    iD_FINCAFilter = '';
    nombrE_FINCAFilter = '';
    departamentO_FINCAFilter = '';
    municipiO_FINCAFilter = '';
    veredA_FINCAFilter = '';
    corregimientO_FINCAFilter = '';
    ubicacioN_FINCAFilter = '';
    longituD_FINCAFilter = '';
    latituD_FINCAFilter = '';
    contactO_FINCAFilter = '';
    telefonO_FINCAFilter = '';
    correO_FINCAFilter = '';
    clienteNOMBRE_CLIENTEFilter = '';


    _entityTypeFullName = 'PALMASoft.Fincas.Finca';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _fincasServiceProxy: FincasServiceProxy,
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
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return customSettings.EntityHistory && customSettings.EntityHistory.isEnabled && _.filter(customSettings.EntityHistory.enabledEntities, entityType => entityType === this._entityTypeFullName).length === 1;
    }

    getFincas(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._fincasServiceProxy.getAll(
            this.filterText,
            this.iD_FINCAFilter,
            this.nombrE_FINCAFilter,
            this.departamentO_FINCAFilter,
            this.municipiO_FINCAFilter,
            this.veredA_FINCAFilter,
            this.corregimientO_FINCAFilter,
            this.ubicacioN_FINCAFilter,
            this.longituD_FINCAFilter,
            this.latituD_FINCAFilter,
            this.contactO_FINCAFilter,
            this.telefonO_FINCAFilter,
            this.correO_FINCAFilter,
            this.clienteNOMBRE_CLIENTEFilter,
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

    createFinca(): void {
        this.createOrEditFincaModal.show();
    }

    showHistory(finca: FincaDto): void {
        this.entityTypeHistoryModal.show({
            entityId: finca.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteFinca(finca: FincaDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._fincasServiceProxy.delete(finca.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._fincasServiceProxy.getFincasToExcel(
            this.filterText,
            this.iD_FINCAFilter,
            this.nombrE_FINCAFilter,
            this.departamentO_FINCAFilter,
            this.municipiO_FINCAFilter,
            this.veredA_FINCAFilter,
            this.corregimientO_FINCAFilter,
            this.ubicacioN_FINCAFilter,
            this.longituD_FINCAFilter,
            this.latituD_FINCAFilter,
            this.contactO_FINCAFilter,
            this.telefonO_FINCAFilter,
            this.correO_FINCAFilter,
            this.clienteNOMBRE_CLIENTEFilter,
        )
            .subscribe(result => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
    // //

    @ViewChild('file') file;
    initializing
    saving
    isInitAndInsert = false;
    getting
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
            console.log(sendJsonData.values);
            // this._paisesServiceProxy.importcsv(sendJsonData).subscribe(result => {
            //     console.log(result);
            // });


            this._fincasServiceProxy.getAll(undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined)
                .pipe(finalize(() => { this.initializing = false; this.reloadPage() }))
                .subscribe(result => {
                    console.log("fincas :",result)

                    let resultMap = result.items.map((x) => {
                        return [x.finca.iD_FINCA]
                    });
                    let sendValues = this._fileImportService.duplicatedMRObjectArray([...resultMap, ...sendJsonData.values], 0);
                    console.log("send values :", sendValues)
                    sendValues = sendValues.slice(resultMap.length, sendValues.length);
                    console.log("send values :", sendValues, [...resultMap, ...sendJsonData.values])

                    sendValues.forEach((x, i) => {//insert new list
                        var finca: CreateOrEditFincaDto = new CreateOrEditFincaDto();
                        finca.iD_FINCA = x[0];
                        finca.nombrE_FINCA = x[1];
                        finca.departamentO_FINCA = x[2];
                        finca.municipiO_FINCA = x[3];
                        finca.veredA_FINCA = x[4];
                        finca.corregimientO_FINCA = x[5];
                        finca.ubicacioN_FINCA = x[6];
                        finca.longituD_FINCA = x[7];
                        finca.latituD_FINCA = x[8];
                        finca.contactO_FINCA = x[9];
                        finca.telefonO_FINCA = x[10];
                        finca.correO_FINCA = x[11];
                        finca.clienteId = x[13];
                        console.log(finca)
                        this.insert(finca);
                    });
                });




        };
        reader.readAsBinaryString(target.files[0]);
    }
    initEntityRecords(): void {
        this.initializing = true;
        this._fincasServiceProxy.getAll(undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined)
            .pipe(finalize(() => { this.initializing = false; this.reloadPage() }))
            .subscribe(result => {
                result.items.forEach((x) => {
                    this._fincasServiceProxy.delete(x.finca.id)
                        .subscribe(() => {
                            // this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                })
            });
    }

    insert(finca: FincaDto): void {
        this.saving = true;

        this._fincasServiceProxy.createOrEdit(finca)
            .pipe(finalize(() => { this.saving = false; this.reloadPage(); }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
            });
    }

    addFiles() {
        this.file.nativeElement.click();
    }
}
