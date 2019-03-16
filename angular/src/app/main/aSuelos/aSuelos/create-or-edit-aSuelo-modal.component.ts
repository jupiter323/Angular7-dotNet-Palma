import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ASuelosServiceProxy, CreateOrEditASueloDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditASueloModal',
    templateUrl: './create-or-edit-aSuelo-modal.component.html'
})
export class CreateOrEditASueloModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    aSuelo: CreateOrEditASueloDto = new CreateOrEditASueloDto();



    constructor(
        injector: Injector,
        private _aSuelosServiceProxy: ASuelosServiceProxy
    ) {
        super(injector);
    }

    show(aSueloId?: number): void {

        if (!aSueloId) {
            this.aSuelo = new CreateOrEditASueloDto();
            this.aSuelo.id = aSueloId;

            this.active = true;
            this.modal.show();
        } else {
            this._aSuelosServiceProxy.getASueloForEdit(aSueloId).subscribe(result => {
                this.aSuelo = result.aSuelo;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._aSuelosServiceProxy.createOrEdit(this.aSuelo)
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
