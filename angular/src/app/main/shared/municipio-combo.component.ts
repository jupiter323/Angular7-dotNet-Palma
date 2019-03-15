import { Component, ElementRef, EventEmitter, Injector, Input, OnInit, Output, ViewChild, forwardRef } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { MunicipioDto, MunicipiosServiceProxy, GetMunicipioForViewDto, DepartamentosServiceProxy } from '@shared/service-proxies/service-proxies';
import { ControlValueAccessor, FormControl, ReactiveFormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    selector: 'municipio-combo',
    template:
        `
    <select class="form-control" [formControl]="selectedMunicipio">
         <option *ngFor="let municipio of mis_municipios" [value]="municipio.municipio.id">{{municipio.municipio.nombrE_MUNICIPIO}}
    </select>`,
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => MunicipioComboComponent),
        multi: true,
    }]
})
export class MunicipioComboComponent extends AppComponentBase implements OnInit, ControlValueAccessor {

    @Input('departmentId')

    set departmentId(val: any) {
        console.log("departmento ID", val) // <-- do your logic here!
        if (val)
            this._departamentoService.getDepartamentoForView(val).subscribe(result => {
                console.log("departmento :", result);
                this.getFilteredMunicipiosByDepartmentNom(result.departamento.nombrE_DEPARTAMENTO);
            });
    }

    municipios: MunicipioDto[] = [];
    selectedMunicipio = new FormControl('');
    mis_municipios: GetMunicipioForViewDto[] = [];
    onTouched: any = () => { };

    constructor(
        private _departamentoService: DepartamentosServiceProxy,
        private _municipioService: MunicipiosServiceProxy,
        injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
        this.getFilteredMunicipiosByDepartmentNom(undefined);
        //this._departamentoService.getDepartamentos(undefined).subscribe(result => {
        //this._departamentoService.getDepartamentos(1).subscribe(result => {
        //    this.departamentos = result.items;
        //});

    }
    getFilteredMunicipiosByDepartmentNom(DepartmentNom: string) {
        this._municipioService.getAll(undefined, undefined, undefined, DepartmentNom, undefined, undefined, undefined).subscribe(
            result => {
                //this.departamentos = result.items;

                this.mis_municipios = result.items;
                //console.log(this.mis_departamentos[0].departamento.id);
                return this.mis_municipios;

            },
            error => {
                console.log('ERROR: ' + error);
            });

    }


    writeValue(obj: any): void {
        if (this.selectedMunicipio) {
            this.selectedMunicipio.setValue(obj);
        }
    }

    registerOnChange(fn: any): void {
        this.selectedMunicipio.valueChanges.subscribe(fn);
    }

    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        if (isDisabled) {
            this.selectedMunicipio.disable();
        } else {
            this.selectedMunicipio.enable();
        }
    }
}
