import { CommonModule } from '@angular/common';
import { Component, computed, model, signal } from '@angular/core';
import { Components } from '../../components';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ...Components],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  // Local state as signals
  firstName = signal('');
  lastName = signal('');
  title = signal('');
  readonlyExample = signal('Static value');
  value = model<string>('');

  fullName = computed(() => {
    const f = this.firstName().trim();
    const l = this.lastName().trim();
    return [f, l].filter(Boolean).join(' ') || 'No name';
    // Template reads fullName() and re-renders as dependencies change
  });

  onSubmit() {
    alert(`Hello, ${this.fullName()}! Your title is "${this.title()}" and value is "${this.value()}"`);
  }

  reset() {
    this.firstName.set('');
    this.lastName.set('');
    this.title.set('');
  }

}
