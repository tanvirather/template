import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'nc-card',
  imports: [],
  templateUrl: './card.html',
  styleUrl: './card.css',
})
export class Card {
  @Input() header: string = "";
  @Input() reset: boolean = false;
  @Input() help: boolean = false;
  @Input() history: boolean = false;
  @Input() submit: string = "";
  @Output() resetClick = new EventEmitter();
  @Output() helpClick = new EventEmitter();
  @Output() historyClick = new EventEmitter();
  @Output() submitClick = new EventEmitter();
  cardId = () => "card-" + this.header.replace(" ", "");
}
