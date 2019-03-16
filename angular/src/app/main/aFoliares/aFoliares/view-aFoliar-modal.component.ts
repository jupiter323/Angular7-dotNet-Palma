import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetAFoliarForViewDto, AFoliarDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAFoliarModal',
    templateUrl: './view-aFoliar-modal.component.html'
})
export class ViewAFoliarModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAFoliarForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetAFoliarForViewDto();
        this.item.aFoliar = new AFoliarDto();
    }

    show(item: GetAFoliarForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
