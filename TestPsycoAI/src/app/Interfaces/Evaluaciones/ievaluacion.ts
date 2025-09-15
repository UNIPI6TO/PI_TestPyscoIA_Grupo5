import { ITipoTest } from "../Configuraciones/itipo-test"

export interface IEvaluacion {
    id?: number,
    creado: Date,
    actualizado?: Date,
    eliminado: boolean,
    cantidadPreguntas: number,
    idConfiguracionTest: number,
    idEvaluador: number,
    idPaciente: number,
    duracion: number,
    contestadas?: number,
    noContestadas?: number,
    completado?: boolean,
    iniciado?: boolean,
    tiempoTranscurrido?: number,
    fechaInicioTest?: Date,
    fechaFinTest?: Date,
    evaluacion: String,
    tipoTest?: ITipoTest
}
