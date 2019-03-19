import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { ASuelosComponent } from './aSuelos/aSuelos/aSuelos.component';
import { ViewASueloModalComponent } from './aSuelos/aSuelos/view-aSuelo-modal.component';
import { CreateOrEditASueloModalComponent } from './aSuelos/aSuelos/create-or-edit-aSuelo-modal.component';

import { AFoliaresComponent } from './aFoliares/aFoliares/aFoliares.component';
import { ViewAFoliarModalComponent } from './aFoliares/aFoliares/view-aFoliar-modal.component';
import { CreateOrEditAFoliarModalComponent } from './aFoliares/aFoliares/create-or-edit-aFoliar-modal.component';

import { AnalisesComponent } from './analises/analises/analises.component';
import { ViewAnalisisModalComponent } from './analises/analises/view-analisis-modal.component';
import { CreateOrEditAnalisisModalComponent } from './analises/analises/create-or-edit-analisis-modal.component';
import { FincaLookupTableModalComponent } from './analises/analises/finca-lookup-table-modal.component';

import { FincasComponent } from './fincas/fincas/fincas.component';
import { ViewFincaModalComponent } from './fincas/fincas/view-finca-modal.component';
import { CreateOrEditFincaModalComponent } from './fincas/fincas/create-or-edit-finca-modal.component';
import { ClienteLookupTableModalComponent } from './fincas/fincas/cliente-lookup-table-modal.component';

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
import { AnaliseComboComponent } from './shared/analise-combo/analise-combo.component';

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
		ASuelosComponent,
		ViewASueloModalComponent,		CreateOrEditASueloModalComponent,
		AFoliaresComponent,
		ViewAFoliarModalComponent,		CreateOrEditAFoliarModalComponent,
		AnalisesComponent,
		ViewAnalisisModalComponent,		CreateOrEditAnalisisModalComponent,
    FincaLookupTableModalComponent,
        FincasComponent,
        ViewFincaModalComponent, CreateOrEditFincaModalComponent,
        ClienteLookupTableModalComponent,
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
        DashboardComponent,
        AnaliseComboComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
