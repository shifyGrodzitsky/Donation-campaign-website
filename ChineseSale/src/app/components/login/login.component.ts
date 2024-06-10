import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginModel } from 'src/app/models/login.model';
import { AuthorizationService } from 'src/app/services/authorization.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  login: LoginModel = new LoginModel();
  token: string = "";
  frmLogin: FormGroup = new FormGroup({});
  emailReg: RegExp = new RegExp('[]@[a-z].[a-z]');
  submitted: boolean = false;

  constructor(private router: Router, private activatedRoute: ActivatedRoute, public authService: AuthorizationService) {
    this.frmLogin = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
  }

  async loginFunc() {
    this.token = "";
    this.submitted = true;
    if (this.login.email && this.login.password) {
      await this.authService.loginFunc(this.login)
      this.token = this.authService.token;
      location.reload();
    }
  }
}
