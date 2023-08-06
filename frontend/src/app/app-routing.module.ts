import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TerrainPageComponent } from './Presentation/pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './Presentation/pages/terrains-page/terrains-page.component';
import { NodeDetailPageComponent } from './Presentation/pages/node-detail-page/node-detail-page.component';
import { ThoughtPageComponent } from './Presentation/pages/thought-page/thought-page.component';

const routes: Routes = [
  { path: '', component: TerrainsPageComponent },
  { path: 'terrain/:id/nodes', component: TerrainPageComponent },
  { path: 'node/:id/detail', component: NodeDetailPageComponent },
  { path: 'thought/:id', component: ThoughtPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
