import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Jugador } from '../model/jugador';
import { Globals } from '../globals';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JugadorService {

  constructor(private httpClient : HttpClient,
  private globals : Globals) { }

  crearJugador(nombre : string) : Observable<any>{
    var data = JSON.stringify({ Nombre : nombre });
    var headersOption = new HttpHeaders({'Content-Type':'application/json'});
    const httpOptions = {
      headers: headersOption
    };
    console.log(data);
    return this.httpClient.post(this.globals.UrlApi + this.globals.CrearJugadores, data, httpOptions);
  }
}
