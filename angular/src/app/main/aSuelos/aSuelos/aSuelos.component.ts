import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { ASuelosServiceProxy, ASueloDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditASueloModalComponent } from './create-or-edit-aSuelo-modal.component';
import { ViewASueloModalComponent } from './view-aSuelo-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './aSuelos.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ASuelosComponent extends AppComponentBase {

    @ViewChild('createOrEditASueloModal') createOrEditASueloModal: CreateOrEditASueloModalComponent;
    @ViewChild('viewASueloModalComponent') viewASueloModal: ViewASueloModalComponent;
    @ViewChild('entityTypeHistoryModal') entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    coD_SUELOSFilter = '';
    iD_SUELOSFilter = '';
    materiaL_SUELOSFilter = '';
    maxPROFUNDIDAD_MUESTRAFilter : number;
		maxPROFUNDIDAD_MUESTRAFilterEmpty : number;
		minPROFUNDIDAD_MUESTRAFilter : number;
		minPROFUNDIDAD_MUESTRAFilterEmpty : number;
    texturA_SUELOSFilter = '';
    maxARENAFilter : number;
		maxARENAFilterEmpty : number;
		minARENAFilter : number;
		minARENAFilterEmpty : number;
    maxLIMOFilter : number;
		maxLIMOFilterEmpty : number;
		minLIMOFilter : number;
		minLIMOFilterEmpty : number;
    maxARCILLAFilter : number;
		maxARCILLAFilterEmpty : number;
		minARCILLAFilter : number;
		minARCILLAFilterEmpty : number;
    maxPHFilter : number;
		maxPHFilterEmpty : number;
		minPHFilter : number;
		minPHFilterEmpty : number;
    maxCARBONO_ORGANICOFilter : number;
		maxCARBONO_ORGANICOFilterEmpty : number;
		minCARBONO_ORGANICOFilter : number;
		minCARBONO_ORGANICOFilterEmpty : number;
    maxMATERIA_ORGANICAFilter : number;
		maxMATERIA_ORGANICAFilterEmpty : number;
		minMATERIA_ORGANICAFilter : number;
		minMATERIA_ORGANICAFilterEmpty : number;
    maxFOSFOROFilter : number;
		maxFOSFOROFilterEmpty : number;
		minFOSFOROFilter : number;
		minFOSFOROFilterEmpty : number;
    maxAZUFREFilter : number;
		maxAZUFREFilterEmpty : number;
		minAZUFREFilter : number;
		minAZUFREFilterEmpty : number;
    maxACIDEZFilter : number;
		maxACIDEZFilterEmpty : number;
		minACIDEZFilter : number;
		minACIDEZFilterEmpty : number;
    maxALUMINIOFilter : number;
		maxALUMINIOFilterEmpty : number;
		minALUMINIOFilter : number;
		minALUMINIOFilterEmpty : number;
    maxCALCIOFilter : number;
		maxCALCIOFilterEmpty : number;
		minCALCIOFilter : number;
		minCALCIOFilterEmpty : number;
    maxMAGNESIOFilter : number;
		maxMAGNESIOFilterEmpty : number;
		minMAGNESIOFilter : number;
		minMAGNESIOFilterEmpty : number;
    maxPOTASIOFilter : number;
		maxPOTASIOFilterEmpty : number;
		minPOTASIOFilter : number;
		minPOTASIOFilterEmpty : number;
    maxSODIOFilter : number;
		maxSODIOFilterEmpty : number;
		minSODIOFilter : number;
		minSODIOFilterEmpty : number;
    maxCATIONICOFilter : number;
		maxCATIONICOFilterEmpty : number;
		minCATIONICOFilter : number;
		minCATIONICOFilterEmpty : number;
    maxELECTRICAFilter : number;
		maxELECTRICAFilterEmpty : number;
		minELECTRICAFilter : number;
		minELECTRICAFilterEmpty : number;
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
    maxCICEFilter : number;
		maxCICEFilterEmpty : number;
		minCICEFilter : number;
		minCICEFilterEmpty : number;
    maxSUMA_BASESFilter : number;
		maxSUMA_BASESFilterEmpty : number;
		minSUMA_BASESFilter : number;
		minSUMA_BASESFilterEmpty : number;
    maxSAT_BASESFilter : number;
		maxSAT_BASESFilterEmpty : number;
		minSAT_BASESFilter : number;
		minSAT_BASESFilterEmpty : number;
    maxSAT_KFilter : number;
		maxSAT_KFilterEmpty : number;
		minSAT_KFilter : number;
		minSAT_KFilterEmpty : number;
    maxSAT_CAFilter : number;
		maxSAT_CAFilterEmpty : number;
		minSAT_CAFilter : number;
		minSAT_CAFilterEmpty : number;
    maxSAT_MGFilter : number;
		maxSAT_MGFilterEmpty : number;
		minSAT_MGFilter : number;
		minSAT_MGFilterEmpty : number;
    maxSAT_NAFilter : number;
		maxSAT_NAFilterEmpty : number;
		minSAT_NAFilter : number;
		minSAT_NAFilterEmpty : number;
    maxSAT_ALFilter : number;
		maxSAT_ALFilterEmpty : number;
		minSAT_ALFilter : number;
		minSAT_ALFilterEmpty : number;
    maxCA_MGFilter : number;
		maxCA_MGFilterEmpty : number;
		minCA_MGFilter : number;
		minCA_MGFilterEmpty : number;
    maxK_MGFilter : number;
		maxK_MGFilterEmpty : number;
		minK_MGFilter : number;
		minK_MGFilterEmpty : number;
    maxCA_MG_DIV_KFilter : number;
		maxCA_MG_DIV_KFilterEmpty : number;
		minCA_MG_DIV_KFilter : number;
		minCA_MG_DIV_KFilterEmpty : number;
    analisiS_IDFilter = '';


    _entityTypeFullName = 'PALMASoft.ASuelos.ASuelo';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _aSuelosServiceProxy: ASuelosServiceProxy,
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

    getASuelos(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._aSuelosServiceProxy.getAll(
            this.filterText,
            this.coD_SUELOSFilter,
            this.iD_SUELOSFilter,
            this.materiaL_SUELOSFilter,
            this.maxPROFUNDIDAD_MUESTRAFilter == null ? this.maxPROFUNDIDAD_MUESTRAFilterEmpty: this.maxPROFUNDIDAD_MUESTRAFilter,
            this.minPROFUNDIDAD_MUESTRAFilter == null ? this.minPROFUNDIDAD_MUESTRAFilterEmpty: this.minPROFUNDIDAD_MUESTRAFilter,
            this.texturA_SUELOSFilter,
            this.maxARENAFilter == null ? this.maxARENAFilterEmpty: this.maxARENAFilter,
            this.minARENAFilter == null ? this.minARENAFilterEmpty: this.minARENAFilter,
            this.maxLIMOFilter == null ? this.maxLIMOFilterEmpty: this.maxLIMOFilter,
            this.minLIMOFilter == null ? this.minLIMOFilterEmpty: this.minLIMOFilter,
            this.maxARCILLAFilter == null ? this.maxARCILLAFilterEmpty: this.maxARCILLAFilter,
            this.minARCILLAFilter == null ? this.minARCILLAFilterEmpty: this.minARCILLAFilter,
            this.maxPHFilter == null ? this.maxPHFilterEmpty: this.maxPHFilter,
            this.minPHFilter == null ? this.minPHFilterEmpty: this.minPHFilter,
            this.maxCARBONO_ORGANICOFilter == null ? this.maxCARBONO_ORGANICOFilterEmpty: this.maxCARBONO_ORGANICOFilter,
            this.minCARBONO_ORGANICOFilter == null ? this.minCARBONO_ORGANICOFilterEmpty: this.minCARBONO_ORGANICOFilter,
            this.maxMATERIA_ORGANICAFilter == null ? this.maxMATERIA_ORGANICAFilterEmpty: this.maxMATERIA_ORGANICAFilter,
            this.minMATERIA_ORGANICAFilter == null ? this.minMATERIA_ORGANICAFilterEmpty: this.minMATERIA_ORGANICAFilter,
            this.maxFOSFOROFilter == null ? this.maxFOSFOROFilterEmpty: this.maxFOSFOROFilter,
            this.minFOSFOROFilter == null ? this.minFOSFOROFilterEmpty: this.minFOSFOROFilter,
            this.maxAZUFREFilter == null ? this.maxAZUFREFilterEmpty: this.maxAZUFREFilter,
            this.minAZUFREFilter == null ? this.minAZUFREFilterEmpty: this.minAZUFREFilter,
            this.maxACIDEZFilter == null ? this.maxACIDEZFilterEmpty: this.maxACIDEZFilter,
            this.minACIDEZFilter == null ? this.minACIDEZFilterEmpty: this.minACIDEZFilter,
            this.maxALUMINIOFilter == null ? this.maxALUMINIOFilterEmpty: this.maxALUMINIOFilter,
            this.minALUMINIOFilter == null ? this.minALUMINIOFilterEmpty: this.minALUMINIOFilter,
            this.maxCALCIOFilter == null ? this.maxCALCIOFilterEmpty: this.maxCALCIOFilter,
            this.minCALCIOFilter == null ? this.minCALCIOFilterEmpty: this.minCALCIOFilter,
            this.maxMAGNESIOFilter == null ? this.maxMAGNESIOFilterEmpty: this.maxMAGNESIOFilter,
            this.minMAGNESIOFilter == null ? this.minMAGNESIOFilterEmpty: this.minMAGNESIOFilter,
            this.maxPOTASIOFilter == null ? this.maxPOTASIOFilterEmpty: this.maxPOTASIOFilter,
            this.minPOTASIOFilter == null ? this.minPOTASIOFilterEmpty: this.minPOTASIOFilter,
            this.maxSODIOFilter == null ? this.maxSODIOFilterEmpty: this.maxSODIOFilter,
            this.minSODIOFilter == null ? this.minSODIOFilterEmpty: this.minSODIOFilter,
            this.maxCATIONICOFilter == null ? this.maxCATIONICOFilterEmpty: this.maxCATIONICOFilter,
            this.minCATIONICOFilter == null ? this.minCATIONICOFilterEmpty: this.minCATIONICOFilter,
            this.maxELECTRICAFilter == null ? this.maxELECTRICAFilterEmpty: this.maxELECTRICAFilter,
            this.minELECTRICAFilter == null ? this.minELECTRICAFilterEmpty: this.minELECTRICAFilter,
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
            this.maxCICEFilter == null ? this.maxCICEFilterEmpty: this.maxCICEFilter,
            this.minCICEFilter == null ? this.minCICEFilterEmpty: this.minCICEFilter,
            this.maxSUMA_BASESFilter == null ? this.maxSUMA_BASESFilterEmpty: this.maxSUMA_BASESFilter,
            this.minSUMA_BASESFilter == null ? this.minSUMA_BASESFilterEmpty: this.minSUMA_BASESFilter,
            this.maxSAT_BASESFilter == null ? this.maxSAT_BASESFilterEmpty: this.maxSAT_BASESFilter,
            this.minSAT_BASESFilter == null ? this.minSAT_BASESFilterEmpty: this.minSAT_BASESFilter,
            this.maxSAT_KFilter == null ? this.maxSAT_KFilterEmpty: this.maxSAT_KFilter,
            this.minSAT_KFilter == null ? this.minSAT_KFilterEmpty: this.minSAT_KFilter,
            this.maxSAT_CAFilter == null ? this.maxSAT_CAFilterEmpty: this.maxSAT_CAFilter,
            this.minSAT_CAFilter == null ? this.minSAT_CAFilterEmpty: this.minSAT_CAFilter,
            this.maxSAT_MGFilter == null ? this.maxSAT_MGFilterEmpty: this.maxSAT_MGFilter,
            this.minSAT_MGFilter == null ? this.minSAT_MGFilterEmpty: this.minSAT_MGFilter,
            this.maxSAT_NAFilter == null ? this.maxSAT_NAFilterEmpty: this.maxSAT_NAFilter,
            this.minSAT_NAFilter == null ? this.minSAT_NAFilterEmpty: this.minSAT_NAFilter,
            this.maxSAT_ALFilter == null ? this.maxSAT_ALFilterEmpty: this.maxSAT_ALFilter,
            this.minSAT_ALFilter == null ? this.minSAT_ALFilterEmpty: this.minSAT_ALFilter,
            this.maxCA_MGFilter == null ? this.maxCA_MGFilterEmpty: this.maxCA_MGFilter,
            this.minCA_MGFilter == null ? this.minCA_MGFilterEmpty: this.minCA_MGFilter,
            this.maxK_MGFilter == null ? this.maxK_MGFilterEmpty: this.maxK_MGFilter,
            this.minK_MGFilter == null ? this.minK_MGFilterEmpty: this.minK_MGFilter,
            this.maxCA_MG_DIV_KFilter == null ? this.maxCA_MG_DIV_KFilterEmpty: this.maxCA_MG_DIV_KFilter,
            this.minCA_MG_DIV_KFilter == null ? this.minCA_MG_DIV_KFilterEmpty: this.minCA_MG_DIV_KFilter,
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

    createASuelo(): void {
        this.createOrEditASueloModal.show();
    }

    showHistory(aSuelo: ASueloDto): void {
        this.entityTypeHistoryModal.show({
            entityId: aSuelo.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteASuelo(aSuelo: ASueloDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._aSuelosServiceProxy.delete(aSuelo.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._aSuelosServiceProxy.getASuelosToExcel(
        this.filterText,
            this.coD_SUELOSFilter,
            this.iD_SUELOSFilter,
            this.materiaL_SUELOSFilter,
            this.maxPROFUNDIDAD_MUESTRAFilter == null ? this.maxPROFUNDIDAD_MUESTRAFilterEmpty: this.maxPROFUNDIDAD_MUESTRAFilter,
            this.minPROFUNDIDAD_MUESTRAFilter == null ? this.minPROFUNDIDAD_MUESTRAFilterEmpty: this.minPROFUNDIDAD_MUESTRAFilter,
            this.texturA_SUELOSFilter,
            this.maxARENAFilter == null ? this.maxARENAFilterEmpty: this.maxARENAFilter,
            this.minARENAFilter == null ? this.minARENAFilterEmpty: this.minARENAFilter,
            this.maxLIMOFilter == null ? this.maxLIMOFilterEmpty: this.maxLIMOFilter,
            this.minLIMOFilter == null ? this.minLIMOFilterEmpty: this.minLIMOFilter,
            this.maxARCILLAFilter == null ? this.maxARCILLAFilterEmpty: this.maxARCILLAFilter,
            this.minARCILLAFilter == null ? this.minARCILLAFilterEmpty: this.minARCILLAFilter,
            this.maxPHFilter == null ? this.maxPHFilterEmpty: this.maxPHFilter,
            this.minPHFilter == null ? this.minPHFilterEmpty: this.minPHFilter,
            this.maxCARBONO_ORGANICOFilter == null ? this.maxCARBONO_ORGANICOFilterEmpty: this.maxCARBONO_ORGANICOFilter,
            this.minCARBONO_ORGANICOFilter == null ? this.minCARBONO_ORGANICOFilterEmpty: this.minCARBONO_ORGANICOFilter,
            this.maxMATERIA_ORGANICAFilter == null ? this.maxMATERIA_ORGANICAFilterEmpty: this.maxMATERIA_ORGANICAFilter,
            this.minMATERIA_ORGANICAFilter == null ? this.minMATERIA_ORGANICAFilterEmpty: this.minMATERIA_ORGANICAFilter,
            this.maxFOSFOROFilter == null ? this.maxFOSFOROFilterEmpty: this.maxFOSFOROFilter,
            this.minFOSFOROFilter == null ? this.minFOSFOROFilterEmpty: this.minFOSFOROFilter,
            this.maxAZUFREFilter == null ? this.maxAZUFREFilterEmpty: this.maxAZUFREFilter,
            this.minAZUFREFilter == null ? this.minAZUFREFilterEmpty: this.minAZUFREFilter,
            this.maxACIDEZFilter == null ? this.maxACIDEZFilterEmpty: this.maxACIDEZFilter,
            this.minACIDEZFilter == null ? this.minACIDEZFilterEmpty: this.minACIDEZFilter,
            this.maxALUMINIOFilter == null ? this.maxALUMINIOFilterEmpty: this.maxALUMINIOFilter,
            this.minALUMINIOFilter == null ? this.minALUMINIOFilterEmpty: this.minALUMINIOFilter,
            this.maxCALCIOFilter == null ? this.maxCALCIOFilterEmpty: this.maxCALCIOFilter,
            this.minCALCIOFilter == null ? this.minCALCIOFilterEmpty: this.minCALCIOFilter,
            this.maxMAGNESIOFilter == null ? this.maxMAGNESIOFilterEmpty: this.maxMAGNESIOFilter,
            this.minMAGNESIOFilter == null ? this.minMAGNESIOFilterEmpty: this.minMAGNESIOFilter,
            this.maxPOTASIOFilter == null ? this.maxPOTASIOFilterEmpty: this.maxPOTASIOFilter,
            this.minPOTASIOFilter == null ? this.minPOTASIOFilterEmpty: this.minPOTASIOFilter,
            this.maxSODIOFilter == null ? this.maxSODIOFilterEmpty: this.maxSODIOFilter,
            this.minSODIOFilter == null ? this.minSODIOFilterEmpty: this.minSODIOFilter,
            this.maxCATIONICOFilter == null ? this.maxCATIONICOFilterEmpty: this.maxCATIONICOFilter,
            this.minCATIONICOFilter == null ? this.minCATIONICOFilterEmpty: this.minCATIONICOFilter,
            this.maxELECTRICAFilter == null ? this.maxELECTRICAFilterEmpty: this.maxELECTRICAFilter,
            this.minELECTRICAFilter == null ? this.minELECTRICAFilterEmpty: this.minELECTRICAFilter,
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
            this.maxCICEFilter == null ? this.maxCICEFilterEmpty: this.maxCICEFilter,
            this.minCICEFilter == null ? this.minCICEFilterEmpty: this.minCICEFilter,
            this.maxSUMA_BASESFilter == null ? this.maxSUMA_BASESFilterEmpty: this.maxSUMA_BASESFilter,
            this.minSUMA_BASESFilter == null ? this.minSUMA_BASESFilterEmpty: this.minSUMA_BASESFilter,
            this.maxSAT_BASESFilter == null ? this.maxSAT_BASESFilterEmpty: this.maxSAT_BASESFilter,
            this.minSAT_BASESFilter == null ? this.minSAT_BASESFilterEmpty: this.minSAT_BASESFilter,
            this.maxSAT_KFilter == null ? this.maxSAT_KFilterEmpty: this.maxSAT_KFilter,
            this.minSAT_KFilter == null ? this.minSAT_KFilterEmpty: this.minSAT_KFilter,
            this.maxSAT_CAFilter == null ? this.maxSAT_CAFilterEmpty: this.maxSAT_CAFilter,
            this.minSAT_CAFilter == null ? this.minSAT_CAFilterEmpty: this.minSAT_CAFilter,
            this.maxSAT_MGFilter == null ? this.maxSAT_MGFilterEmpty: this.maxSAT_MGFilter,
            this.minSAT_MGFilter == null ? this.minSAT_MGFilterEmpty: this.minSAT_MGFilter,
            this.maxSAT_NAFilter == null ? this.maxSAT_NAFilterEmpty: this.maxSAT_NAFilter,
            this.minSAT_NAFilter == null ? this.minSAT_NAFilterEmpty: this.minSAT_NAFilter,
            this.maxSAT_ALFilter == null ? this.maxSAT_ALFilterEmpty: this.maxSAT_ALFilter,
            this.minSAT_ALFilter == null ? this.minSAT_ALFilterEmpty: this.minSAT_ALFilter,
            this.maxCA_MGFilter == null ? this.maxCA_MGFilterEmpty: this.maxCA_MGFilter,
            this.minCA_MGFilter == null ? this.minCA_MGFilterEmpty: this.minCA_MGFilter,
            this.maxK_MGFilter == null ? this.maxK_MGFilterEmpty: this.maxK_MGFilter,
            this.minK_MGFilter == null ? this.minK_MGFilterEmpty: this.minK_MGFilter,
            this.maxCA_MG_DIV_KFilter == null ? this.maxCA_MG_DIV_KFilterEmpty: this.maxCA_MG_DIV_KFilter,
            this.minCA_MG_DIV_KFilter == null ? this.minCA_MG_DIV_KFilterEmpty: this.minCA_MG_DIV_KFilter,
            this.analisiS_IDFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
