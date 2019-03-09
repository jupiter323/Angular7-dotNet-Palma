import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ClientesServiceProxy, CreateOrEditClienteDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditClienteModal',
    templateUrl: './create-or-edit-cliente-modal.component.html'
})
export class CreateOrEditClienteModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    cliente: CreateOrEditClienteDto = new CreateOrEditClienteDto();

            fechA_CLIENTE: Date;


    constructor(
        injector: Injector,
        private _clientesServiceProxy: ClientesServiceProxy
    ) {
        super(injector);
    }

    show(clienteId?: number): void {
this.fechA_CLIENTE = null;

        if (!clienteId) {
            this.cliente = new CreateOrEditClienteDto();
            this.cliente.id = clienteId;

            this.active = true;
            this.modal.show();
        } else {
            this._clientesServiceProxy.getClienteForEdit(clienteId).subscribe(result => {
                this.cliente = result.cliente;

                if (this.cliente.fechA_CLIENTE) {
					this.fechA_CLIENTE = this.cliente.fechA_CLIENTE.toDate();
                }

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
        if (this.fechA_CLIENTE) {
            if (!this.cliente.fechA_CLIENTE) {
                this.cliente.fechA_CLIENTE = moment(this.fechA_CLIENTE).startOf('day');
            }
            else {
                this.cliente.fechA_CLIENTE = moment(this.fechA_CLIENTE);
            }
        }
        else {
            this.cliente.fechA_CLIENTE = null;
        }
            this._clientesServiceProxy.createOrEdit(this.cliente)
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
