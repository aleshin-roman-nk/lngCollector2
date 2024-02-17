import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TerrianComponent } from './Presentation/comps-ent/terrian/terrian.component';
import { TerrainPageComponent } from './Presentation/pages/terrain-page/terrain-page.component';
import { TerrainsPageComponent } from './Presentation/pages/terrains-page/terrains-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ModalComponent } from './Presentation/comps-tools/modal/modal.component';
import { EditTerrainComponent } from './Presentation/comps-edit/edit-terrain/edit-terrain.component';
import { LoadingAnimComponent } from './Presentation/comps-tools/loading-anim/loading-anim.component';
import { NodeComponent } from './Presentation/comps-ent/node-title/node-title.component';
import { FlashCardPlayItemComponent } from './Presentation/comps-play/flash-card-play-item/flash-card-play-item.component';
import { NodeDetailPageComponent } from './Presentation/pages/node-detail-page/node-detail-page.component';
import { ConfirmationComponent } from './Presentation/comps-tools/confirmation/confirmation.component';
import { FlashCardPageComponent } from './Presentation/pages/flash-card-page/flash-card-page.component';
import { EditFlashCardAnswerModalComponent } from './Presentation/comps-edit/edit-flash-card-answer/edit-flash-card-answer-modal.component';
import { ElementFocusedDirective } from './Presentation/directives/element-focused.directive';
import { TextInputComponent } from './Presentation/comps-tools/text-input-inline/text-input-inline.component';
import { TextInputOnpageComponent } from './Presentation/comps-tools/text-input-onpage/text-input-onpage.component';
import { FlashCardsPlayPageComponent } from './Presentation/pages/flash-cards-play-page/flash-cards-play-page.component';
import { CardInputComponent } from './Presentation/comps-play/card-input/card-input.component';
import { FlashCardTitleComponent } from './Presentation/comps-ent/flash-card-title/flash-card-title.component';
import { CardCheckResultComponent } from './Presentation/comps-play/card-check-result/card-check-result.component';
import { AuthInterceptor } from './Core/services/auth.interceptor';
import { GlobalErrorComponent } from './Presentation/comps-tools/global-error/global-error.component';
import { HomeAuthorizedPageComponent } from './Presentation/pages/home-authorized-page/home-authorized-page.component';
import { ModalMessageComponent } from './Presentation/comps-tools/modal-message/modal-message.component';
import { EditResearchTextComponent } from './Presentation/comps-edit/edit-research-text/edit-research-text.component';
import { FillPipe } from './Presentation/pipes/fill.pipe';
import { QuestFinishedAnimationComponent } from './Presentation/comps-play/quest-finished-animation/quest-finished-animation.component';
import { ProgressBarComponent } from './Presentation/comps-tools/progress-bar/progress-bar.component';

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
    FlashCardPlayItemComponent,
    NodeDetailPageComponent,
    ConfirmationComponent,
    FlashCardPageComponent,
    EditFlashCardAnswerModalComponent,
    ElementFocusedDirective,
    TextInputComponent,
    TextInputOnpageComponent,
    FlashCardsPlayPageComponent,
    CardInputComponent,
    FlashCardTitleComponent,
    CardCheckResultComponent,
    GlobalErrorComponent,
    HomeAuthorizedPageComponent,
    ModalMessageComponent,
    EditResearchTextComponent,
    FillPipe,
    QuestFinishedAnimationComponent,
    ProgressBarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
