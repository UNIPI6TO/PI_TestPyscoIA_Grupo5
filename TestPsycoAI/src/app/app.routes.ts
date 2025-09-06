import { Routes } from '@angular/router';
import { NuevoPacienteComponent } from './Pacientes/nuevo-paciente/nuevo-paciente';
import { PacientesComponent } from './Pacientes/pacientes';

export const routes: Routes = [
    
        {
            path: 'pacientes',
            children: [
                {
                    path: 'nuevo',
                    component: NuevoPacienteComponent
                },
                {
                    path: '',
                    component: PacientesComponent
                }
            ]

        }
    
];
