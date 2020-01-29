import { Component, OnInit, ElementRef, Renderer2 } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { Router } from '@angular/router';
import { Login } from 'src/app/shared/models/login';
import { user } from 'src/app/shared/models/user';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  selector: 'app-auth-signin',
  templateUrl: './auth-signin.component.html',
  styleUrls: ['./auth-signin.component.scss']
})
export class AuthSigninComponent implements OnInit {
  strongMessage: string = "";
  eventMessage: string = "";
  userinfoObj = new user();
  loginObj = new Login();
  message: string;
  isMessage: boolean = false;
  constructor(  private authService: AuthenticationService,
    private router: Router,
    private elem: ElementRef,
    private renderer: Renderer2,private notificationService : NotificationService) { }

  ngOnInit() {
  }

  signIn() {
    
    this.authService.login(this.loginObj).subscribe(
      data => {
        console.log(data);
        localStorage.setItem("user", JSON.stringify(data));
        this.router.navigate(["dashboard/analytics"]);
      },
      error => {
        this.notificationService.error("Invalid Credentials");
      }
    );
  }

}
