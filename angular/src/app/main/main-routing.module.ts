import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ASuelosComponent } from './aSuelos/aSuelos/aSuelos.component';
import { AFoliaresComponent } from './aFoliares/aFoliares/aFoliares.component';
import { AnalisesComponent } from './analises/analises/analises.component';
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
                    { path: 'aSuelos/aSuelos/:id', component: ASuelosComponent, data: { permission: 'Pages.ASuelos' }  },
                    { path: 'aFoliares/aFoliares/:id', component: AFoliaresComponent, data: { permission: 'Pages.AFoliares' }  },
                    { path: 'aSuelos/aSuelos', component: ASuelosComponent, data: { permission: 'Pages.ASuelos' }  },
                    { path: 'aFoliares/aFoliares', component: AFoliaresComponent, data: { permission: 'Pages.AFoliares' }  },
                    { path: 'analises/analises', component: AnalisesComponent, data: { permission: 'Pages.Analises' }  },
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
