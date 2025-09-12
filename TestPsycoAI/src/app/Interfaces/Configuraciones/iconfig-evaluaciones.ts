import { ITipoTest } from "./itipo-test";

export interface IConfigEvaluaciones {
    id?: number,
    creado: Date,
    actualizado?: Date,
    eliminado: boolean,
    nombre: string,
    idTipoTest?: number,
    duracion: number,
    tipoTest?: ITipoTest    
}
