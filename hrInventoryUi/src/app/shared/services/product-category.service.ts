import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoryService {
  connect(): category[] {
    throw new Error("Method not implemented.");
  }

  

  constructor(private http:HttpClient) {}

 

 
}
