import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import Story from '../api/news/contract/Story';
import {getLatestNews} from '../api/news/newsApi';

@Component({
  selector: 'app-news-screen',
  templateUrl: './news-screen.component.html',
  styleUrls: ['./news-screen.component.scss']
})
export class NewsScreenComponent implements OnInit {

  constructor(private http: HttpClient, private router: Router) {
  }

  private initObject = 0;
  private pageSize = 0;

  stories: Story[] = [];
  ngOnInit() {
    this.updateData();
  }

  public updateData() {
    const httpOptions = {
      headers: new HttpHeaders({
        'story_id': this.initObject.toString(),
        'page_size': this.pageSize.toString(),
      })
    };
    const stories: Story[] = null;
    this.http.get(`http://localhost:7071/api/GetLatestNews`, httpOptions)
      .subscribe((data: Story[]) => {
        this.stories = data;
      });
  }

  public onSearch(searchText: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/x-www-form-urlencoded',
      })
    };
    const stories: Story[] = null;
    this.http.post(`http://localhost:7071/api/SearchNews`, {'search_text' : searchText}, httpOptions)
      .subscribe((data: Story[]) => {
        this.stories = data;
      });
  }

  public onNavigate(url: string) {
    window.open(url, '_blank');
  }

}
