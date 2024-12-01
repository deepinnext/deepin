import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateNoteCommand, NoteDto, NoteQuery } from '../models/note.model';
import { PagedResult } from '../models/pagination.model';
import { environment } from '../../../environments/environment';
import { Subject } from 'rxjs';

const NOTES_URL = `${environment.apiUrl}/api/v1/notes`;
@Injectable({
  providedIn: 'root'
})
export class NotesService {
  private titleSubject = new Subject<string>();
  title$ = this.titleSubject.asObservable();

  constructor(
    private http: HttpClient
  ) { }

  setTitle(title: string) {
    this.titleSubject.next(title);
  }

  create(command: CreateNoteCommand) {
    return this.http.post<NoteDto>(NOTES_URL, command);
  }

  get(id: number) {
    return this.http.get<NoteDto>(`${NOTES_URL}/${id}`);
  }

  search(query: NoteQuery) {
    if (!query.search) {
      delete query.search;
    }
    if (!query.status) {
      delete query.status;
    }
    if (!query.isDeleted) {
      delete query.isDeleted;
    }
    if (!query.categoryId) {
      delete query.categoryId;
    }
    return this.http.get<PagedResult<NoteDto>>(NOTES_URL, {
      params: query as any
    });
  }
}
