import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export enum ThemeType {
  light = 'light',
  dark = 'dark'
}

@Injectable({
  providedIn: 'root'
})
export class LayoutService {
  private _theme: BehaviorSubject<ThemeType> = new BehaviorSubject<ThemeType>(this.defaultTheme);
  public readonly theme = this._theme.asObservable();
  constructor() { }

  switchTheme() {
    const nextTheme = this._theme.value === ThemeType.dark ? ThemeType.light : ThemeType.dark;
    this._theme.next(nextTheme);
    localStorage.setItem('theme', nextTheme);
  }

  get defaultTheme() {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === ThemeType.dark) {
      return ThemeType.dark;
    }
    return ThemeType.light;
  }
}
