import { ITipoTest } from "./itipo-test";

export interface IConfigEvaluacionesResumen {
     id: number,
    creado: Date,
    actualizado?: Date,
    eliminado: boolean,
    nombre: string,
    duracion: number,
    tipoTest: ITipoTest,
    numeroSecciones?: number,
    numeroPreguntas?: number

}
