import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetMunicipioForViewDto, MunicipioDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewMunicipioModal',
    templateUrl: './view-municipio-modal.component.html'
})
export class ViewMunicipioModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetMunicipioForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetMunicipioForViewDto();
        this.item.municipio = new MunicipioDto();
    }

    show(item: GetMunicipioForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
