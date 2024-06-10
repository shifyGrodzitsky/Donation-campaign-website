import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';

import { CartModel } from '../models/cart.model';
import { DraftModel } from '../models/draft.model';
@Injectable({
  providedIn: 'root'
})
export class CartService {
  drafts: DraftModel[] = []
  cart: CartModel = new CartModel()

  private callToGetCartSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  callToGetCart$: Observable<boolean> = this.callToGetCartSubject.asObservable();
  private callToGetDraftSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  callToGetDraft$: Observable<boolean> = this.callToGetDraftSubject.asObservable();
  constructor(private http: HttpClient) {

  }

  setGetCart() {
    let flag = this.callToGetCartSubject.value;
    this.callToGetCartSubject.next(!flag);
  }

  async getCart(): Promise<CartModel> {
    let url = 'http://localhost:5187/api/Cart';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    const response = await this.http.get<CartModel>(url, { headers }).toPromise();
    this.cart = response as CartModel;
    return this.cart;
  }

  setGetDrafts() {
    let flag = this.callToGetDraftSubject.value;
    this.callToGetDraftSubject.next(!flag);
  }

  async getDrafts(): Promise<DraftModel[]> {
    let url = 'http://localhost:5187/api/Drafts/' + this.cart.id;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    try {
      const response = await this.http.get<DraftModel[]>(url, { headers }).toPromise();
      this.drafts = response as DraftModel[];
      return this.drafts;
    }
    catch (error) { console.error(error); throw error; }
  }

  addDraft(draft: DraftModel): Observable<any> {
    const url = 'http://localhost:5187/api/Drafts';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(url, draft, { headers });
  }


  deleteDraftFromCart(id: number): Observable<boolean> {
    let url = 'http://localhost:5187/api/Drafts/' + id;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.delete<boolean>(url, { headers });
  }

  async addQuantity(draftId: number): Promise<void> {
    let url = 'http://localhost:5187/api/Drafts/UpdateDraftQuentity' ;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    try {
      await this.http.post(url,draftId, { headers }).toPromise();
    }
    catch (error) { console.error(error); throw error; }
  }


  async decreaseQuantity(draftId: number): Promise<void> {
    let url = 'http://localhost:5187/api/Drafts/DecreaseDraftQuentity';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    try {
      await this.http.post(url,draftId, { headers }).toPromise();
    }
    catch (error) { console.error(error); throw error; }
  }
confirmOrder(): Observable<any> {
    let url = 'http://localhost:5187/api/OrderDetails';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(url,this.cart.id, {headers});  }
}
