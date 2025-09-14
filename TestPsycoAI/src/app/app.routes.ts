import { Routes } from '@angular/router';
import { NuevoPacienteComponent } from './Components/Pacientes/nuevo-paciente/nuevo-paciente';
import { PacientesComponent } from './Components/Pacientes/pacientes';
import { EditarPacienteComponent } from './Components/Pacientes/editar-paciente/editar-paciente';
import { TestComponent } from './Components/Test/test';
import { TipoTestComponent } from './Components/Configuraciones/tipo-test/tipo-test';
import { EditarTipoTestComponent } from './Components/Configuraciones/tipo-test/editar-tipo-test/editar-tipo-test';
import { NuevoTipoTestComponent } from './Components/Configuraciones/tipo-test/nuevo-tipo-test/nuevo-tipo-test';
import { ConfigEvaluacionesComponent } from './Components/Configuraciones/config-evaluaciones/config-evaluaciones';
import { ConfigEvaluacionesDetalleComponent } from './Components/Configuraciones/config-evaluaciones/config-evaluaciones-detalle/config-evaluaciones-detalle';
import { ConfigEvaluacionesEditarComponent } from './Components/Configuraciones/config-evaluaciones/config-evaluaciones-editar/config-evaluaciones-editar';
import { ConfigEvaluadoresComponent } from './Components/Configuraciones/config-evaluadores/config-evaluadores';
import { NuevoEvaluadorComponent } from './Components/Configuraciones/config-evaluadores/nuevo-evaluador/nuevo-evaluador';
import { EditarEvaluadorComponent } from './Components/Configuraciones/config-evaluadores/editar-evaluador/editar-evaluador';

export const routes: Routes = [
    
        {
            path: 'pacientes',
            children: [
                {
                    path: 'nuevo',
                    component: NuevoPacienteComponent
                },
                {
                    path: 'editar/:idpaciente',
                    component: EditarPacienteComponent
                },
                {
                    path: '',
                    component: PacientesComponent
                }
            ]

        },
        {
            path: 'test',
            children: [
                {
                    path: 'tomar-evaluacion',
                    component: TestComponent
                }
            ]
        },
        {
            path: 'configuraciones',
            children: [
                {
                    path: 'tipo-test',
                    component: TipoTestComponent
                },{
                    path: 'tipo-test/nuevo',
                    component: NuevoTipoTestComponent
                },
                {
                    path: 'tipo-test/editar/:id',
                    component: EditarTipoTestComponent
                },
                {
                    path: 'evaluaciones',
                    component: ConfigEvaluacionesComponent
                },
                {
                    path: 'evaluaciones/detalle/:id',
                    component: ConfigEvaluacionesDetalleComponent
                },
                {
                    path: 'evaluaciones/editar/:id',
                    component: ConfigEvaluacionesEditarComponent
                },
                {
                    path: 'evaluadores',
                    component: ConfigEvaluadoresComponent
                },
                {
                    path: 'evaluadores/nuevo',
                    component: NuevoEvaluadorComponent
                },
                {
                    path: 'evaluadores/editar/:id',
                    component: EditarEvaluadorComponent
                }
            ]
        }
    ];  
