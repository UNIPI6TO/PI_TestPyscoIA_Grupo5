import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-test',
  imports: [],
  templateUrl: './test.html',
  styleUrls: ['./test.css']
})
export class TestComponent implements OnInit {
constructor(private titleService: Title) {  }
  ngOnInit(): void {
    this.titleService.setTitle('Evaluación de [Aqui el Test] - PsycoAI');
  }
}
