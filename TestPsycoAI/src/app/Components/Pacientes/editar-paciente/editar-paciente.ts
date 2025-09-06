import { Component, OnInit, ViewChild } from '@angular/core';
import { IPaciente } from '../../../Interfaces/ipaciente';
import { FormsModule, NgForm } from '@angular/forms';
import { ICiudad } from '../../../Interfaces/iciudad';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CiudadService } from '../../../Service/ciudad';
import { PacienteService } from '../../../Service/paciente';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-editar-paciente',
  imports: [RouterLink, CommonModule,FormsModule],
  templateUrl: './editar-paciente.html',
  styleUrls: ['./editar-paciente.css']
})
export class EditarPacienteComponent implements OnInit {

    @ViewChild('pacienteForm') public pacienteForm!: NgForm;
  
  public ciudadesDisponibles: ICiudad[] = [];

  public paciente: IPaciente = {
    id: 0,
    creado: '',
    actualizado: '',
    eliminado: false,
    cedula: '',
    nombre: '',
    email: '',
    fechaNacimiento: '',
    direccion: '',
    idCiudad: 0
  };
  private pacienteId: number = 0; 
  constructor(
    private ciudadService: CiudadService, 
    private pacienteService: PacienteService,
    private parametros: ActivatedRoute,
    private router: Router
  ) { }
  ngOnInit(): void {
    this.cargarCiudades();
    this.cargarPaciente();
  }

  public onSubmit(): void {
    if (!this.pacienteForm) 
    {
      console.error('El formulario no está disponible.');
      return;
    
    }
    if(this.paciente.id === 0){
      console.error('El ID del paciente no es válido.');
      return;
    }
    if(this.pacienteId != this.paciente.id){
      console.error('Manipulación de ID detectada.');
      return;
    }
    if (this.pacienteForm.valid) {
      this.pacienteService.actualizarPaciente(this.paciente).subscribe({
      next: (response) => {
        Swal.fire({
          icon: 'success',
          title: 'Éxito',
          text: 'Paciente actualizado correctamente.'
        });
        console.log('Paciente actualizado:', response);
        this.pacienteForm?.resetForm();
        this.paciente = {
          id: 0,
          creado: '',
          actualizado: '',
          eliminado: false,
          cedula: '',
          nombre: '',
          email: '',
          fechaNacimiento: '',
          direccion: '',
          idCiudad: 0
        };
        this.router.navigate(['/pacientes']);
      },
      error: (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'Error al actualizar el paciente.'
        });
      }
    });
  } else {
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Datos inválido. Por favor, corrige los errores.'
      });
  }
}

  private cargarPaciente(): void {
    // Aquí deberías obtener el ID del paciente desde la ruta o algún otro medio
    this.parametros.params.subscribe((parametros) => {
      if (parametros['idpaciente']) {
        this.pacienteId = +parametros['idpaciente']; // Convertir a número
      }
    });
    this.pacienteService.obtenerUnPaciente(this.pacienteId).subscribe({
      next: (paciente) => {
        this.paciente = paciente; 
        console.log('Paciente cargado:', this.paciente);
        // Formatear la fecha a 'yyyy-MM-dd' para el input tipo date
        const fechaFormateada = this.paciente.fechaNacimiento
          ? new Date(this.paciente.fechaNacimiento).toISOString().substring(0, 10)
          : '';
        this.pacienteForm?.setValue({
          cedula: this.paciente.cedula,
          nombre: this.paciente.nombre,
          email: this.paciente.email,
          fechaNacimiento: fechaFormateada,
          direccion: this.paciente.direccion,
          idCiudad: this.paciente.idCiudad
        });
      },
      error: (err) => {
        console.error('Error al cargar el paciente:', err);
      }
    });
  }
   private cargarCiudades(): void {
    this.ciudadService.getCiudades().subscribe({
      next: (ciudades) => {
        this.ciudadesDisponibles = ciudades;
        console.log('Ciudades cargadas:', this.ciudadesDisponibles);
      },
      error: (err) => {
        console.error('Error al cargar las ciudades:', err);
      }
    });
  } 
}
