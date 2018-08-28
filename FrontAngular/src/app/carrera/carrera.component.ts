import { Component, OnInit } from '@angular/core';
import { Carrera } from '../model/carrera';
import { CarreraService } from '../shared/carrera.service';
import { Jugador } from '../model/jugador';

@Component({
  selector: 'app-carrera',
  templateUrl: './carrera.component.html',
  styleUrls: ['./carrera.component.css']
})
export class CarreraComponent implements OnInit {
  public show : boolean = true;
  public carrera : Carrera;
  public arrayAux : Array<{ IdJugador : number }> = [];
  timeOut : number = 100000;
  rt: number;
  jugadores : Array<Jugador> = [];
  public timerContador;
  public timerCrearCarrera;

  
  constructor(private carreraService : CarreraService) {
    this.carreraService.ObtenerJugadores$.subscribe((data) =>{
      this.jugadores = data;
    });
   }

  ngOnInit() {
  }

  comenzarCarrera() : void
  {
    this.arrayAux = [];
    this.jugadores.forEach(jugador =>{
        this.arrayAux.push({ IdJugador : jugador.IdJugador });
    });
    this.carreraService.crearCarrera(this.arrayAux).subscribe((data)=>{
      this.carrera = data;
    });
    this.show = false;

    this.remainTime(this.timeOut);
    this.timerCrearCarrera = setTimeout(() => this.comenzarCarrera(), this.timeOut);
    
  }

  remainTime(timeOut : number) : void
  {
    this.rt = (timeOut - 1000) / 1000;
    timeOut -= 1000;
    console.log(this.rt);
    if (this.rt > 0) {
      this.timerContador = setTimeout(() => this.remainTime(timeOut), 1000);
    }
    else {
      this.rt = 0;
    }
  }

  volver(){
    clearTimeout(this.timerContador);
    clearTimeout(this.timerCrearCarrera);
  }
}
