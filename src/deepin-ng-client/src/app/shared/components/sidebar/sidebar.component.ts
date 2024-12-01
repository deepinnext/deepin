import { Component, OnInit } from '@angular/core';
import { MatButton, MatFabButton, MatIconButton, MatMiniFabButton } from '@angular/material/button';
import { MatDivider } from '@angular/material/divider';
import { MatIcon } from '@angular/material/icon';
import { MatNavList, MatListItem, MatListModule } from '@angular/material/list';
import { MatToolbar } from '@angular/material/toolbar';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CategoryService } from '../../../core/services/category.service';
import { CategoryDto } from '../../../core/models/category.model';

@Component({
    selector: 'deepin-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrl: './sidebar.component.scss',
    imports: [
        RouterLink,
        RouterLinkActive,
        MatIcon,
        MatDivider,
        MatListModule,
        MatMiniFabButton,
        MatToolbar,
        MatIconButton
    ]
})
export class SidebarComponent implements OnInit {
  categories: CategoryDto[] = [];
  constructor(private router: Router, private categoryService: CategoryService) {

  }
  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  isActived(route: string): boolean {
    return this.router.isActive(route, { paths: 'subset', queryParams: 'exact', fragment: 'ignored', matrixParams: 'ignored' });
  }
}
