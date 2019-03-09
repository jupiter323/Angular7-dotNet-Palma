import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetClienteForViewDto, ClienteDto , Generos} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewClienteModal',
    templateUrl: './view-cliente-modal.component.html'
})
export class ViewClienteModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetClienteForViewDto;
    generos = Generos;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetClienteForViewDto();
        this.item.cliente = new ClienteDto();
    }

    show(item: GetClienteForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
