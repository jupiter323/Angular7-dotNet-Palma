import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PaisesServiceProxy, CreateOrEditPaisDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPaisModal',
    templateUrl: './create-or-edit-pais-modal.component.html'
})
export class CreateOrEditPaisModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pais: CreateOrEditPaisDto = new CreateOrEditPaisDto();



    constructor(
        injector: Injector,
        private _paisesServiceProxy: PaisesServiceProxy
    ) {
        super(injector);
    }

    show(paisId?: number): void {

        if (!paisId) {
            this.pais = new CreateOrEditPaisDto();
            this.pais.id = paisId;

            console.log("is id:", paisId)
            this.active = true;
            this.modal.show();
        } else {
            console.log("is after id:", paisId)
            this._paisesServiceProxy.getPaisForEdit(paisId).subscribe(result => {
                this.pais = result.pais;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;
            console.log("save pais",this.pais)
			
            this._paisesServiceProxy.createOrEdit(this.pais)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
