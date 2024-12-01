import { Component } from '@angular/core';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatFormField, MatInput } from '@angular/material/input';
import { MatToolbar } from '@angular/material/toolbar';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatIcon } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { LayoutService, ThemeType } from '../../../core/services/layout.service';

@Component({
  selector: 'deepin-topbar',
  standalone: true,
  imports: [
    MatToolbar,
    MatInput,
    MatIcon,
    MatIconButton,
    MatButton,
    MatMenu,
    MatMenuTrigger,
    MatMenuItem,
    MatSlideToggleModule,
    MatFormField
  ],
  templateUrl: './topbar.component.html',
  styleUrl: './topbar.component.scss'
})
export class TopbarComponent {
  isLightTheme = true;
  constructor(
    private layoutService: LayoutService,
  ) {
    this.layoutService.theme.subscribe(res => {
      this.isLightTheme = res === ThemeType.light;
    });
  }


  toggleTheme() {
    this.layoutService.switchTheme();
  }
}
