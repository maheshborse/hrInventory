import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { category } from '../models/category';
import { catchError } from 'rxjs/operators';
import { _throw } from 'rxjs/observable/throw';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json-patch+json'})
};

@Injectable({
  providedIn: 'root'
})

export class ProductCategoryService {
  
  constructor(private http:HttpClient) {}

  getCategory():Observable<any>{
    let Url=`/Catagory`;
    return this.http.get<any>(Url).pipe(
      catchError(this.handleError)
    );
  }

  postRequest(SavecategoryModel:any):Observable<any>{
    let Url=`/Catagory`;
    return this.http.post<any>(Url,SavecategoryModel,httpOptions).pipe(
      catchError(this.handleError)
    )
  }


  patchRequest(categoryModel):Observable<any>{
    const Url=`/Catagory/${categoryModel.categoryid}`;
    return this.http.put<any>(Url,categoryModel,httpOptions).pipe(
      catchError(this.handleError)
    )
  }

  deleteRequest(id:number):Observable<any>{
    debugger;
    let Url=`/Catagory/${id}`;
    return this.http.delete<any>(Url).pipe(
        catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return _throw(
      error);
  };


 
}
