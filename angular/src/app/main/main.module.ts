import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { ClientesComponent } from './clientes/clientes/clientes.component';
import { ViewClienteModalComponent } from './clientes/clientes/view-cliente-modal.component';
import { CreateOrEditClienteModalComponent } from './clientes/clientes/create-or-edit-cliente-modal.component';
import { DepartamentoComboComponent } from 'app/main/shared/departamento-combo.component';
import { MunicipioComboComponent } from 'app/main/shared/municipio-combo.component';

import { MunicipiosComponent } from './municipios/municipios/municipios.component';
import { ViewMunicipioModalComponent } from './municipios/municipios/view-municipio-modal.component';
import { CreateOrEditMunicipioModalComponent } from './municipios/municipios/create-or-edit-municipio-modal.component';
import { DepartamentoLookupTableModalComponent } from './municipios/municipios/departamento-lookup-table-modal.component';

import { DepartamentosComponent } from './departamentos/departamentos/departamentos.component';
import { ViewDepartamentoModalComponent } from './departamentos/departamentos/view-departamento-modal.component';
import { CreateOrEditDepartamentoModalComponent } from './departamentos/departamentos/create-or-edit-departamento-modal.component';
import { PaisLookupTableModalComponent } from './departamentos/departamentos/pais-lookup-table-modal.component';

import { PaisesComponent } from './paises/paises/paises.component';
import { ViewPaisModalComponent } from './paises/paises/view-pais-modal.component';
import { CreateOrEditPaisModalComponent } from './paises/paises/create-or-edit-pais-modal.component';
import { AutoCompleteModule } from 'primeng/primeng';
import { PaginatorModule } from 'primeng/primeng';
import { EditorModule } from 'primeng/primeng';
import { InputMaskModule } from 'primeng/primeng'; import { FileUploadModule } from 'primeng/primeng';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
        FileUploadModule,
        AutoCompleteModule,
        PaginatorModule,
        EditorModule,
        InputMaskModule, TableModule,

        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot()
    ],
    declarations: [
        ClientesComponent,
        ViewClienteModalComponent, CreateOrEditClienteModalComponent,
        MunicipiosComponent,
        MunicipioComboComponent,
        ViewMunicipioModalComponent, CreateOrEditMunicipioModalComponent,
        DepartamentoLookupTableModalComponent,
        DepartamentosComponent,
        DepartamentoComboComponent,
        ViewDepartamentoModalComponent, CreateOrEditDepartamentoModalComponent,
        PaisLookupTableModalComponent,
        PaisesComponent,
        ViewPaisModalComponent, CreateOrEditPaisModalComponent,
        DashboardComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
