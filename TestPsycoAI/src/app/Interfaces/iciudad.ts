import { IProvincia } from "./iprovincia";

export interface ICiudad {
  id?: number;
  creado?: string;
  actualizado?: string;
  eliminado?: boolean;
  nombre: string;
  idProvincia: number;
  provincia?: IProvincia;
}
