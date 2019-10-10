import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AppRoutingModule } from './core/app-routing.module';
import { AppComponent } from './app.component';
import { NewsScreenComponent } from './news-screen/news-screen.component';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from './core/material.module';

@NgModule({
  declarations: [
    AppComponent,
    NewsScreenComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CustomMaterialModule,
    FlexLayoutModule,
],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
