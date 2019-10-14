import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import Story from '../../commons/Story';
import {NewsService} from '../../services/news-service.service';

@Component({
  selector: 'app-news-screen',
  templateUrl: './news-screen.component.html',
  styleUrls: ['./news-screen.component.scss']
})
export class NewsScreenComponent implements OnInit {

  constructor(private http: HttpClient, private router: Router, private newsService: NewsService) {
  }

  public loading = false;
  private initObject = 0;
  private pageSize = 30;
  public isFirstPage = true;
  private pageIds: number[] = [];

  stories: Story[] = [];
  ngOnInit() {
    this.updateData();
  }

  public updateData() {
    this.loading = true;
    this.newsService.getNews(this.initObject, this.pageSize).subscribe(
      data => {
        this.stories = data;
        this.pageIds = [];
        this.isFirstPage = true;
        this.loading = false;
      }
    );
  }

  public onPrev() {
    this.loading = true;

    this.newsService.getNews(this.pageIds.pop(), this.pageSize).subscribe(
      data => {
        this.stories = data;
        if (this.pageIds.length === 0) {
          this.isFirstPage = true; }
        this.loading = false;
      }
    );
  }

  public onNext() {
    this.loading = true;
    this.pageIds.push(this.stories[0].id);

    this.newsService.getNews(this.stories[this.stories.length - 1].id, this.pageSize).subscribe(
      data => {
        this.isFirstPage = false;
        this.stories = this.stories.concat(data);
        this.loading = false;
      }
    );
  }

  public onSearch(searchText: string) {
    this.loading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/x-www-form-urlencoded',
      })
    };

    this.newsService.storySearch(searchText).subscribe(
      data => {
        this.stories = data;
        this.loading = false;
      }
    );
  }



}
