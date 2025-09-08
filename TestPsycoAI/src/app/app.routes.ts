import { Routes } from '@angular/router';
import { NuevoPacienteComponent } from './Components/Pacientes/nuevo-paciente/nuevo-paciente';
import { PacientesComponent } from './Components/Pacientes/pacientes';
import { EditarPacienteComponent } from './Components/Pacientes/editar-paciente/editar-paciente';
import { TestComponent } from './Components/Test/test';

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
        }
    ];  
