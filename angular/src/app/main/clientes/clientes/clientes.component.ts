import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { ClientesServiceProxy, ClienteDto , Generos } from '@shared/service-proxies/service-proxies';
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
    maxFECHA_CLIENTEFilter : moment.Moment;
		minFECHA_CLIENTEFilter : moment.Moment;
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
        private _fileDownloadService: FileDownloadService
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
}
