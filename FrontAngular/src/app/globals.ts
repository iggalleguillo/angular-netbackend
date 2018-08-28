import { Injectable } from "@angular/core";

@Injectable()
export class Globals {
    readonly UrlApi = "http://localhost:52002/api/";
    readonly CrearJugadores = "Jugador/Crear";
    readonly CrearCarrera = "Carrera/Crear";
    readonly ObtenerCarreras = "Carrera/ObtenerCarreras";
}
