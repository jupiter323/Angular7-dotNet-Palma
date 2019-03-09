import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { DepartamentosServiceProxy, DepartamentoDto  } from '@shared/service-proxies/service-proxies';
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
}
