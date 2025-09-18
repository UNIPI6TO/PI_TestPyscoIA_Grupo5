import { Component, OnInit } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { Inject } from '@angular/core';
import { PacienteService } from '../../Service/paciente';
import { IPaciente } from '../../Interfaces/ipaciente';
import { Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { IUsuario } from '../../Interfaces/Login/iusuario';
import { AccesoDenegadoComponent } from '../../layout/acceso-denegado/acceso-denegado';
@Component({
  selector: 'app-pacientes',
  imports: [CommonModule, FormsModule, RouterLink, AccesoDenegadoComponent],
  templateUrl: './pacientes.html',
  styleUrls: ['./pacientes.css'],

})
export class PacientesComponent implements OnInit {
  
  public pacientes: IPaciente[] = [];
  public pacientesFiltrados: IPaciente[] = [];
  // Variables para los campos de búsqueda
  public filtro: string = '';

  constructor(@Inject(PacienteService) 
              private pacienteService: PacienteService,
              private titleService: Title,
              private router: Router
            ) { }

  ngOnInit(): void {
    this.cargarPacientes();
    this.titleService.setTitle('Gestión de Pacientes - PsycoAI');
    this.verificarSesion();
  }

  rolesValidos: string[] = ['ADMIN', 'EVALUADOR'];
  accesoDenegado: boolean = false;
  sesion: IUsuario | null = null;
  iniciadaSesion: boolean = false;
  verificarSesion(){
    const match = document.cookie.match(new RegExp('(^| )username=([^;]+)'));
    if (match) {
      const username = JSON.parse(decodeURIComponent(match[2]));
      this.sesion = username;
      this.iniciadaSesion = true;
      if (this.sesion && !this.rolesValidos.includes(this.sesion.rol)) {
        this.accesoDenegado = true;
      }
    } else {
      this.sesion = null;
      this.iniciadaSesion = false;
      this.router.navigate(['/iniciar-sesion']).then(() => {
        window.location.reload();
      });
    }
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