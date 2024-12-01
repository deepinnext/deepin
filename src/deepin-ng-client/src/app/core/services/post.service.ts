import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { PostDto, PostPagedQuery } from '../models/post.model';
import { PagedResult } from '../models/pagination.model';

const POSTS_URL = `${environment.apiUrl}/api/v1/posts`;
@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(
    private http: HttpClient
  ) { }

  searchPosts(query: PostPagedQuery) {
    if (!query.search) {
      delete query.search;
    }
    if (!query.postStatus) {
      delete query.postStatus;
    }
    if (!query.isDeleted) {
      delete query.isDeleted;
    }
    if (!query.categoryId) {
      delete query.categoryId;
    }
    return this.http.get<PagedResult<PostDto>>(POSTS_URL, {
      params: query as any
    });
  }
}
