import { Component } from '@angular/core';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { MatSidenavContainer, MatSidenav, MatSidenavContent } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { MatDivider } from '@angular/material/divider';
import { TopbarComponent } from "../topbar/topbar.component";

@Component({
    selector: 'deepin-layout',
    templateUrl: './layout.component.html',
    styleUrl: './layout.component.scss',
    imports: [
        RouterOutlet,
        MatSidenavContainer,
        MatSidenav,
        MatSidenavContent,
        MatDivider,
        SidebarComponent,
        TopbarComponent
    ]
})
export class LayoutComponent {

}
