import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { AnalisesServiceProxy, AnalisisDto , AnalisisTipo } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAnalisisModalComponent } from './create-or-edit-analisis-modal.component';
import { ViewAnalisisModalComponent } from './view-analisis-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { ViewAnalisisDetailModalComponent } from './view-analisis-detail-modal.component';

@Component({
    templateUrl: './analises.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class AnalisesComponent extends AppComponentBase {

    @ViewChild('createOrEditAnalisisModal') createOrEditAnalisisModal: CreateOrEditAnalisisModalComponent;
    @ViewChild('viewAnalisisModal') viewAnalisisModal: ViewAnalisisModalComponent;
    @ViewChild('viewAnalisisDetailModal') viewAnalisisDetailModal: ViewAnalisisDetailModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    iD_INFORMEFilter = '';
    tipO_INFORMEFilter = -1;
    maxFECHA_MUESTREOFilter : moment.Moment;
		minFECHA_MUESTREOFilter : moment.Moment;
    maxFECHA_REGISTROFilter : moment.Moment;
		minFECHA_REGISTROFilter : moment.Moment;
    maxFECHA_ENTREGAFilter : moment.Moment;
		minFECHA_ENTREGAFilter : moment.Moment;
        fincaNOMBRE_FINCAFilter = '';

    analisisTipo = AnalisisTipo;



    constructor(
        injector: Injector,
        private _analisesServiceProxy: AnalisesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getAnalises(event?: LazyLoadEvent) {       
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._analisesServiceProxy.getAll(
            this.filterText,
            this.iD_INFORMEFilter,
            this.tipO_INFORMEFilter,
            this.maxFECHA_MUESTREOFilter,
            this.minFECHA_MUESTREOFilter,
            this.maxFECHA_REGISTROFilter,
            this.minFECHA_REGISTROFilter,
            this.maxFECHA_ENTREGAFilter,
            this.minFECHA_ENTREGAFilter,
            this.fincaNOMBRE_FINCAFilter,
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

    createAnalisis(): void {
        this.createOrEditAnalisisModal.show();
    }

    deleteAnalisis(analisis: AnalisisDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._analisesServiceProxy.delete(analisis.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._analisesServiceProxy.getAnalisesToExcel(
        this.filterText,
            this.iD_INFORMEFilter,
            this.tipO_INFORMEFilter,
            this.maxFECHA_MUESTREOFilter,
            this.minFECHA_MUESTREOFilter,
            this.maxFECHA_REGISTROFilter,
            this.minFECHA_REGISTROFilter,
            this.maxFECHA_ENTREGAFilter,
            this.minFECHA_ENTREGAFilter,
            this.fincaNOMBRE_FINCAFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
