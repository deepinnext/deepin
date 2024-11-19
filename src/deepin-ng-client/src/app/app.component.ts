import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LayoutService, ThemeType } from './core/services/layout.service';

@Component({
  selector: 'deepin-root',
  standalone: true,
  imports: [
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  constructor(private layoutService: LayoutService) {
    this.layoutService.theme.subscribe(res => {
      const body = document.body;
      body.className = res === ThemeType.dark ? 'dark-theme' : 'light-theme';
    });
  }
}
