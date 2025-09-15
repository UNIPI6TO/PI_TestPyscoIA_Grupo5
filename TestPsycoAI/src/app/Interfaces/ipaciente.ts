import { ICiudad } from "./iciudad";

export interface IPaciente {
    id?: number;
    creado?: Date;
    actualizado?: Date;
    eliminado?: boolean;
    cedula: string;
    nombre: string;
    email?: string;
    fechaNacimiento: Date | string;
    direccion: string;
    idCiudad: number;
    ciudad?: ICiudad;
}
