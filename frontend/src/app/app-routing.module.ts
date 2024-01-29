import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TerrainPageComponent } from './Presentation/pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './Presentation/pages/terrains-page/terrains-page.component';
import { NodeDetailPageComponent } from './Presentation/pages/node-detail-page/node-detail-page.component';
import { FlashCardPageComponent } from './Presentation/pages/flash-card-page/flash-card-page.component';
import { FlashCardsPlayPageComponent } from './Presentation/pages/flash-cards-play-page/flash-cards-play-page.component';
import { HomeAuthorizedPageComponent } from './Presentation/pages/home-authorized-page/home-authorized-page.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  { path: 'home', component: HomeAuthorizedPageComponent },
  { path: 'terrains', component: TerrainsPageComponent },
  { path: 'terrain/:id/nodes', component: TerrainPageComponent },
  { path: 'node/:id/detail', component: NodeDetailPageComponent },
  { path: 'node/:id/flash-cards/play', component: FlashCardsPlayPageComponent },
  { path: 'node/:nodeId/flash-card/new', component: FlashCardPageComponent },
  { path: 'flash-card/:id', component: FlashCardPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
