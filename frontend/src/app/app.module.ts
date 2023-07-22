import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TerrianComponent } from './components/terrian/terrian.component';
import { TerrainPageComponent } from './pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './pages/terrains-page/terrains-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ModalComponent } from './components/modal/modal.component';
import { CreateTerrainComponent } from './components/create-terrain/create-terrain.component';
import { LoadingAnimComponent } from './components/loading-anim/loading-anim.component';
import { NodeComponent } from './components/node/node.component';
import { PlayquestComponent } from './components/playquest/playquest.component';
import { NodeDetailPageComponent } from './pages/node-detail-page/node-detail-page.component';
import { ThoughtComponent } from './components/thought/thought.component';
import { CreateThoughtComponent } from './components/create-thought/create-thought.component';
import { ConfirmationComponent } from './components/confirmation/confirmation.component';
import { ThoughtPageComponent } from './pages/thought-page/thought-page.component';
import { EditThexpressionComponent } from './components/edit-thexpression/edit-thexpression.component';
import { ElementFocusedDirective } from './directives/element-focused.directive';


@NgModule({
  declarations: [
    AppComponent,
    TerrianComponent,
    TerrainPageComponent,
    TerrainsPageComponent,
    ModalComponent,
    CreateTerrainComponent,
    LoadingAnimComponent,
    NodeComponent,
    PlayquestComponent,
    NodeDetailPageComponent,
    ThoughtComponent,
    CreateThoughtComponent,
    ConfirmationComponent,
    ThoughtPageComponent,
    EditThexpressionComponent,
    ElementFocusedDirective
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
