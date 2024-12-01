import { Component } from '@angular/core';
import { NoteDto, NoteQuery } from '../../core/models/note.model';
import { PagedResult } from '../../core/models/pagination.model';
import { NotesService } from '../../core/services/notes.service';
import { MatList, MatListItem } from '@angular/material/list';

@Component({
  selector: 'app-notes-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss',
  imports: [
    MatList,
    MatListItem
  ]
})
export class NotesListComponent {
  title = 'Untitled Note';
  query: NoteQuery = {
    pageIndex: 0,
    pageSize: 20
  };
  result: PagedResult<NoteDto> = new PagedResult<NoteDto>();

  constructor(private notesService: NotesService) {
    this.notesService.title$.subscribe(title => {
      this.title = title;
    });
  }

  ngOnInit(): void {
    this.onQueryChange(this.query);
  }

  onQueryChange(query: NoteQuery) {
    this.query = query;
    this.notesService.search(this.query)
      .subscribe(result => {
        this.result = result;
      });
  }
}
