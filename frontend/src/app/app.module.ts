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
import { EditTerrainComponent } from './Presentation/comps-edit/edit-terrain/edit-terrain.component';
import { LoadingAnimComponent } from './Presentation/comps-tools/loading-anim/loading-anim.component';
import { NodeComponent } from './Presentation/comps-ent/node/node.component';
import { FlashCardComponent } from './Presentation/comps-play/flash-card/flash-card.component';
import { NodeDetailPageComponent } from './Presentation/pages/node-detail-page/node-detail-page.component';
import { ThoughtComponent } from './Presentation/comps-ent/thought/thought.component';
import { EditThoughtComponent } from './Presentation/comps-edit/edit-thought/edit-thought.component';
import { ConfirmationComponent } from './Presentation/comps-tools/confirmation/confirmation.component';
import { ThoughtPageComponent } from './Presentation/pages/thought-page/thought-page.component';
import { EditThexpressionComponent } from './Presentation/comps-edit/edit-thexpression/edit-thexpression.component';
import { ElementFocusedDirective } from './Presentation/directives/element-focused.directive';
import { TextInputComponent } from './Presentation/comps-tools/text-input-inline/text-input-inline.component';
import { TextInputOnpageComponent } from './Presentation/comps-tools/text-input-onpage/text-input-onpage.component';
import { QuestionWorkPageComponent } from './Presentation/pages/question-work-page/question-work-page.component';
import { CardInputComponent } from './Presentation/comps-play/card-input/card-input.component';


@NgModule({
  declarations: [
    AppComponent,
    TerrianComponent,
    TerrainPageComponent,
    TerrainsPageComponent,
    ModalComponent,
    EditTerrainComponent,
    LoadingAnimComponent,
    NodeComponent,
    FlashCardComponent,
    NodeDetailPageComponent,
    ThoughtComponent,
    EditThoughtComponent,
    ConfirmationComponent,
    ThoughtPageComponent,
    EditThexpressionComponent,
    ElementFocusedDirective,
    TextInputComponent,
    TextInputOnpageComponent,
    QuestionWorkPageComponent,
    CardInputComponent
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
