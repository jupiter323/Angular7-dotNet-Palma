import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetAnalisisForViewDto, AnalisisDto, AnalisisTipo } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAnalisisModal',
    templateUrl: './view-analisis-modal.component.html'
})
export class ViewAnalisisModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAnalisisForViewDto;
    analisisTipo = AnalisisTipo;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetAnalisisForViewDto();
        this.item.analisis = new AnalisisDto();
    }

    show(item: GetAnalisisForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();

    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
