import { Injectable } from '@angular/core';
import { GiftModel } from '../models/gift.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserModel } from '../models/user.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { RandomModel } from '../models/random.model';

@Injectable({
  providedIn: 'root'
})
export class RandomService {

user:UserModel=new UserModel();
randomResult:RandomModel[]=[]
users:UserModel[]=[]
private callToGetRandomResultSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
callToGetRandomResult$: Observable<boolean> = this.callToGetRandomResultSubject.asObservable();
constructor(private http: HttpClient) {

}

setGetRandomResult() {
  let flag = this.callToGetRandomResultSubject.value;
  this.callToGetRandomResultSubject.next(!flag);
}

 async getRandomResult(): Promise<RandomModel[]> {
    let url = 'http://localhost:5187/api/Random';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    const response = await this.http.get<RandomModel[]>(url, {headers}).toPromise()
      this.randomResult = response as RandomModel[];
      return this.randomResult;
  
  }

 async getUsers(): Promise<UserModel[]> {
    let url = 'http://localhost:5187/api/Users';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    const response = await this.http.get<UserModel[]>(url, {headers}).toPromise()
      this.users = response as UserModel[];
      return this.users;
  
  }
  // getRandomResult(): Observable<RandomModel[]> {
  //   let url = 'http://localhost:5187/api/Random';
  //   const token = localStorage.getItem('token');
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}`
  //   });
  //   return this.http.get<RandomModel[]>(url, {headers}).pipe(map(r => this.randomResult = r));
  // }
  // getUsers(): Observable<UserModel[]> {
  //   let url = 'http://localhost:5187/api/Users';
  //   const token = localStorage.getItem('token');
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}`
  //   });
  //   return this.http.get<UserModel[]>(url, {headers}).pipe(map(r => this.users = r));
  // }

  async getUserByRandom(giftId: number): Promise<UserModel> {
    let url = 'http://localhost:5187/api/Random/'+giftId;  const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    const response = await this.http.get<UserModel>(url,{headers}).toPromise();
    this.user = response as UserModel;
    return this.user;
  }
}
