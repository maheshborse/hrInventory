import { Injectable } from '@angular/core';
import { CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot, ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router,private route: ActivatedRoute) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let user=JSON.parse(localStorage.getItem('user'));
        if(state.root.children[0].children[0].routeConfig.path == "administration-page" && user.role == 1){
            this.router.navigate(['/login']);
            return false;
        }
        // else if((state.root.children[0].children[0].routeConfig.path == "staff-assignment" || state.root.children[0].children[0].routeConfig.path == "administration-page") && user.role == 3){
        //     this.router.navigate(['/unauthorize/'+ user.orgId]);
        //     return false;
        // } else if(state.url == "/administration-page/manage-users/user-setup" &&  user.canAccessUserSetup == false ){
        //     this.router.navigate(['/unauthorize/'+ user.orgId]);
        //     return false;
        // } else if(state.url == "/administration-page/allergies" &&  user.canAccessIconSymbols == false ){
        //     this.router.navigate(['/unauthorize/'+ user.orgId]);
        //     return false;
        //} 
        else if (localStorage.getItem('user')) {
            return true;
        }
        // // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
       
        
    }
}