import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TerrianComponent } from './components/terrian/terrian.component';
import { BuildingComponent } from './components/building/building.component';
import { TerrainPageComponent } from './pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './pages/terrains-page/terrains-page.component';
import { MissionComponent } from './components/mission/mission.component';
import { BuildingPageComponent } from './pages/building-page/building-page.component';
import { MissionPageComponent } from './pages/mission-page/mission-page.component';
import { DoSimpletestComponent } from './pages/working/do-simpletest/do-simpletest.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ModalComponent } from './components/modal/modal.component';
import { CreateTerrainComponent } from './components/create-terrain/create-terrain.component';
import { LoadingAnimComponent } from './components/loading-anim/loading-anim.component';

@NgModule({
  declarations: [
    AppComponent,
    TerrianComponent,
    BuildingComponent,
    TerrainPageComponent,
    TerrainsPageComponent,
    MissionComponent,
    BuildingPageComponent,
    MissionPageComponent,
    DoSimpletestComponent,
    ModalComponent,
    CreateTerrainComponent,
    LoadingAnimComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
