import { ICiudad } from "../iciudad"

export interface IEvaluadores {
    id?: number,
    creado: Date,
    actualizado?: Date,
    eliminado: boolean,
    cedula: string,
    telefono: string,
    nombre: string,
    cargo: string,
    especialidad: string,
    email: string,
    idCiudad: number,
    ciudad?: ICiudad
}
