import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TerrianComponent } from './Presentation/comps-ent/terrian/terrian.component';
import { TerrainPageComponent } from './Presentation/pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './Presentation/pages/terrains-page/terrains-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ModalComponent } from './Presentation/comps-tools/modal/modal.component';
import { CreateTerrainComponent } from './Presentation/comps-edit/create-terrain/create-terrain.component';
import { LoadingAnimComponent } from './Presentation/comps-tools/loading-anim/loading-anim.component';
import { NodeComponent } from './Presentation/comps-ent/node/node.component';
import { PlayquestComponent } from './Presentation/comps-play/playquest/playquest.component';
import { NodeDetailPageComponent } from './Presentation/pages/node-detail-page/node-detail-page.component';
import { ThoughtComponent } from './Presentation/comps-ent/thought/thought.component';
import { CreateThoughtComponent } from './Presentation/comps-edit/create-thought/create-thought.component';
import { ConfirmationComponent } from './Presentation/comps-tools/confirmation/confirmation.component';
import { ThoughtPageComponent } from './Presentation/pages/thought-page/thought-page.component';
import { EditThexpressionComponent } from './Presentation/comps-edit/edit-thexpression/edit-thexpression.component';
import { ElementFocusedDirective } from './Presentation/directives/element-focused.directive';
import { TextInputComponent } from './Presentation/comps-tools/text-input/text-input.component';


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
