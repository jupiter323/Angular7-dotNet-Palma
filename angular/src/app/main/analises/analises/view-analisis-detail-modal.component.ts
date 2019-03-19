import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetAnalisisForViewDto, AnalisisDto, AnalisisTipo, ASuelosServiceProxy, AFoliaresServiceProxy, GetASueloForViewDto, GetFincaForViewDto, GetAFoliarForViewDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAnalisisDetailModal',
    templateUrl: './view-analisis-detail-modal.component.html'
})
export class ViewAnalisisDetailModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAnalisisForViewDto;
    analisisTipo = AnalisisTipo;

    sRecords: Array<GetASueloForViewDto> = [];
    fRecords: Array<GetAFoliarForViewDto> = []

    constructor(
        injector: Injector,
        private _aSuelosServiceProxy: ASuelosServiceProxy,
        private _aFoliaresServiceProxy: AFoliaresServiceProxy
    ) {
        super(injector);
        this.item = new GetAnalisisForViewDto();
        this.item.analisis = new AnalisisDto();
    }

    show(item: GetAnalisisForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();

        console.log(item.analisis.id);
        this.getDetails(item.analisis.id, item.analisis.tipO_INFORME)

    }
    getDetails(analisisId: any, tipoId: number) {
        if (tipoId == 0)
            this._aFoliaresServiceProxy.getAll(
                undefined,
                undefined,
                undefined,
                undefined, undefined
                , undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined,
                analisisId,
                undefined,
                undefined,
                undefined
            ).subscribe(result => {
                console.log(result.items)
                this.fRecords = result.items;
                this.sRecords = undefined
            });
        else if (tipoId == 1)
            this._aSuelosServiceProxy.getAll(
                undefined,
                undefined,
                undefined,
                undefined, undefined
                , undefined, undefined, undefined, undefined, undefined, undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                undefined,
                analisisId,
                undefined,
                undefined,
                undefined
            ).subscribe(result => {
                console.log(result.items)
                this.sRecords = result.items;
                this.fRecords = undefined;
            });

    }
    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
