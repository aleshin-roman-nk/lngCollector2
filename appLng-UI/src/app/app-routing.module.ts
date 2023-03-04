import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingComponent } from './components/building/building.component';
import { BuildingPageComponent } from './pages/building-page/building-page.component';
import { MissionPageComponent } from './pages/mission-page/mission-page.component';
import { TerrainPageComponent } from './pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './pages/terrains-page/terrains-page.component';
import { DoSimpletestComponent } from './pages/working/do-simpletest/do-simpletest.component';

const routes: Routes = [
  { path: '', component: TerrainsPageComponent },
  { path: 'terrain/:id', component: TerrainPageComponent },
  { path: 'building/:id', component: BuildingPageComponent },
  { path: 'mission/:id', component: MissionPageComponent },
  { path: 'go-simpletest/:id', component: DoSimpletestComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
