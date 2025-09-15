import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-evaluacion',
  imports: [],
  templateUrl: './evaluacion.html',
  styleUrls: ['./evaluacion.css']
})
export class EvaluacionComponent implements OnInit {
constructor(private titleService: Title) {  }
  ngOnInit(): void {
    this.titleService.setTitle('Evaluación de [Aqui el Test] - PsycoAI');
  }
}
