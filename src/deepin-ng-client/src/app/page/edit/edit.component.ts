import { Component, OnInit } from '@angular/core';
import { TinymceEditorComponent } from '../../shared/components/tinymce-editor/tinymce-editor.component';
import { MatFormField, MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatChipsModule, MatChipInputEvent } from '@angular/material/chips';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { NgIf } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButton, MatIconButton } from '@angular/material/button';
import { ActivatedRoute } from '@angular/router';
import { MatDivider } from '@angular/material/divider';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-edit',
  standalone: true,
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.scss',
  imports: [
    NgIf,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatLabel,
    MatFormField,
    MatInput,
    MatChipsModule,
    MatIcon,
    MatCardModule,
    MatButton,
    MatIconButton,
    MatDivider,
    MatMenuModule,
    TinymceEditorComponent
  ],
})
export class EditComponent implements OnInit {
  form?: FormGroup;
  tags: string[] = ['angular', 'typescript', 'javascript', 'html', 'css', 'scss', 'sass', 'less'];
  isNew!: boolean;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute) {
    this.isNew = this.route.snapshot.routeConfig?.path === 'new';
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      title: this.fb.control('', [Validators.required]),
      description: this.fb.control('', [Validators.maxLength(200)]),
      content: this.fb.control('', [Validators.required]),
      tags: this.fb.control(this.tags),
      email: this.fb.control('', [Validators.required, Validators.email]),
      password: this.fb.control('', [Validators.required, Validators.minLength(8)])
    });
  }

  removeTag(tag: string) {
    this.tags = this.tags.filter(t => t !== tag);
  }

  addTag(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our keyword
    if (value && !this.tags.includes(value)) {
      this.tags.push(value);
    }

    // Clear the input value
    event.chipInput!.clear();
  }
}
