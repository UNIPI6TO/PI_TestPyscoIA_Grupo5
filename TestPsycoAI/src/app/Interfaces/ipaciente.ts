import { ICiudad } from "./iciudad";

export interface IPaciente {
    id?: number;
    creado?: string;
    actualizado?: string;
    eliminado?: boolean;
    cedula: string;
    nombre: string;
    email?: string;
    fechaNacimiento: string;
    direccion: string;
    idCiudad: number;
    ciudad?: ICiudad;
}
