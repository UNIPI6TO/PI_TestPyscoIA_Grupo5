import { IConfigEvaluaciones } from "./iconfig-evaluaciones";
import { IConfigPreguntas } from "./iconfig-preguntas";

export interface IConfigSecciones {
    id?: number;
    creado: Date,
    actualizado?: Date,
    eliminado: boolean,
    seccion: string,
    numeroPreguntas: number,
    idConfiguracionesTest: number,
    formulaAgregado: string,
    configuracionTest?: IConfigEvaluaciones,
    bancoPreguntas?: IConfigPreguntas[]
}
