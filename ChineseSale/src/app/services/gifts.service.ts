import { Injectable } from '@angular/core';
import { GiftModel } from '../models/gift.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class GiftsService {
  giftsList: GiftModel[] = [];
  secces: boolean = false;
  gift:GiftModel=new GiftModel();
  private callToGetGiftsSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  callToGetGifts$: Observable<boolean> = this.callToGetGiftsSubject.asObservable();
  constructor(private http: HttpClient) {

  }

  setGetGifts() {
    let flag = this.callToGetGiftsSubject.value;
    this.callToGetGiftsSubject.next(!flag);
  }

  getGifts(): Observable<GiftModel[]> {
    let url = 'http://localhost:5187/api/Gifts';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<GiftModel[]>(url,{headers}).pipe(map(l => this.giftsList = l));
  }

  deleteGift(giftId: number): Observable<boolean> {
    let url = `http://localhost:5187/api/Gifts/${giftId}`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.delete<boolean>(url,{headers});
  }

  addGift(newGift: GiftModel): Observable<any> {
    const url = 'http://localhost:5187/api/Gifts';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(url, newGift,{headers});
  }

  
    updateGift(updatedGift: GiftModel): Observable<any> {
      const url = 'http://localhost:5187/api/Gifts/';
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      });
      return this.http.put(url, updatedGift,{headers});
    }
  

    getGiftsByFilter(type: string, value: string): Observable<GiftModel[]> {
      let url = 'http://localhost:5187/api/Gifts/search/' + type + '/' + value;
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      });
      return this.http.get<GiftModel[]>(url,{headers}).pipe(map(l => this.giftsList = l));
    }



      async getGiftById(id:number): Promise<GiftModel> {
        let url = 'http://localhost:5187/api/Gifts/'+id;
        const token = localStorage.getItem('token');
        const headers = new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        });
        const response = await this.http.get<GiftModel>(url,{headers}).toPromise();
        this.gift = response as GiftModel;
        return this.gift;
      }
    }

