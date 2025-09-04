import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { IPaciente } from '../../Interfaces/ipaciente';
import { ICiudad } from '../../Interfaces/iciudad';
import { CiudadService } from '../../Service/ciudad';
import { PacienteService } from '../../Service/paciente';

@Component({
  selector: 'app-nuevo-paciente',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './nuevo-paciente.html',
  styleUrls: ['./nuevo-paciente.css']
})
export class NuevoPacienteComponent implements OnInit {
  
  // Referencia al formulario del template
  @ViewChild('pacienteForm') public pacienteForm!: NgForm;

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

  public ciudadesDisponibles: ICiudad[] = [];

  constructor(private ciudadService: CiudadService, private pacienteService: PacienteService) { }

  ngOnInit(): void {
    this.cargarCiudades();
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

  public onSubmit(): void {
    if (this.pacienteForm.valid) {
      console.log('Formulario enviado. Datos:', this.paciente);
      this.pacienteService.guardarPaciente(this.paciente).subscribe({
        next: (paciente: IPaciente) => {
          console.log('Paciente guardado:', paciente);
          this.pacienteForm.resetForm();
        },
        error: (err: any) => {
          console.error('Error al guardar el paciente:', err);
        }
      });
    } else {
      console.log('El formulario no es válido. Revise los campos.');
    }
  }

  public onReset(): void {
    this.pacienteForm.resetForm();
  }
}