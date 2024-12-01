import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CatalogComponent } from './catalog/catalog.component';
import { NavListComponent } from "./nav-list/nav-list.component";

@Component({
    selector: 'app-page',
    imports: [
        RouterOutlet,
        NavListComponent,
    ],
    templateUrl: './page.component.html',
    styleUrl: './page.component.scss'
})
export class PageComponent {

}
