import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { AFoliaresServiceProxy, CreateOrEditAFoliarDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditAFoliarModal',
    templateUrl: './create-or-edit-aFoliar-modal.component.html'
})
export class CreateOrEditAFoliarModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    aFoliar: CreateOrEditAFoliarDto = new CreateOrEditAFoliarDto();



    constructor(
        injector: Injector,
        private _aFoliaresServiceProxy: AFoliaresServiceProxy
    ) {
        super(injector);
    }

    show(aFoliarId?: number): void {

        if (!aFoliarId) {
            this.aFoliar = new CreateOrEditAFoliarDto();
            this.aFoliar.id = aFoliarId;

            this.active = true;
            this.modal.show();
        } else {
            this._aFoliaresServiceProxy.getAFoliarForEdit(aFoliarId).subscribe(result => {
                this.aFoliar = result.aFoliar;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._aFoliaresServiceProxy.createOrEdit(this.aFoliar)
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
