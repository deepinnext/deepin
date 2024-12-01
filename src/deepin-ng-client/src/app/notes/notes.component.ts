import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NotesListComponent } from "./list/list.component";

@Component({
    selector: 'app-notes',
    templateUrl: './notes.component.html',
    styleUrl: './notes.component.scss',
    standalone: true,
    imports: [
    RouterOutlet,
    NotesListComponent
]
})
export class NotesComponent {

}
