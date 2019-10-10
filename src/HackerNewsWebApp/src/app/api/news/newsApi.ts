
import Story from './contract/Story';
import {Pager} from '../base/Pager';
import {HttpClient, HttpHeaders} from '@angular/common/http';

export function getLatestNews(
  pager: Pager, http: HttpClient): Story[] {

  const httpOptions = {
    headers: new HttpHeaders({
      'story_id': pager.id.toString(),
      'page_size': pager.size.toString(),
    })
  };

  const stories: Story[] = null;

  http.get(`http://localhost:7071/api/GetLatestNews`, httpOptions)
    .subscribe((data) => {
      this.stories = data;
    });

  return stories;
}
