import { Routes } from '@angular/router';
import { NuevoPacienteComponent } from './Pacientes/nuevo-paciente/nuevo-paciente';

export const routes: Routes = [
    
        {
            path: 'paciente',
            children: [
                {
                    path: 'nuevo',
                    component: NuevoPacienteComponent
                }
            ]
        }
    
];
