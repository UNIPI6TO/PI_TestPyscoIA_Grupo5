import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ITipoTest } from '../../../../Interfaces/Configuraciones/itipo-test';
import { TipoTestService } from '../../../../Service/Configuraciones/tipo-test';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-editar-tipo-test',
  imports: [RouterLink,CommonModule,FormsModule],
  templateUrl: './editar-tipo-test.html',
  styleUrls: ['./editar-tipo-test.css']
})
export class EditarTipoTestComponent {

  tipoTest: ITipoTest;

  constructor(private tipoTestService: TipoTestService, private route: ActivatedRoute) {
    this.tipoTest = {
      id: 0,
      nombre: '',
      descripcion: '',
      instrucciones: '',
      creado: new Date(),
      actualizado: new Date(), 
      eliminado: false
    };
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.tipoTestService.getTipoTestById(id).subscribe({
        next: (data) => {
          this.tipoTest = data;
        },
        error: (error) => {
          Swal.fire('Error', error.message, 'error');
        }
      });
    }
  }

  onSubmit(): void {
    this.tipoTestService.updateTipoTest(this.tipoTest).subscribe({
      next: () => {
        Swal.fire('Éxito', 'Tipo de test actualizado correctamente', 'success');
      },
      error: (error) => {
        Swal.fire('Error', error.message, 'error');
      }
    });
  }
}
