import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  input,
  model,
  Signal,
} from '@angular/core';

@Component({
  selector: 'ui-text',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './text.html',
  styleUrls: ['./text.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TextComponent {
  /** Two-way bindable value: [(value)]="..." */
  value = model<string>('');

  /** Inputs as signals (Angular v17+) */
  label = input<string | null>(null);
  hint = input<string | null>(null);
  error = input<string | null>(null);
  placeholder = input<string | null>(null);

  variant = input<'filled' | 'outline'>('filled');
  size = input<'sm' | 'md' | 'lg'>('md');

  required = input(false);
  disabled = input(false);
  readonly = input(false);
  maxlength = input<number | null>(null);

  /** Basic validation: required */
  showError: Signal<boolean> = computed(() => {
    if (this.disabled()) return false;
    if (this.error()) return true;
    if (this.required()) {
      return !this.value() || this.value().trim().length === 0;
    }
    return false;
  });

  errorText: Signal<string> = computed(() =>
    this.error() ??
    (this.required() && this.showError() ? 'This field is required.' : '')
  );

  /** Keep the model in sync on input */
  onInput(v: string) {
    this.value.set(v);
  }

  // Example: side-effect/logging (optional)
  constructor() {
    effect(() => {
      // console.log('UiText value:', this.value());
    });
  }
}
