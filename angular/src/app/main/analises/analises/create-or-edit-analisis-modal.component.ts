import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { AnalisesServiceProxy, CreateOrEditAnalisisDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { FincaLookupTableModalComponent } from './finca-lookup-table-modal.component';


@Component({
    selector: 'createOrEditAnalisisModal',
    templateUrl: './create-or-edit-analisis-modal.component.html'
})
export class CreateOrEditAnalisisModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('fincaLookupTableModal') fincaLookupTableModal: FincaLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    analisis: CreateOrEditAnalisisDto = new CreateOrEditAnalisisDto();

            fechA_MUESTREO: Date;
            fechA_REGISTRO: Date;
            fechA_ENTREGA: Date;
    fincaNOMBRE_FINCA = '';


    constructor(
        injector: Injector,
        private _analisesServiceProxy: AnalisesServiceProxy
    ) {
        super(injector);
    }

    show(analisisId?: number): void {
this.fechA_MUESTREO = null;
this.fechA_REGISTRO = null;
this.fechA_ENTREGA = null;

        if (!analisisId) {
            this.analisis = new CreateOrEditAnalisisDto();
            this.analisis.id = analisisId;
            this.fincaNOMBRE_FINCA = '';

            this.active = true;
            this.modal.show();
        } else {
            this._analisesServiceProxy.getAnalisisForEdit(analisisId).subscribe(result => {
                this.analisis = result.analisis;

                if (this.analisis.fechA_MUESTREO) {
					this.fechA_MUESTREO = this.analisis.fechA_MUESTREO.toDate();
                }
                if (this.analisis.fechA_REGISTRO) {
					this.fechA_REGISTRO = this.analisis.fechA_REGISTRO.toDate();
                }
                if (this.analisis.fechA_ENTREGA) {
					this.fechA_ENTREGA = this.analisis.fechA_ENTREGA.toDate();
                }
                this.fincaNOMBRE_FINCA = result.fincaNOMBRE_FINCA;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
        if (this.fechA_MUESTREO) {
            if (!this.analisis.fechA_MUESTREO) {
                this.analisis.fechA_MUESTREO = moment(this.fechA_MUESTREO).startOf('day');
            }
            else {
                this.analisis.fechA_MUESTREO = moment(this.fechA_MUESTREO);
            }
        }
        else {
            this.analisis.fechA_MUESTREO = null;
        }
        if (this.fechA_REGISTRO) {
            if (!this.analisis.fechA_REGISTRO) {
                this.analisis.fechA_REGISTRO = moment(this.fechA_REGISTRO).startOf('day');
            }
            else {
                this.analisis.fechA_REGISTRO = moment(this.fechA_REGISTRO);
            }
        }
        else {
            this.analisis.fechA_REGISTRO = null;
        }
        if (this.fechA_ENTREGA) {
            if (!this.analisis.fechA_ENTREGA) {
                this.analisis.fechA_ENTREGA = moment(this.fechA_ENTREGA).startOf('day');
            }
            else {
                this.analisis.fechA_ENTREGA = moment(this.fechA_ENTREGA);
            }
        }
        else {
            this.analisis.fechA_ENTREGA = null;
        }
            this._analisesServiceProxy.createOrEdit(this.analisis)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectFincaModal() {
        this.fincaLookupTableModal.id = this.analisis.fincaId;
        this.fincaLookupTableModal.displayName = this.fincaNOMBRE_FINCA;
        this.fincaLookupTableModal.show();
    }


        setFincaIdNull() {
        this.analisis.fincaId = null;
        this.fincaNOMBRE_FINCA = '';
    }


        getNewFincaId() {
        this.analisis.fincaId = this.fincaLookupTableModal.id;
        this.fincaNOMBRE_FINCA = this.fincaLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
