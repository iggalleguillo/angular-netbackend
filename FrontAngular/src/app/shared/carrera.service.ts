import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Globals } from '../globals';
import { Observable, Subject } from 'rxjs';
import { Jugador } from '../model/jugador';

@Injectable({
  providedIn: 'root'
})
export class CarreraService {
  ObtenerJugadores$ : Observable<any>;
  private myMethodSubject = new Subject<any>();

  constructor(private httpClient : HttpClient, 
  private globals : Globals) { 
    this.ObtenerJugadores$ = this.myMethodSubject.asObservable();
  }

  obtenerCarreras() : Observable<any>{
    return this.httpClient.get(this.globals.UrlApi + this.globals.ObtenerCarreras);
  }

  crearCarrera(jugadores : Array<{ IdJugador : number }>) : Observable<any>
  {
    var data = JSON.stringify({Participantes : jugadores});
    var headersOption = new HttpHeaders({'Content-Type':'application/json'});
    const httpOptions = {
      headers: headersOption
    };
    return this.httpClient.post(this.globals.UrlApi + this.globals.CrearCarrera, data, httpOptions);
  }

  ObtenerJugadores(jugadores : Array<Jugador>){
    this.myMethodSubject.next(jugadores);
  }
}
