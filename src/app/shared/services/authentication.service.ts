import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { _throw } from 'rxjs/observable/throw';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Login } from '../models/login';
import { of } from 'rxjs/observable/of';
import { Router } from '@angular/router';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json-patch+json'})
  };

@Injectable()
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  login(loginModel:any):Observable<any>{
    localStorage.setItem("loginModel",JSON.stringify(loginModel));
    let Url=`/Authenticate`;
    return this.http.post<any>(Url,loginModel,httpOptions).pipe(
        catchError(this.handleError)
    );
  }

  loginByApp(token:any):Observable<any>{
    let Url=`/ValidateToken`;
    return this.http.post<any>(Url,{accessToken:token},httpOptions).pipe(
        catchError(this.handleError)
    );
  }

  logout(){
    //this.router.navigate([`/logout`]);
  }

  getRefreshToken(refreshToken:string):Observable<any>{
    let model=JSON.parse(localStorage.getItem("user"));
    let tokenModel={refreshToken:refreshToken, accessToken:model.access_Token};
    let Url=`/RefreshToken`;
    return this.http.post<any>(Url,tokenModel,httpOptions).pipe(
        catchError(this.handleError)
    );
    //return of();
  }

  checkIDPEnabled(orgId:string):Observable<any>{
    let Url=`/defaults/IsIDPLoginEnbaled?organizationId=${orgId}`;
    return this.http.get<any>(Url,httpOptions).pipe(
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