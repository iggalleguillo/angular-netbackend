import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes} from '@angular/router';
import { AppComponent } from './app.component';
import { JugadoresComponent } from './jugadores/jugadores.component';
import { CarrerasComponent } from './carreras/carreras.component';
import { CarreraComponent } from './carrera/carrera.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Globals } from './globals';

const routes: Routes = [
  { path : 'carreras', component : CarrerasComponent},
  { path : 'carrera', component : CarreraComponent},
  { path : '', redirectTo : '/carreras', pathMatch : 'full' }
];

@NgModule({
  declarations: [
    AppComponent,
    JugadoresComponent,
    CarrerasComponent,
    CarreraComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
    Globals
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
