import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { ClientesServiceProxy, ClienteDto, Generos, CreateOrEditClienteDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditClienteModalComponent } from './create-or-edit-cliente-modal.component';
import { ViewClienteModalComponent } from './view-cliente-modal.component';
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
    templateUrl: './clientes.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ClientesComponent extends AppComponentBase {

    @ViewChild('createOrEditClienteModal') createOrEditClienteModal: CreateOrEditClienteModalComponent;
    @ViewChild('viewClienteModalComponent') viewClienteModal: ViewClienteModalComponent;
    @ViewChild('entityTypeHistoryModal') entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    iD_CLIENTEFilter = '';
    nombrE_CLIENTEFilter = '';
    apellidO_CLIENTEFilter = '';
    generO_CLIENTEFilter = -1;
    maxFECHA_CLIENTEFilter: moment.Moment;
    minFECHA_CLIENTEFilter: moment.Moment;
    celulaR_CLIENTEFilter = '';
    direccioN_CLIENTEFilter = '';
    departamentO_CLIENTEFilter = '';
    municipiO_CLIENTEFilter = '';
    empresA_CLIENTEFilter = '';
    profesioN_CLIENTEFilter = '';

    generos = Generos;

    _entityTypeFullName = 'PALMASoft.Clientes.Cliente';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _clientesServiceProxy: ClientesServiceProxy,
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

    getClientes(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._clientesServiceProxy.getAll(
            this.filterText,
            this.iD_CLIENTEFilter,
            this.nombrE_CLIENTEFilter,
            this.apellidO_CLIENTEFilter,
            this.generO_CLIENTEFilter,
            this.maxFECHA_CLIENTEFilter,
            this.minFECHA_CLIENTEFilter,
            this.celulaR_CLIENTEFilter,
            this.direccioN_CLIENTEFilter,
            this.departamentO_CLIENTEFilter,
            this.municipiO_CLIENTEFilter,
            this.empresA_CLIENTEFilter,
            this.profesioN_CLIENTEFilter,
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

    createCliente(): void {
        this.createOrEditClienteModal.show();
    }

    showHistory(cliente: ClienteDto): void {
        this.entityTypeHistoryModal.show({
            entityId: cliente.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteCliente(cliente: ClienteDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._clientesServiceProxy.delete(cliente.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._clientesServiceProxy.getClientesToExcel(
            this.filterText,
            this.iD_CLIENTEFilter,
            this.nombrE_CLIENTEFilter,
            this.apellidO_CLIENTEFilter,
            this.generO_CLIENTEFilter,
            this.maxFECHA_CLIENTEFilter,
            this.minFECHA_CLIENTEFilter,
            this.celulaR_CLIENTEFilter,
            this.direccioN_CLIENTEFilter,
            this.departamentO_CLIENTEFilter,
            this.municipiO_CLIENTEFilter,
            this.empresA_CLIENTEFilter,
            this.profesioN_CLIENTEFilter,
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
                var cliente: CreateOrEditClienteDto = new CreateOrEditClienteDto();
                cliente.iD_CLIENTE = x[0];
                cliente.nombrE_CLIENTE = x[1];
                cliente.apellidO_CLIENTE = x[2];
                cliente.generO_CLIENTE = x[3];
                cliente.fechA_CLIENTE = this._fileImportService.excelDateValueToMoment(x[4]);
                console.log("date   :", this._fileImportService.excelDateValueToMoment(x[4]), "   excel data  :",this._fileImportService.parseDateExcel(x[4]),this._fileImportService.excelDateValueToDate(x[4]))
                cliente.celulaR_CLIENTE = x[5];
                cliente.direccioN_CLIENTE = x[6];
                cliente.departamentO_CLIENTE = x[7];
                cliente.municipiO_CLIENTE = x[8];
                cliente.empresA_CLIENTE = x[9];
                cliente.profesioN_CLIENTE = x[10];
                this.insert(cliente);
            });


        };
        reader.readAsBinaryString(target.files[0]);
    }
    //   (filter: string | null | undefined, iD_CLIENTEFilter: string | null | undefined, nOMBRE_CLIENTEFilter: string | null | undefined, aPELLIDO_CLIENTEFilter: string | null | undefined, gENERO_CLIENTEFilter: number | null | undefined, maxFECHA_CLIENTEFilter: moment.Moment | null | undefined, minFECHA_CLIENTEFilter: moment.Moment | null | undefined, cELULAR_CLIENTEFilter: string | null | undefined, dIRECCION_CLIENTEFilter: string | null | undefined, dEPARTAMENTO_CLIENTEFilter: string | null | undefined, mUNICIPIO_CLIENTEFilter: string | null | undefined, eMPRESA_CLIENTEFilter: string | null | undefined, pROFESION_CLIENTEFilter: string | null | undefined, sorting: string | null | undefined, skipCount: number | null | undefined, maxResultCount: number | null | undefined)
    initEntityRecords(): void {
        this.initializing = true;
        //   this._clientesServiceProxy.getAll("", "", "", "", "", 0, 10000)
        //       .pipe(finalize(() => { this.initializing = false; this.reloadPage() }))
        //       .subscribe(result => {
        //           result.items.forEach((x) => {
        //               this._clientesServiceProxy.delete(x.cliente.id)
        //                   .subscribe(() => {
        //                       // this.notify.success(this.l('SuccessfullyDeleted'));
        //                   });
        //           })
        //       });
    }
    insert(cliente: ClienteDto): void {
        this.saving = true;

        this._clientesServiceProxy.createOrEdit(cliente)
            .pipe(finalize(() => { this.saving = false; this.reloadPage(); }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
            });
    }

    addFiles() {
        this.file.nativeElement.click();
    }
}
