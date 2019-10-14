import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AppRoutingModule } from './core/app-routing.module';
import { AppComponent } from './app.component';
import { NewsScreenComponent } from './components/news-screen/news-screen.component';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from './core/material.module';
import { NgxLoadingModule } from 'ngx-loading';
import { StoryCardComponent } from './components/story-card/story-card.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

@NgModule({
  declarations: [
    AppComponent,
    NewsScreenComponent,
    StoryCardComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CustomMaterialModule,
    FlexLayoutModule,
    NgxLoadingModule.forRoot({}),
    InfiniteScrollModule,
  ],
  providers: [],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
