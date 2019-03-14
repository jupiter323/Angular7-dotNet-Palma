import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FincasComponent } from './fincas/fincas/fincas.component';
import { ClientesComponent } from './clientes/clientes/clientes.component';
import { MunicipiosComponent } from './municipios/municipios/municipios.component';
import { DepartamentosComponent } from './departamentos/departamentos/departamentos.component';
import { PaisesComponent } from './paises/paises/paises.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'fincas/fincas', component: FincasComponent, data: { permission: 'Pages.Fincas' }  },
                    { path: 'clientes/clientes', component: ClientesComponent, data: { permission: 'Pages.Clientes' }  },
                    { path: 'municipios/municipios', component: MunicipiosComponent, data: { permission: 'Pages.Municipios' }  },
                    { path: 'departamentos/departamentos', component: DepartamentosComponent, data: { permission: 'Pages.Departamentos' }  },
                    { path: 'paises/paises', component: PaisesComponent, data: { permission: 'Pages.Paises' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
