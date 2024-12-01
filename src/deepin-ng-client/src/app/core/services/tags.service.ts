import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TagDto } from '../models/tag.model';
import { environment } from '../../../environments/environment';

const TAGS_URL = `${environment.apiUrl}/api/v1/tags`;
@Injectable({
  providedIn: 'root'
})
export class TagsService {

  constructor(
    private http: HttpClient
  ) { }

  list() {
    return this.http.get<TagDto[]>(TAGS_URL);
  }
}
