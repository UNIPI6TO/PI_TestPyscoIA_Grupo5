import { Injectable } from '@angular/core';
import { jsPDF } from 'jspdf'
import { autoTable } from 'jspdf-autotable'
import { IEvaluacion } from '../../Interfaces/Evaluaciones/ievaluacion';
import { IPaciente } from '../../Interfaces/ipaciente';
import { IEvaluadores } from '../../Interfaces/Configuraciones/ievaluadores';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class PdfService {
    constructor() { }

  async generarPdf(evaluacion: IEvaluacion, paciente: IPaciente, evaluador: IEvaluadores, rol:string): Promise<Blob> {
    if (!evaluacion || !paciente || !evaluador) {
      Swal.fire('Error', 'No hay evaluación cargada para exportar.', 'error');
      return new Blob();
    }

    const doc = new jsPDF();
    // Attach autoTable to jsPDF instance if not already
    if (typeof (doc as any).autoTable !== 'function') {
      (doc as any).autoTable = autoTable;
    }
    let y = 18;

    // Título
    doc.setFontSize(16);
    doc.text(`Detalle de Evaluación: ${evaluacion.configuracionTest?.tipoTest?.nombre ?? ''}`, 14, y);
    y += 8;

    // Datos del paciente
    doc.setFontSize(11);
    doc.text(`Cédula: ${paciente?.cedula ?? ''}`, 14, y); y += 6;
    doc.text(`Nombre: ${paciente?.nombre ?? ''}`, 14, y); y += 6;
    doc.text(`Edad: ${this.calcularEdad(paciente?.fechaNacimiento?.toString() ?? '')} años`, 14, y); y += 6;
    doc.text(`Ciudad: ${paciente?.ciudad?.nombre ?? ''}`, 14, y); y += 6;
    doc.text(`Email: ${paciente?.email ?? ''}`, 14, y); y += 6;
    doc.text(`Dirección: ${paciente?.direccion ?? ''}`, 14, y); y += 8;

    // Datos del evaluador y evaluación
    doc.text(`Evaluador: ${evaluador?.nombre ?? ''}`, 14, y); y += 6;
    doc.text(`Fecha de Inicio: ${evaluacion.fechaInicioTest ?? ''}`, 14, y); y += 6;
    doc.text(`Fecha de Finalización: ${evaluacion.fechaFinTest ?? 'No disponible'}`, 14, y); y += 6;

    // Estado
    let estado = 'No iniciada';
    if (evaluacion.completado) estado = 'Completada';
    else if (evaluacion.iniciado) estado = 'Inconclusa';
    doc.text(`Estado: ${estado}`, 14, y); y += 6;

    // Preguntas contestadas
    doc.text(`Preguntas Contestadas: ${evaluacion.contestadas || 0} de ${evaluacion.cantidadPreguntas || 0}`, 14, y); y += 6;

    // Score por sección
    if (evaluacion.completado && evaluacion.secciones) {
      let scores = evaluacion.secciones.map(s => `${s.seccion}: ${s.score || 0}`).join(' | ');
      doc.text(`Score: ${scores}`, 14, y); y += 6;
    }

    // Tiempo transcurrido
    const minutos = Math.floor((evaluacion.tiempoTranscurrido ?? 0) / 60);
    const segundos = (evaluacion.tiempoTranscurrido ?? 0) % 60;
    
    if (evaluacion.completado && (rol === 'ADMIN' || rol === 'EVALUADOR'))
    {
      doc.text(`Tiempo Transcurrido: ${minutos}:${segundos} min`, 14, y); y += 6;
      doc.text(`Resultado AI: ${evaluacion.resultadosAI || 'Sin resultado'} - Score AI: ${evaluacion.score || 0}`, 14, y); y += 10;
    }else{
      doc.text(`Tiempo Transcurrido: ${minutos}:${segundos} min`, 14, y); y += 10;
    }

    
    // Secciones, preguntas y respuestas
    if (evaluacion.secciones) {
      evaluacion.secciones.forEach(seccion => {
        doc.setFontSize(13);
        doc.text(`Sección: ${seccion.seccion}`, 14, y);
        y += 7;

        // Tabla de preguntas y respuestas
        const body: string[][] = [];
        seccion.preguntas.forEach(pregunta => {
          // Respuestas
          let respuestas = '';
          if (pregunta.opciones) {
            pregunta.opciones.forEach(opcion => {
              if (opcion.seleccionado) {
                respuestas = `${opcion.opcion}`;
              }
            });
          }
          if (!respuestas) respuestas = 'Sin respuesta';

          body.push([
            pregunta.pregunta.replace(/<[^>]+>/g, ''), // Elimina HTML
            respuestas.trim()
          ]);
        });

        autoTable(doc, {
          head: [['Pregunta', 'Respuestas']],
          body: body,
          startY: y,
          theme: 'grid',
          headStyles: { fillColor: [41, 128, 185] },
          styles: { fontSize: 10, cellPadding: 2 },
          margin: { left: 14, right: 14 }
        });

        y = (doc as any).lastAutoTable.finalY + 8;
      });
    }
    const pdfBlob = doc.output('blob'); 
    return pdfBlob;
  }
  async descargarPdf(blob: Blob, nombre='reporte.pdf') {
      const url = URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = nombre;
      a.click();
      URL.revokeObjectURL(a.href);
  }
  
  calcularEdad(fechaNacimiento: string): number {
    const hoy = new Date();
    const nacimiento = new Date(fechaNacimiento);
    let edad = hoy.getFullYear() - nacimiento.getFullYear();
    const mes = hoy.getMonth() - nacimiento.getMonth();
    if (mes < 0 || (mes === 0 && hoy.getDate() < nacimiento.getDate())) {
      edad--;
    }
    return edad;
  }
}