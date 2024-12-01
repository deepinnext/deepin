import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CategoryDto } from '../models/category.model';

const CATEGORIES_URL = `${environment.apiUrl}/api/v1/categories`;

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(
    private http: HttpClient
  ) { }

  getCategories() {
    return this.http.get<CategoryDto[]>(CATEGORIES_URL);
  }
}
