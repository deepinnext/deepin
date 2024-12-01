import { Component } from '@angular/core';
import { EditorComponent, EditorModule, TINYMCE_SCRIPT_SRC } from '@tinymce/tinymce-angular'

@Component({
    selector: 'app-tinymce-editor',
    imports: [
        EditorModule
    ],
    templateUrl: './tinymce-editor.component.html',
    styleUrl: './tinymce-editor.component.scss',
    providers: [
        {
            provide: TINYMCE_SCRIPT_SRC,
            useValue: 'assets/lib/tinymce/tinymce.min.js'
        }
    ]
})
export class TinymceEditorComponent {
  initConfig: EditorComponent['init'] = {
    plugins: 'lists link image table code help wordcount'
  };
}
