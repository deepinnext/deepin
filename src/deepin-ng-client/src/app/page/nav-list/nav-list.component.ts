import { Component, OnInit } from '@angular/core';
import { PostService } from '../../core/services/post.service';
import { PagedResult } from '../../core/models/pagination.model';
import { PostDto, PostPagedQuery } from '../../core/models/post.model';
import { MatList, MatListItem } from '@angular/material/list';

@Component({
  selector: 'deepin-nav-list',
  standalone: true,
  templateUrl: './nav-list.component.html',
  styleUrl: './nav-list.component.scss',
  imports: [
    MatList,
    MatListItem
  ],
})
export class NavListComponent implements OnInit {
  query: PostPagedQuery = {
    pageIndex: 1,
    pageSize: 20
  };
  result: PagedResult<PostDto> = new PagedResult<PostDto>();

  constructor(private postService: PostService) { }

  ngOnInit(): void {
    this.onQueryChange(this.query);
  }

  onQueryChange(query: PostPagedQuery) {
    this.query = query;
    this.postService.searchPosts(this.query).subscribe(result => {
      this.result = result;
    });
  }
}
