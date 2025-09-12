import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ITipoTest } from '../../../../Interfaces/Configuraciones/itipo-test';
import { TipoTestService } from '../../../../Service/Configuraciones/tipo-test';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-nuevo-tipo-test',
  imports: [RouterLink,CommonModule,FormsModule],
  templateUrl: './nuevo-tipo-test.html',
  styleUrls: ['./nuevo-tipo-test.css']
})
export class NuevoTipoTestComponent {
  tipoTest: ITipoTest = {
    id: 0,
    nombre: '',
    descripcion: '',
    instrucciones: '',
    creado: new Date(),
    
    eliminado: false
  };

  constructor(private tipoTestService: TipoTestService, private router: Router) { }

  onSubmit() {
    this.tipoTestService.createTipoTest(this.tipoTest).subscribe({
      next: () => {
        Swal.fire('Éxito', 'Tipo de evaluación creado correctamente', 'success');
        this.router.navigate(['/configuraciones/tipo-test']);
      },
      error: (error) => {
        Swal.fire('Error', error.message, 'error');
      }
    });
  }
}
