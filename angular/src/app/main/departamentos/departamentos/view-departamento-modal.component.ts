import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetDepartamentoForViewDto, DepartamentoDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewDepartamentoModal',
    templateUrl: './view-departamento-modal.component.html'
})
export class ViewDepartamentoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetDepartamentoForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetDepartamentoForViewDto();
        this.item.departamento = new DepartamentoDto();
    }

    show(item: GetDepartamentoForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
