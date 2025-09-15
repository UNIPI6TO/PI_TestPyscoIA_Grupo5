export interface IOpciones {
    id?: number,
    creado: Date,
    actualizado?: Date,
    eliminado: boolean,
    orden: number,
    opcion: string,
    peso: number,
    seleccionado: boolean,
    idPreguntas: number
}
