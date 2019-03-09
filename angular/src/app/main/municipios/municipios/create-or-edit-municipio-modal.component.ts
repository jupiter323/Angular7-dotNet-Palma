import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { MunicipiosServiceProxy, CreateOrEditMunicipioDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { DepartamentoLookupTableModalComponent } from './departamento-lookup-table-modal.component';


@Component({
    selector: 'createOrEditMunicipioModal',
    templateUrl: './create-or-edit-municipio-modal.component.html'
})
export class CreateOrEditMunicipioModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('departamentoLookupTableModal') departamentoLookupTableModal: DepartamentoLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    municipio: CreateOrEditMunicipioDto = new CreateOrEditMunicipioDto();

    departamentoNOMBRE_DEPARTAMENTO = '';


    constructor(
        injector: Injector,
        private _municipiosServiceProxy: MunicipiosServiceProxy
    ) {
        super(injector);
    }

    show(municipioId?: number): void {

        if (!municipioId) {
            this.municipio = new CreateOrEditMunicipioDto();
            this.municipio.id = municipioId;
            this.departamentoNOMBRE_DEPARTAMENTO = '';

            this.active = true;
            this.modal.show();
        } else {
            this._municipiosServiceProxy.getMunicipioForEdit(municipioId).subscribe(result => {
                this.municipio = result.municipio;

                this.departamentoNOMBRE_DEPARTAMENTO = result.departamentoNOMBRE_DEPARTAMENTO;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._municipiosServiceProxy.createOrEdit(this.municipio)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectDepartamentoModal() {
        this.departamentoLookupTableModal.id = this.municipio.departamentoId;
        this.departamentoLookupTableModal.displayName = this.departamentoNOMBRE_DEPARTAMENTO;
        this.departamentoLookupTableModal.show();
    }


        setDepartamentoIdNull() {
        this.municipio.departamentoId = null;
        this.departamentoNOMBRE_DEPARTAMENTO = '';
    }


        getNewDepartamentoId() {
        this.municipio.departamentoId = this.departamentoLookupTableModal.id;
        this.departamentoNOMBRE_DEPARTAMENTO = this.departamentoLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
