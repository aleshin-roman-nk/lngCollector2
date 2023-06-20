import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TerrainPageComponent } from './pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './pages/terrains-page/terrains-page.component';
import { NodeDetailPageComponent } from './pages/node-detail-page/node-detail-page.component';

const routes: Routes = [
  { path: '', component: TerrainsPageComponent },
  { path: 'terrain/:id/nodes', component: TerrainPageComponent },
  { path: 'node/:id/detail', component: NodeDetailPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
