import { Component, ElementRef, EventEmitter, Injector, Input, OnInit, Output, ViewChild, forwardRef } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DepartamentoDto, DepartamentosServiceProxy, GetDepartamentoForViewDto } from '@shared/service-proxies/service-proxies';
import { ControlValueAccessor, FormControl, ReactiveFormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    selector: 'departamento-combo',
    template:
        `
    <select class="form-control" [formControl]="selectedDepartamento">
         <option *ngFor="let departamento of mis_departamentos" [value]="departamento.departamento.id">{{departamento.departamento.nombrE_DEPARTAMENTO}}
    </select>`,
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => DepartamentoComboComponent),
        multi: true,
    }]
})
export class DepartamentoComboComponent extends AppComponentBase implements OnInit, ControlValueAccessor {

    departamentos: DepartamentoDto[] = [];
    selectedDepartamento = new FormControl('');
    mis_departamentos: GetDepartamentoForViewDto[] = [];
    onTouched: any = () => { };

    constructor(
        private _departamentoService: DepartamentosServiceProxy,
        injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {

        this._departamentoService.getAll(undefined, undefined, undefined, undefined, undefined, undefined, undefined).subscribe(
            result => {
                //this.departamentos = result.items;
                
                this.mis_departamentos = result.items;
                //console.log(this.mis_departamentos[0].departamento.id);
                return this.mis_departamentos;

            },
            error => {
                console.log('ERROR: ' + error);
            });

        //this._departamentoService.getDepartamentos(undefined).subscribe(result => {
        //this._departamentoService.getDepartamentos(1).subscribe(result => {
        //    this.departamentos = result.items;
        //});

    }

    writeValue(obj: any): void {
        if (this.selectedDepartamento) {
            this.selectedDepartamento.setValue(obj);
        }
    }

    registerOnChange(fn: any): void {
        this.selectedDepartamento.valueChanges.subscribe(fn);
    }

    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        if (isDisabled) {
            this.selectedDepartamento.disable();
        } else {
            this.selectedDepartamento.enable();
        }
    }
}
