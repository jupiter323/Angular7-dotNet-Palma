import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { MunicipiosServiceProxy, MunicipioDto, CreateOrEditMunicipioDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditMunicipioModalComponent } from './create-or-edit-municipio-modal.component';
import { ViewMunicipioModalComponent } from './view-municipio-modal.component';
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
    templateUrl: './municipios.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class MunicipiosComponent extends AppComponentBase {

    @ViewChild('createOrEditMunicipioModal') createOrEditMunicipioModal: CreateOrEditMunicipioModalComponent;
    @ViewChild('viewMunicipioModalComponent') viewMunicipioModal: ViewMunicipioModalComponent;
    @ViewChild('entityTypeHistoryModal') entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    iD_MUNICIPIOFilter = '';
    nombrE_MUNICIPIOFilter = '';
        departamentoNOMBRE_DEPARTAMENTOFilter = '';


    _entityTypeFullName = 'PALMASoft.Municipios.Municipio';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _municipiosServiceProxy: MunicipiosServiceProxy,
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

    getMunicipios(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._municipiosServiceProxy.getAll(
            this.filterText,
            this.iD_MUNICIPIOFilter,
            this.nombrE_MUNICIPIOFilter,
            this.departamentoNOMBRE_DEPARTAMENTOFilter,
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

    createMunicipio(): void {
        this.createOrEditMunicipioModal.show();
    }

    showHistory(municipio: MunicipioDto): void {
        this.entityTypeHistoryModal.show({
            entityId: municipio.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteMunicipio(municipio: MunicipioDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._municipiosServiceProxy.delete(municipio.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._municipiosServiceProxy.getMunicipiosToExcel(
        this.filterText,
            this.iD_MUNICIPIOFilter,
            this.nombrE_MUNICIPIOFilter,
            this.departamentoNOMBRE_DEPARTAMENTOFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
    //
     //

     @ViewChild('file') file;
     initializing
     saving
     isInitAndInsert = false;
     onFileChange(evt: any) {
         /* wire up file reader */
         const target: DataTransfer = <DataTransfer>(evt.target);
         if (target.files.length !== 1) throw new Error('Cannot use multiple files');
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
                 var municipio: CreateOrEditMunicipioDto = new CreateOrEditMunicipioDto();
                 municipio.iD_MUNICIPIO = x[0];
                 municipio.nombrE_MUNICIPIO = x[1];
                 municipio.departamentoId = x[3];               
                 this.insert(municipio);
             });
 
 
         };
         reader.readAsBinaryString(target.files[0]);
     }
     initEntityRecords(): void {
         this.initializing = true;
         this._municipiosServiceProxy.getAll("", "", "", "", "", 0, 10000)
             .pipe(finalize(() => { this.initializing = false; this.reloadPage() }))
             .subscribe(result => {
                 result.items.forEach((x) => {
                     this._municipiosServiceProxy.delete(x.municipio.id)
                         .subscribe(() => {
                             // this.notify.success(this.l('SuccessfullyDeleted'));
                         });
                 })
             });
     }
     insert(municipio: MunicipioDto): void {
         this.saving = true;
 
         this._municipiosServiceProxy.createOrEdit(municipio)
             .pipe(finalize(() => { this.saving = false; this.reloadPage(); }))
             .subscribe(() => {
                 this.notify.info(this.l('SavedSuccessfully'));
             });
     }
 
     addFiles() {
         this.file.nativeElement.click();
     }
}
