import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { FincasServiceProxy, FincaDto  } from '@shared/service-proxies/service-proxies';
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
}
