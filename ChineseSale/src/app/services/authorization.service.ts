import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { UserModel } from '../models/user.model';
import { LoginModel } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  token:string="";
  tokeRole:string[]=[]
  private callToGetAuthSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  callToGetAuth$: Observable<boolean> = this.callToGetAuthSubject.asObservable();
  constructor(private http: HttpClient) { }

    async createUserFunc(user: UserModel): Promise<any> {
      const url = 'http://localhost:5187/api/Auth/register';
      const response = await this.http.post(url, user).toPromise();
      return response;
    }
 
  async loginFunc(login:LoginModel): Promise<string> { 
    this.token=""
    let url = 'http://localhost:5187/api/Auth/login'; 
    const response = await this.http.post(url,login,{ responseType: 'text' }).toPromise(); 
    this.token = response as string;
    this.tokeRole=this.token.split(" ");
    localStorage.setItem('token', this.tokeRole[0]); 
    localStorage.setItem('role', this.tokeRole[1]); 
    console.log("token",this.token)
    return this.token; 
  
  }
}

