import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor,HttpResponse,HttpHeaderResponse } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { environment } from '../../../environments/environment';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { switchMap, catchError, finalize, filter, take } from 'rxjs/operators';
 
@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    private refreshTokenInProgress = false;
    // Refresh Token Subject tracks the current token, or is null if no token is currently
    // available (e.g. refresh pending).
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
        null
    );   
    
    constructor(private authService: AuthenticationService,private router: Router){}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const url = environment.baseUrl;
        let token = '';

        // if (localStorage.getItem('user'))
        // {
        //     const user = JSON.parse(localStorage.getItem('user'));
        //     token = user.access_Token;
        // }

        if (request.url.toString().indexOf('assets/i18n') < 1) {
            request = request.clone({
                url: url + request.url,

                // setHeaders: {
                //     'Authorization': `Bearer ${token}`,
                //     'Content-Type': 'application/json',
                // },
            });
        }
        return next.handle(request)
        
        //***************** For Refresh Token */
        
        // .catch(error=>{
        //     // We don't want to refresh token for some requests like login or refresh token itself
        //     // So we verify url and we throw an error if it's the case
        //     if(request.url.includes("refreshToken") || request.url.includes("Authenticate"))
        //     {
        //         // We do another check to see if refresh token failed
        //         // In this case we want to logout user and to redirect it to login page

        //         if (request.url.includes("refreshToken")) {
        //             let user=JSON.parse(localStorage.getItem('user'));
        //             this.router.navigate([`/unauthorize/${user.orgId}`]);
        //         }

        //         return Observable.throw(error);
        //     }

        //     // If error status is different than 401 we want to skip refresh token
        //     // So we check that and throw the error if it's the case
        //     if (error.status !== 401) {
        //         return Observable.throw(error);
        //     }

        //     if (this.refreshTokenInProgress) {
        //         return this.refreshTokenSubject.pipe(
        //             filter(token => token != null),
        //             take(1),
        //             switchMap(token => {
        //                 return next.handle(this.addAuthenticationToken(request,token));
        //             }),);
        //     }
        //     else {
        //         this.refreshTokenInProgress = true;

        //         //Set the refreshTokenSubject to null so that subsequent API calls will wait until the new token has been retrieved
        //         this.refreshTokenSubject.next(null);
        //         let user=JSON.parse(localStorage.getItem('user'));

        //         return this.authService.getRefreshToken(user.refresh_Token).pipe(
        //             switchMap(data => {
        //                 if (data) {
        //                     this.refreshTokenSubject.next(data.access_Token);
        //                     localStorage.setItem('user',JSON.stringify(data));
        //                     return next.handle(this.addAuthenticationToken(request,data.access_Token));
        //                 }    
        //                 // If we don't get a new token, we are in trouble so logout.
        //                 this.router.navigate([`/unauthorize/${user.orgId}`]);
        //                 return null;
        //             }),
        //             catchError(error => {
        //                 // If there is an exception calling 'refreshToken', bad news so logout.
        //                 this.router.navigate([`/unauthorize/${user.orgId}`]);
        //                 return Observable.throw(error);
        //             }),
        //             finalize(() => {
        //                 this.refreshTokenInProgress = false;
        //             }))
        //     }
        // });   

        //*********************************************/
    }

    //**********************For Refresh Token */

    // addAuthenticationToken(request:any,token:any) {
    //     // let token="";
    //     // if(localStorage.getItem('user'))
    //     // {
    //     //     let user=JSON.parse(localStorage.getItem('user'));
    //     //     token=user.access_Token; 
    //     // }


    //     // If access token is null this means that user is not logged in
    //     // And we return the original request
    //     if(token==null || token=="")
    //     {
    //         return request;
    //     }

    //     // We clone the request, because the original request is immutable
    //     return request.clone({
    //         setHeaders: {
    //             "Authorization": `Bearer ${token}`,                    
    //             "Content-Type": "application/json"                
    //         }
    //     });
    // }

    //*************************************/

}