import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'nc-button',
  imports: [],
  templateUrl: './button.html',
  styleUrl: './button.css',
})
export class Button {
  @Input() id!: string; // the id of the button
  @Input() text: string = "Save"; // the text of the button
  @Output() onclick = new EventEmitter();

}
