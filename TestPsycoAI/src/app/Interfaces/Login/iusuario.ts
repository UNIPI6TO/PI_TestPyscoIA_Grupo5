export interface IUsuario {
    id: number,
    usuario: string,
    rol: string,
    idEvaluador?: number | null,
    idPaciente?: number | null,
    creado: Date,
    actualizado?: Date | null,
    eliminado: boolean,
    password?: string,
}
export interface IInicioSesion {
    usuario: string;
    password: string;
}
