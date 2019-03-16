import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetASueloForViewDto, ASueloDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewASueloModal',
    templateUrl: './view-aSuelo-modal.component.html'
})
export class ViewASueloModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetASueloForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetASueloForViewDto();
        this.item.aSuelo = new ASueloDto();
    }

    show(item: GetASueloForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
