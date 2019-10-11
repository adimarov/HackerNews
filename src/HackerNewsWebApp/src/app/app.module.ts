import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AppRoutingModule } from './core/app-routing.module';
import { AppComponent } from './app.component';
import { NewsScreenComponent } from './components/news-screen/news-screen.component';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from './core/material.module';
import { NgxLoadingModule } from 'ngx-loading';

@NgModule({
  declarations: [
    AppComponent,
    NewsScreenComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CustomMaterialModule,
    FlexLayoutModule,
    NgxLoadingModule.forRoot({}),
  ],
  providers: [],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
