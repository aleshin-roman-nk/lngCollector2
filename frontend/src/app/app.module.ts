import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TerrianComponent } from './comps-ent/terrian/terrian.component';
import { TerrainPageComponent } from './pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './pages/terrains-page/terrains-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ModalComponent } from './comps-tools/modal/modal.component';
import { CreateTerrainComponent } from './comps-edit/create-terrain/create-terrain.component';
import { LoadingAnimComponent } from './comps-tools/loading-anim/loading-anim.component';
import { NodeComponent } from './comps-ent/node/node.component';
import { PlayquestComponent } from './comps-edit/playquest/playquest.component';
import { NodeDetailPageComponent } from './pages/node-detail-page/node-detail-page.component';
import { ThoughtComponent } from './comps-ent/thought/thought.component';
import { CreateThoughtComponent } from './comps-edit/create-thought/create-thought.component';
import { ConfirmationComponent } from './comps-tools/confirmation/confirmation.component';
import { ThoughtPageComponent } from './pages/thought-page/thought-page.component';
import { EditThexpressionComponent } from './comps-edit/edit-thexpression/edit-thexpression.component';
import { ElementFocusedDirective } from './directives/element-focused.directive';
import { TextInputComponent } from './comps-tools/text-input/text-input.component';


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
    ElementFocusedDirective,
    TextInputComponent
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
