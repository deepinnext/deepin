import { Component } from '@angular/core';
import { MatList, MatListItem } from '@angular/material/list';

@Component({
  selector: 'deepin-catalog',
  standalone: true,
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.scss',
  imports: [
    MatList,
    MatListItem
  ],
})
export class CatalogComponent {
  list: { name: string, id: number }[] =  [
    { name: 'Quick note', id: 1 },
    { name: 'Drafts', id: 2 },
    { name: 'Todo List', id: 3 },
    { name: 'Reading List', id: 4 },
    { name: 'Archive', id: 5 },
    { name: 'Trash', id: 6 },
    { name: 'Quick note', id: 7 },
    { name: 'Drafts', id: 8 },
    { name: 'Todo List', id: 9 },
    { name: 'Reading List', id: 10 },
    { name: 'Archive', id: 11 },
    { name: 'Trash', id: 12 },
    { name: 'Quick note', id: 13 },
    { name: 'Drafts', id: 14 },
    { name: 'Todo List', id: 15 },
    { name: 'Reading List', id: 16 },
    { name: 'Archive', id: 17 },
    { name: 'Trash', id: 18 },
    { name: 'Quick note', id: 1 },
    { name: 'Drafts', id: 2 },
    { name: 'Todo List', id: 3 },
    { name: 'Reading List', id: 4 },
    { name: 'Archive', id: 5 },
    { name: 'Trash', id: 6 },
    { name: 'Quick note', id: 7 },
    { name: 'Drafts', id: 8 },
    { name: 'Todo List', id: 9 },
    { name: 'Reading List', id: 10 },
    { name: 'Archive', id: 11 },
    { name: 'Trash', id: 12 },
    { name: 'Quick note', id: 13 },
    { name: 'Drafts', id: 14 },
    { name: 'Todo List', id: 15 },
    { name: 'Reading List', id: 16 },
    { name: 'Archive', id: 17 },
    { name: 'Trash', id: 18 }
  ]
}
