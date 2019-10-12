import {Component, Input, OnInit} from '@angular/core';
import Story from '../../commons/Story';
import * as moment from 'moment';

@Component({
  selector: 'app-story-card',
  templateUrl: './story-card.component.html',
  styleUrls: ['./story-card.component.scss']
})
export class StoryCardComponent implements OnInit {
  @Input() story: Story;

  public SHORTTEXTLEN = 57;
  public fullText = false;
  constructor() { }

  ngOnInit() {
  }

  public onNavigate(url: string) {
    window.open(url, '_blank');
  }

  public getText(): string {
    return this.fullText ? this.story.text : this.getShortText();
  }

  public getDate(unixDate: number) {
    return moment.unix(unixDate).format('DD-MM-YYYY');
  }

  private getShortText(): string {
    return this.story.text.length > this.SHORTTEXTLEN ? this.story.text.substr(0, this.SHORTTEXTLEN) + '...' : this.story.text;
  }

  public showFullText() {
    this.fullText = true;
  }
}
