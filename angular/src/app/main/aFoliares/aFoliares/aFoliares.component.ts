import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { AFoliaresServiceProxy, AFoliarDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAFoliarModalComponent } from './create-or-edit-aFoliar-modal.component';
import { ViewAFoliarModalComponent } from './view-aFoliar-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './aFoliares.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class AFoliaresComponent extends AppComponentBase {

    @ViewChild('createOrEditAFoliarModal') createOrEditAFoliarModal: CreateOrEditAFoliarModalComponent;
    @ViewChild('viewAFoliarModalComponent') viewAFoliarModal: ViewAFoliarModalComponent;
    @ViewChild('entityTypeHistoryModal') entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    coD_FOLIARFilter = '';
    iD_FOLIARFilter = '';
    materiaL_FOLIARFilter = '';
    maxNUM_HOJA_FOLIARFilter : number;
		maxNUM_HOJA_FOLIARFilterEmpty : number;
		minNUM_HOJA_FOLIARFilter : number;
		minNUM_HOJA_FOLIARFilterEmpty : number;
    maxNITROGENOFilter : number;
		maxNITROGENOFilterEmpty : number;
		minNITROGENOFilter : number;
		minNITROGENOFilterEmpty : number;
    maxFOSFOROFilter : number;
		maxFOSFOROFilterEmpty : number;
		minFOSFOROFilter : number;
		minFOSFOROFilterEmpty : number;
    maxPOTASIOFilter : number;
		maxPOTASIOFilterEmpty : number;
		minPOTASIOFilter : number;
		minPOTASIOFilterEmpty : number;
    maxCALCIOFilter : number;
		maxCALCIOFilterEmpty : number;
		minCALCIOFilter : number;
		minCALCIOFilterEmpty : number;
    maxMAGNESIOFilter : number;
		maxMAGNESIOFilterEmpty : number;
		minMAGNESIOFilter : number;
		minMAGNESIOFilterEmpty : number;
    maxCLOROFilter : number;
		maxCLOROFilterEmpty : number;
		minCLOROFilter : number;
		minCLOROFilterEmpty : number;
    maxAZUFREFilter : number;
		maxAZUFREFilterEmpty : number;
		minAZUFREFilter : number;
		minAZUFREFilterEmpty : number;
    maxBOROFilter : number;
		maxBOROFilterEmpty : number;
		minBOROFilter : number;
		minBOROFilterEmpty : number;
    maxHIERROFilter : number;
		maxHIERROFilterEmpty : number;
		minHIERROFilter : number;
		minHIERROFilterEmpty : number;
    maxCOBREFilter : number;
		maxCOBREFilterEmpty : number;
		minCOBREFilter : number;
		minCOBREFilterEmpty : number;
    maxMANGANESOFilter : number;
		maxMANGANESOFilterEmpty : number;
		minMANGANESOFilter : number;
		minMANGANESOFilterEmpty : number;
    maxZINCFilter : number;
		maxZINCFilterEmpty : number;
		minZINCFilter : number;
		minZINCFilterEmpty : number;
    maxCa_Mg_KFilter : number;
		maxCa_Mg_KFilterEmpty : number;
		minCa_Mg_KFilter : number;
		minCa_Mg_KFilterEmpty : number;
    maxCa_Mg_div_KFilter : number;
		maxCa_Mg_div_KFilterEmpty : number;
		minCa_Mg_div_KFilter : number;
		minCa_Mg_div_KFilterEmpty : number;
    maxN_div_KFilter : number;
		maxN_div_KFilterEmpty : number;
		minN_div_KFilter : number;
		minN_div_KFilterEmpty : number;
    maxN_div_PFilter : number;
		maxN_div_PFilterEmpty : number;
		minN_div_PFilter : number;
		minN_div_PFilterEmpty : number;
    maxK_div_PFilter : number;
		maxK_div_PFilterEmpty : number;
		minK_div_PFilter : number;
		minK_div_PFilterEmpty : number;
    maxCa_div_BFilter : number;
		maxCa_div_BFilterEmpty : number;
		minCa_div_BFilter : number;
		minCa_div_BFilterEmpty : number;
    analisiS_IDFilter = '';


    _entityTypeFullName = 'PALMASoft.AFoliares.AFoliar';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _aFoliaresServiceProxy: AFoliaresServiceProxy,
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

    getAFoliares(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._aFoliaresServiceProxy.getAll(
            this.filterText,
            this.coD_FOLIARFilter,
            this.iD_FOLIARFilter,
            this.materiaL_FOLIARFilter,
            this.maxNUM_HOJA_FOLIARFilter == null ? this.maxNUM_HOJA_FOLIARFilterEmpty: this.maxNUM_HOJA_FOLIARFilter,
            this.minNUM_HOJA_FOLIARFilter == null ? this.minNUM_HOJA_FOLIARFilterEmpty: this.minNUM_HOJA_FOLIARFilter,
            this.maxNITROGENOFilter == null ? this.maxNITROGENOFilterEmpty: this.maxNITROGENOFilter,
            this.minNITROGENOFilter == null ? this.minNITROGENOFilterEmpty: this.minNITROGENOFilter,
            this.maxFOSFOROFilter == null ? this.maxFOSFOROFilterEmpty: this.maxFOSFOROFilter,
            this.minFOSFOROFilter == null ? this.minFOSFOROFilterEmpty: this.minFOSFOROFilter,
            this.maxPOTASIOFilter == null ? this.maxPOTASIOFilterEmpty: this.maxPOTASIOFilter,
            this.minPOTASIOFilter == null ? this.minPOTASIOFilterEmpty: this.minPOTASIOFilter,
            this.maxCALCIOFilter == null ? this.maxCALCIOFilterEmpty: this.maxCALCIOFilter,
            this.minCALCIOFilter == null ? this.minCALCIOFilterEmpty: this.minCALCIOFilter,
            this.maxMAGNESIOFilter == null ? this.maxMAGNESIOFilterEmpty: this.maxMAGNESIOFilter,
            this.minMAGNESIOFilter == null ? this.minMAGNESIOFilterEmpty: this.minMAGNESIOFilter,
            this.maxCLOROFilter == null ? this.maxCLOROFilterEmpty: this.maxCLOROFilter,
            this.minCLOROFilter == null ? this.minCLOROFilterEmpty: this.minCLOROFilter,
            this.maxAZUFREFilter == null ? this.maxAZUFREFilterEmpty: this.maxAZUFREFilter,
            this.minAZUFREFilter == null ? this.minAZUFREFilterEmpty: this.minAZUFREFilter,
            this.maxBOROFilter == null ? this.maxBOROFilterEmpty: this.maxBOROFilter,
            this.minBOROFilter == null ? this.minBOROFilterEmpty: this.minBOROFilter,
            this.maxHIERROFilter == null ? this.maxHIERROFilterEmpty: this.maxHIERROFilter,
            this.minHIERROFilter == null ? this.minHIERROFilterEmpty: this.minHIERROFilter,
            this.maxCOBREFilter == null ? this.maxCOBREFilterEmpty: this.maxCOBREFilter,
            this.minCOBREFilter == null ? this.minCOBREFilterEmpty: this.minCOBREFilter,
            this.maxMANGANESOFilter == null ? this.maxMANGANESOFilterEmpty: this.maxMANGANESOFilter,
            this.minMANGANESOFilter == null ? this.minMANGANESOFilterEmpty: this.minMANGANESOFilter,
            this.maxZINCFilter == null ? this.maxZINCFilterEmpty: this.maxZINCFilter,
            this.minZINCFilter == null ? this.minZINCFilterEmpty: this.minZINCFilter,
            this.maxCa_Mg_KFilter == null ? this.maxCa_Mg_KFilterEmpty: this.maxCa_Mg_KFilter,
            this.minCa_Mg_KFilter == null ? this.minCa_Mg_KFilterEmpty: this.minCa_Mg_KFilter,
            this.maxCa_Mg_div_KFilter == null ? this.maxCa_Mg_div_KFilterEmpty: this.maxCa_Mg_div_KFilter,
            this.minCa_Mg_div_KFilter == null ? this.minCa_Mg_div_KFilterEmpty: this.minCa_Mg_div_KFilter,
            this.maxN_div_KFilter == null ? this.maxN_div_KFilterEmpty: this.maxN_div_KFilter,
            this.minN_div_KFilter == null ? this.minN_div_KFilterEmpty: this.minN_div_KFilter,
            this.maxN_div_PFilter == null ? this.maxN_div_PFilterEmpty: this.maxN_div_PFilter,
            this.minN_div_PFilter == null ? this.minN_div_PFilterEmpty: this.minN_div_PFilter,
            this.maxK_div_PFilter == null ? this.maxK_div_PFilterEmpty: this.maxK_div_PFilter,
            this.minK_div_PFilter == null ? this.minK_div_PFilterEmpty: this.minK_div_PFilter,
            this.maxCa_div_BFilter == null ? this.maxCa_div_BFilterEmpty: this.maxCa_div_BFilter,
            this.minCa_div_BFilter == null ? this.minCa_div_BFilterEmpty: this.minCa_div_BFilter,
            this.analisiS_IDFilter,
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

    createAFoliar(): void {
        this.createOrEditAFoliarModal.show();
    }

    showHistory(aFoliar: AFoliarDto): void {
        this.entityTypeHistoryModal.show({
            entityId: aFoliar.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteAFoliar(aFoliar: AFoliarDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._aFoliaresServiceProxy.delete(aFoliar.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._aFoliaresServiceProxy.getAFoliaresToExcel(
        this.filterText,
            this.coD_FOLIARFilter,
            this.iD_FOLIARFilter,
            this.materiaL_FOLIARFilter,
            this.maxNUM_HOJA_FOLIARFilter == null ? this.maxNUM_HOJA_FOLIARFilterEmpty: this.maxNUM_HOJA_FOLIARFilter,
            this.minNUM_HOJA_FOLIARFilter == null ? this.minNUM_HOJA_FOLIARFilterEmpty: this.minNUM_HOJA_FOLIARFilter,
            this.maxNITROGENOFilter == null ? this.maxNITROGENOFilterEmpty: this.maxNITROGENOFilter,
            this.minNITROGENOFilter == null ? this.minNITROGENOFilterEmpty: this.minNITROGENOFilter,
            this.maxFOSFOROFilter == null ? this.maxFOSFOROFilterEmpty: this.maxFOSFOROFilter,
            this.minFOSFOROFilter == null ? this.minFOSFOROFilterEmpty: this.minFOSFOROFilter,
            this.maxPOTASIOFilter == null ? this.maxPOTASIOFilterEmpty: this.maxPOTASIOFilter,
            this.minPOTASIOFilter == null ? this.minPOTASIOFilterEmpty: this.minPOTASIOFilter,
            this.maxCALCIOFilter == null ? this.maxCALCIOFilterEmpty: this.maxCALCIOFilter,
            this.minCALCIOFilter == null ? this.minCALCIOFilterEmpty: this.minCALCIOFilter,
            this.maxMAGNESIOFilter == null ? this.maxMAGNESIOFilterEmpty: this.maxMAGNESIOFilter,
            this.minMAGNESIOFilter == null ? this.minMAGNESIOFilterEmpty: this.minMAGNESIOFilter,
            this.maxCLOROFilter == null ? this.maxCLOROFilterEmpty: this.maxCLOROFilter,
            this.minCLOROFilter == null ? this.minCLOROFilterEmpty: this.minCLOROFilter,
            this.maxAZUFREFilter == null ? this.maxAZUFREFilterEmpty: this.maxAZUFREFilter,
            this.minAZUFREFilter == null ? this.minAZUFREFilterEmpty: this.minAZUFREFilter,
            this.maxBOROFilter == null ? this.maxBOROFilterEmpty: this.maxBOROFilter,
            this.minBOROFilter == null ? this.minBOROFilterEmpty: this.minBOROFilter,
            this.maxHIERROFilter == null ? this.maxHIERROFilterEmpty: this.maxHIERROFilter,
            this.minHIERROFilter == null ? this.minHIERROFilterEmpty: this.minHIERROFilter,
            this.maxCOBREFilter == null ? this.maxCOBREFilterEmpty: this.maxCOBREFilter,
            this.minCOBREFilter == null ? this.minCOBREFilterEmpty: this.minCOBREFilter,
            this.maxMANGANESOFilter == null ? this.maxMANGANESOFilterEmpty: this.maxMANGANESOFilter,
            this.minMANGANESOFilter == null ? this.minMANGANESOFilterEmpty: this.minMANGANESOFilter,
            this.maxZINCFilter == null ? this.maxZINCFilterEmpty: this.maxZINCFilter,
            this.minZINCFilter == null ? this.minZINCFilterEmpty: this.minZINCFilter,
            this.maxCa_Mg_KFilter == null ? this.maxCa_Mg_KFilterEmpty: this.maxCa_Mg_KFilter,
            this.minCa_Mg_KFilter == null ? this.minCa_Mg_KFilterEmpty: this.minCa_Mg_KFilter,
            this.maxCa_Mg_div_KFilter == null ? this.maxCa_Mg_div_KFilterEmpty: this.maxCa_Mg_div_KFilter,
            this.minCa_Mg_div_KFilter == null ? this.minCa_Mg_div_KFilterEmpty: this.minCa_Mg_div_KFilter,
            this.maxN_div_KFilter == null ? this.maxN_div_KFilterEmpty: this.maxN_div_KFilter,
            this.minN_div_KFilter == null ? this.minN_div_KFilterEmpty: this.minN_div_KFilter,
            this.maxN_div_PFilter == null ? this.maxN_div_PFilterEmpty: this.maxN_div_PFilter,
            this.minN_div_PFilter == null ? this.minN_div_PFilterEmpty: this.minN_div_PFilter,
            this.maxK_div_PFilter == null ? this.maxK_div_PFilterEmpty: this.maxK_div_PFilter,
            this.minK_div_PFilter == null ? this.minK_div_PFilterEmpty: this.minK_div_PFilter,
            this.maxCa_div_BFilter == null ? this.maxCa_div_BFilterEmpty: this.maxCa_div_BFilter,
            this.minCa_div_BFilter == null ? this.minCa_div_BFilterEmpty: this.minCa_div_BFilter,
            this.analisiS_IDFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
