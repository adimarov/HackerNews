import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import Story from '../commons/Story';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor(private http: HttpClient) { }

  public GET_NEWS_URL: string = environment.serverURL + 'GetLatestNews';
  public SEARCH_NEWS_URL: string = environment.serverURL + 'SearchNews';

  public storySearch(searchText: string): Observable<Story[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/x-www-form-urlencoded',
      })
    };
    return this.http.post<Story[]>(this.SEARCH_NEWS_URL, {'search_text' : searchText}, httpOptions);
  }

  public getNews(initObject: number, pageSize: number): Observable<Story[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        'story_id': initObject.toString(),
        'page_size': pageSize.toString(),
      })
    };
    return this.http.get<Story[]>(this.GET_NEWS_URL, httpOptions);
  }
}
