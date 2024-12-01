import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { QuillModule } from 'ngx-quill'
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { NgIf } from '@angular/common';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule, MatLabel, MatFormField } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { NotesService } from '../../core/services/notes.service';
import { NoteDto } from '../../core/models/note.model';

@Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrl: './edit.component.scss',
    imports: [
        NgIf,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatLabel,
        MatFormField,
        MatChipsModule,
        MatIcon,
        MatIconButton,
        MatCardModule,
        MatButton,
        MatIconButton,
        MatMenuModule,
        QuillModule
    ]
})
export class EditComponent {
    form?: FormGroup;
    tags: string[] = [];
    isNew!: boolean;
    isLoading = false;
    categoryId: number = 0;
    constructor(
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private notesService: NotesService) {
        this.isNew = this.route.snapshot.routeConfig?.path === 'new';
        if (this.route.snapshot.queryParamMap.has('cid')) {
            this.categoryId = Number(this.route.snapshot.queryParamMap.get('cid'));
        }
    }

    ngOnInit(): void {
        if (this.isNew) {
            this.buildForm();
        } else {
            const id: number = this.route.snapshot.params['id'];
            this.notesService.get(id)
                .subscribe(note => {
                    this.buildForm(note);
                    this.tags = note.tags.map(s => s.name);
                });
        }
    }


    onLockChange() {
        this.form?.get('isPublic')?.setValue(!this.form?.get('isPublic')?.value);
    }

    onTitleChange(event: Event) {
        const title = (event.target as HTMLHeadingElement).innerText;
        this.form?.get('title')?.setValue(title);
        this.notesService.setTitle(title);
    }

    onDescriptionChange(event: Event) {
        const description = (event.target as HTMLParagraphElement).innerText;
        this.form?.get('description')?.setValue(description);
    }

    buildForm(note?: NoteDto) {
        this.form = this.fb.group({
            categoryId: this.fb.control(note?.categoryId || this.categoryId),
            title: this.fb.control(note?.title || '', [Validators.maxLength(50)]),
            description: this.fb.control(note?.description || '', [Validators.maxLength(200)]),
            content: this.fb.control(note?.content || ''),
            tags: this.fb.control(note?.tags || []),
            status: this.fb.control(note?.status || 'draft'),
            isPublic: this.fb.control(note?.isPublic || false),
        });
    }

    removeTag(tag: string) {
        this.tags = this.tags.filter(t => t !== tag);
    }

    addTag(event: MatChipInputEvent): void {
        const value = (event.value || '').trim();
        if (value && !this.tags.includes(value)) {
            this.tags.push(value);
        }
        event.chipInput!.clear();
    }

    onPublish() {
        this.form?.get('status')?.setValue('published');
        this.onSubmit();
    }

    onSubmit() {
        console.debug(this.form?.value);
        if (this.form?.invalid || this.isLoading) {
            return;
        }
        this.isLoading = true;
        this.notesService.create(this.form?.value)
            .subscribe({
                next: note => {
                    console.debug('Note created:', note);
                },
                error: err => {
                    console.error('Failed to create note:', err);
                },
                complete: () => {
                    console.debug('Create note completed');
                    this.isLoading = false;
                }
            });
    }
}
