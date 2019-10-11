import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {NewsScreenComponent} from '../components/news-screen/news-screen.component';

const routes: Routes = [
  {path: 'news-screen', component: NewsScreenComponent},
  {path: '', component: NewsScreenComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
