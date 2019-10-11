import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import Story from '../../commons/Story';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-news-screen',
  templateUrl: './news-screen.component.html',
  styleUrls: ['./news-screen.component.scss']
})
export class NewsScreenComponent implements OnInit {

  constructor(private http: HttpClient, private router: Router) {
  }

  public loading = false;
  private initObject = 0;
  private pageSize = 30;
  public isFirstPage = true;
  private pageIds: number[] = [];
  public GET_NEWS_URL: string = environment.serverURL + 'GetLatestNews';
  public SEARCH_NEWS_URL: string = environment.serverURL + 'SearchNews';

  stories: Story[] = [];
  ngOnInit() {
    this.updateData();
  }

  public updateData() {
    this.loading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'story_id': this.initObject.toString(),
        'page_size': this.pageSize.toString(),
      })
    };
    this.http.get(this.GET_NEWS_URL, httpOptions)
      .subscribe((data: Story[]) => {
        this.stories = data;
        this.loading = false;
      });
  }

  public onPrev() {
    this.loading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'story_id': this.pageIds.pop().toString(),
        'page_size': this.pageSize.toString(),
      })
    };
    this.http.get(this.GET_NEWS_URL, httpOptions)
      .subscribe((data: Story[]) => {
        this.stories = data;
        if (this.pageIds.length === 0) {
          this.isFirstPage = true; }
        this.loading = false;
      });
  }

  public onNext() {
    this.loading = true;
    this.pageIds.push(this.stories[0].id);
    const httpOptions = {
      headers: new HttpHeaders({
        'story_id': this.stories[this.stories.length - 1].id.toString(),
        'page_size': this.pageSize.toString(),
      })
    };
    this.http.get(this.GET_NEWS_URL, httpOptions)
      .subscribe((data: Story[]) => {
        this.isFirstPage = false;
        this.stories = data;
        this.loading = false;
      });
  }

  public onSearch(searchText: string) {
    this.loading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/x-www-form-urlencoded',
      })
    };
    this.http.post(this.SEARCH_NEWS_URL, {'search_text' : searchText}, httpOptions)
      .subscribe((data: Story[]) => {
        this.stories = data;
        this.loading = false;
      });
  }

  public onNavigate(url: string) {
    window.open(url, '_blank');
  }

}
