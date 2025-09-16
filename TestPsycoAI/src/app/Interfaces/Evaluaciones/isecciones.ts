import { IPreguntas } from "./ipreguntas";

export interface ISecciones {
    id?: number,
    creado: Date,
    actualizado?: Date,
    eliminado: boolean,
    score: number,
    seccion: string,
    idEvaluaciones: number,
    idConfiguracionSecciones: number,
    formulaAgregado: string,
    preguntas: IPreguntas[]
}
