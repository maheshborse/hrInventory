import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { _throw } from 'rxjs/observable/throw';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json-patch+json'})
};

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  constructor(private http:HttpClient) { }


  getPodetail():Observable<any>{
    let Url=`/Pomaster`;
    return this.http.get<any>(Url).pipe(
      catchError(this.handleError)
    );
  }

  patchRequest(EditProductModel,id):Observable<any>{
    const Url=`/Pomaster/${id}`;
    return this.http.put<any>(Url,EditProductModel,httpOptions).pipe(
      catchError(this.handleError)
    )
  }


  postRequest(SaveproductModel:any):Observable<any>{
    let Url=`/Pomaster`;
    return this.http.post<any>(Url,SaveproductModel,httpOptions).pipe(
      catchError(this.handleError)
    )
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
