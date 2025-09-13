import { IConfigOpciones } from "./iconfig-opciones";

export interface IConfigPreguntas {
    id?: number;
    creado: Date;
    actualizado?: Date;
    eliminado: boolean;
    pregunta: string;
    idConfiguracionSecciones: number;
    inversa: boolean;
    opciones?: IConfigOpciones[];
}
