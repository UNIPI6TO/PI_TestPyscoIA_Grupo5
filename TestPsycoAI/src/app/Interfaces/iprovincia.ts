import { IPais } from "./ipais";

export interface IProvincia {
  id?: number;
  creado?: string;
  actualizado?: string;
  eliminado?: boolean;
  nombre: string;
  idProvincia: number;
  provincia: IPais;
}
