import { Component, OnInit } from '@angular/core';
import { Carrera } from '../model/carrera';
import { CarreraService } from '../shared/carrera.service';

@Component({
  selector: 'app-carreras',
  templateUrl: './carreras.component.html',
  styleUrls: ['./carreras.component.css']
})
export class CarrerasComponent implements OnInit {
  carreras : Carrera[];

  constructor(private carreraService : CarreraService) { 
  }

  ngOnInit() {
    this.carreraService.obtenerCarreras().subscribe(carr =>
    {
      this.carreras = carr;
      console.log(this.carreras);
    });
  }

}
