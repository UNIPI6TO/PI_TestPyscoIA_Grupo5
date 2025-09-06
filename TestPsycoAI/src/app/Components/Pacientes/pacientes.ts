import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Inject } from '@angular/core';
import { PacienteService } from '../../Service/paciente';
import { IPaciente } from '../../Interfaces/ipaciente';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-pacientes',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './pacientes.html',
  styleUrls: ['./pacientes.css'],

})
export class PacientesComponent implements OnInit {
  
  



  public pacientes: IPaciente[] = [];
  public pacientesFiltrados: IPaciente[] = [];
  // Variables para los campos de búsqueda
  public filtro: string = '';

  constructor(@Inject(PacienteService) private pacienteService: PacienteService) { }

  ngOnInit(): void {
    this.cargarPacientes();
  }

  /**
   * Carga la lista completa de pacientes desde el servicio.
   */
  public cargarPacientes(): void {
    this.pacienteService.getPacientes().subscribe(
      (data: IPaciente[]) => {
        this.pacientes = data;
        this.aplicarFiltros();
      }
    );
  }

  /**
   * Aplica los filtros de búsqueda a la lista de pacientes.
   */
  public aplicarFiltros(): void {
    let tempPacientes = this.pacientes;

    if (this.filtro) {
      const filtroLower = this.filtro.toLowerCase();
      tempPacientes = tempPacientes.filter(paciente =>
      paciente.cedula?.toLowerCase().includes(filtroLower) ||
      paciente.nombre?.toLowerCase().includes(filtroLower) ||
      paciente.email?.toLowerCase().includes(filtroLower) ||
      paciente.fechaNacimiento?.toLowerCase().includes(filtroLower) ||
      paciente.direccion?.toLowerCase().includes(filtroLower) ||
      paciente.ciudad?.nombre?.toLowerCase().includes(filtroLower)
      );
    }

    

    this.pacientesFiltrados = tempPacientes;
  }
  
  eliminarPaciente(id: number) {
  Swal.fire({
    title: '¿Estás seguro?',
    text: 'Esta acción no se puede deshacer.',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#d33',
    cancelButtonColor: '#3085d6',
    confirmButtonText: 'Sí, eliminar',
    cancelButtonText: 'Cancelar'
  }).then((result) => {
    if (result.isConfirmed) {
      this.pacienteService.eliminarPaciente(id).subscribe({
        next: () => {
          console.log(`Paciente con ID ${id} eliminado`);
          this.cargarPacientes(); // Recargar la lista de pacientes
        },
        error: (err) => {
          console.error('Error al eliminar el paciente:', err);
          Swal.fire('Error', 'No se pudo eliminar el paciente.', 'error');
        }
      });
    }
  });
}

}