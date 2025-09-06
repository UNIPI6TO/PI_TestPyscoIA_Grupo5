import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Inject } from '@angular/core';
import { PacienteService } from '../../Service/paciente';
import { IPaciente } from '../../Interfaces/ipaciente';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-pacientes',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './pacientes.html',
  styleUrls: ['./pacientes.css'],

})
export class PacientesComponent implements OnInit {
eliminarPaciente(arg0: any) {
throw new Error('Method not implemented.');
}
editarPaciente(arg0: any) {
throw new Error('Method not implemented.');
}


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
}