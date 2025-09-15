export interface IPreguntas {
    id?: number,
    creado: Date,
    actualizado?: Date,
    eliminado: boolean,
    pregunta: string,
    respuesta?: string,
    valor?: number,
    idConfiguracionPreguntas: number,
    idSecciones: number
}
