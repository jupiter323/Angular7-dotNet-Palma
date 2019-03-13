import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { DepartamentosServiceProxy, CreateOrEditDepartamentoDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { PaisLookupTableModalComponent } from './pais-lookup-table-modal.component';


@Component({
    selector: 'createOrEditDepartamentoModal',
    templateUrl: './create-or-edit-departamento-modal.component.html'
})
export class CreateOrEditDepartamentoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('paisLookupTableModal') paisLookupTableModal: PaisLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    departamento: CreateOrEditDepartamentoDto = new CreateOrEditDepartamentoDto();

    paisNOMBRE_PAIS = '';


    constructor(
        injector: Injector,
        private _departamentosServiceProxy: DepartamentosServiceProxy
    ) {
        super(injector);
    }

    show(departamentoId?: number): void {

        if (!departamentoId) {
            this.departamento = new CreateOrEditDepartamentoDto();
            this.departamento.id = departamentoId;
            this.paisNOMBRE_PAIS = '';

            this.active = true;
            this.modal.show();
        } else {
            this._departamentosServiceProxy.getDepartamentoForEdit(departamentoId).subscribe(result => {
                this.departamento = result.departamento;

                this.paisNOMBRE_PAIS = result.paisNOMBRE_PAIS;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;
            console.log(this.departamento)
			
            this._departamentosServiceProxy.createOrEdit(this.departamento)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectPaisModal() {
        this.paisLookupTableModal.id = this.departamento.paisId;
        this.paisLookupTableModal.displayName = this.paisNOMBRE_PAIS;
        this.paisLookupTableModal.show();
    }


        setPaisIdNull() {
        this.departamento.paisId = null;
        this.paisNOMBRE_PAIS = '';
    }


        getNewPaisId() {
        this.departamento.paisId = this.paisLookupTableModal.id;
        this.paisNOMBRE_PAIS = this.paisLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
