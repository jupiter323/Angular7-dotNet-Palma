import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPaisForViewDto, PaisDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPaisModal',
    templateUrl: './view-pais-modal.component.html'
})
export class ViewPaisModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPaisForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPaisForViewDto();
        this.item.pais = new PaisDto();
    }

    show(item: GetPaisForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
