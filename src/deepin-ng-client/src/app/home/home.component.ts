import { Component } from '@angular/core';
import { LayoutComponent } from '../shared/components/layout/layout.component';
import { TinymceEditorComponent } from '../shared/components/tinymce-editor/tinymce-editor.component';
@Component({
    selector: 'app-home',
    imports: [LayoutComponent, TinymceEditorComponent],
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss'
})
export class HomeComponent {
}
