import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { FincasServiceProxy, CreateOrEditFincaDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ClienteLookupTableModalComponent } from './cliente-lookup-table-modal.component';


@Component({
    selector: 'createOrEditFincaModal',
    templateUrl: './create-or-edit-finca-modal.component.html'
})
export class CreateOrEditFincaModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('clienteLookupTableModal') clienteLookupTableModal: ClienteLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    finca: CreateOrEditFincaDto = new CreateOrEditFincaDto();

    clienteNOMBRE_CLIENTE = '';


    constructor(
        injector: Injector,
        private _fincasServiceProxy: FincasServiceProxy
    ) {
        super(injector);
    }

    show(fincaId?: number): void {

        if (!fincaId) {
            this.finca = new CreateOrEditFincaDto();
            this.finca.id = fincaId;
            this.clienteNOMBRE_CLIENTE = '';

            this.active = true;
            this.modal.show();
        } else {
            this._fincasServiceProxy.getFincaForEdit(fincaId).subscribe(result => {
                this.finca = result.finca;

                this.clienteNOMBRE_CLIENTE = result.clienteNOMBRE_CLIENTE;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._fincasServiceProxy.createOrEdit(this.finca)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectClienteModal() {
        this.clienteLookupTableModal.id = this.finca.clienteId;
        this.clienteLookupTableModal.displayName = this.clienteNOMBRE_CLIENTE;
        this.clienteLookupTableModal.show();
    }


        setClienteIdNull() {
        this.finca.clienteId = null;
        this.clienteNOMBRE_CLIENTE = '';
    }


        getNewClienteId() {
        this.finca.clienteId = this.clienteLookupTableModal.id;
        this.clienteNOMBRE_CLIENTE = this.clienteLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
