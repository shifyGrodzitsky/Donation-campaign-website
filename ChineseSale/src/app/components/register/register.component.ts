import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/models/user.model';
import { AuthorizationService } from 'src/app/services/authorization.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
user:UserModel=new UserModel();
frmUser: FormGroup = new FormGroup({});
emailReg: RegExp = new RegExp('[]@[a-z].[a-z]');
submitted: boolean = false;
constructor(private router: Router,public authService:AuthorizationService) {
  this.frmUser = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    address: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required]),
    phone: new FormControl('', [Validators.required]),
    password:new FormControl('', [Validators.required]),
  });
}
createUser(){
this.submitted = true;
if(this.user.email&&this.user.password &&this.user.address&&this.user.firstName&&this.user.lastName&&this.user.phone){
this.authService.createUserFunc(this.user)}
this.router.navigate(['/login']);
}

}
