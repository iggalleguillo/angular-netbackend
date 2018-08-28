import { Component, OnInit } from '@angular/core';
import { Jugador } from '../model/jugador';
import { CarreraService } from '../shared/carrera.service';
import { JugadorService } from '../shared/jugador.service';

@Component({
  selector: 'app-jugadores',
  templateUrl: './jugadores.component.html',
  styleUrls: ['./jugadores.component.css']
})
export class JugadoresComponent implements OnInit {
  jugadores : Array<Jugador> = [];
  public jugador : Jugador;

  constructor(private carreraService : CarreraService,
  private jugadorService : JugadorService) {
    
   }

  ngOnInit() {
  }

  agregarJugador(nombre : string){
    this.jugador = new Jugador();
    this.jugador.Nombre = nombre;
    this.jugadorService.crearJugador(this.jugador.Nombre).subscribe((data)=>{
      console.log(data["IdJugador"]);
      this.jugador.IdJugador = data["IdJugador"];
    });
    console.log(this.jugador);
    this.jugadores.push(this.jugador);
    this.carreraService.ObtenerJugadores(this.jugadores);
  }
}
