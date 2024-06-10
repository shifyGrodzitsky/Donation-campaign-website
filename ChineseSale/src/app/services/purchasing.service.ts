import { Injectable } from '@angular/core';
import {UserModel} from '../models/user.model';
import {OrderDetailsModel} from '../models/orderDetails.model'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { GiftModel } from '../models/gift.model';
@Injectable({
  providedIn: 'root'
})
export class PurchasingService {

  usersList: UserModel[] = [];
  orderDetailsList: OrderDetailsModel[] = [];
  giftsList:GiftModel[]=[];
  private callToGetGiftsSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  callToGetGifts$: Observable<boolean> = this.callToGetGiftsSubject.asObservable();
  constructor(private http: HttpClient) {

  }
  getUsersByGiftPurchasing(giftId: number): Observable<UserModel[]> {
    let url = 'http://localhost:5187/api/Purchase/'+giftId;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<UserModel[]>(url,{headers}).pipe(map(l => this.usersList = l));
  }

  getOrderDetailsByGiftPurchasing(giftId: number): Observable<OrderDetailsModel[]> {
    let url = 'http://localhost:5187/api/Purchase/GetPurchasesOrderDetailes/'+giftId;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<OrderDetailsModel[]>(url,{headers}).pipe(map(l => this.orderDetailsList = l));
  }
  GetSortByMostExpensive():Observable<GiftModel[]> {
    let url = 'http://localhost:5187/api/Purchase/SortByExpensiveGift';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<GiftModel[]>(url,{headers}).pipe(map(l => this.giftsList = l));
 
  }

   GetSortByMostPurchased():Observable<GiftModel[]> {
    let url = 'http://localhost:5187/api/Purchase/SortByNumOfPurchases';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<GiftModel[]>(url,{headers}).pipe(map(l => this.giftsList = l));
 
  }


}
