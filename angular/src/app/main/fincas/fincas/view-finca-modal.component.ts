import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetFincaForViewDto, FincaDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewFincaModal',
    templateUrl: './view-finca-modal.component.html'
})
export class ViewFincaModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetFincaForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetFincaForViewDto();
        this.item.finca = new FincaDto();
    }

    show(item: GetFincaForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
