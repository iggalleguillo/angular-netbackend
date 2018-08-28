import { Jugador } from "./jugador";
import { Caballo } from "./caballo";

export class Carrera {
    IdCarrera : number
    NombreCarrera : string
    Participantes : Jugador[]
    Caballo : Caballo
    Ganadores : Jugador []
}
