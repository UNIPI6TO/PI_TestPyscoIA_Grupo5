import { Component } from '@angular/core';
import { IEvaluadores } from '../../../Interfaces/Configuraciones/ievaluadores';

@Component({
  selector: 'app-config-evaluadores',
  imports: [],
  templateUrl: './config-evaluadores.html',
  styleUrls: ['./config-evaluadores.css']
})
export class ConfigEvaluadoresComponent {
  evaluadores: IEvaluadores[] = [];
}
