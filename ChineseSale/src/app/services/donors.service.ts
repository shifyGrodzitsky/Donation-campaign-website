import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import {DonorModel} from '../models/donor.model'
import { GiftModel } from '../models/gift.model';
import { OrderDetailsModel } from '../models/orderDetails.model';
@Injectable({
  providedIn: 'root'
})
export class DonorsService {

  donorsList: DonorModel[] = [];
  donor:DonorModel=new DonorModel();
  secces: boolean = false;
  giftsListForDonor:GiftModel[]=[];
  orderDetails:OrderDetailsModel[]=[]

  private callToGetDonorsSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  callToGetDonors$: Observable<boolean> = this.callToGetDonorsSubject.asObservable();

  constructor(private http: HttpClient) {

  }
 
  setGetDonors() {
    let flag = this.callToGetDonorsSubject.value;
    this.callToGetDonorsSubject.next(!flag);
  };

  getDonors(): Observable<DonorModel[]> {
    let url = 'http://localhost:5187/api/Donors';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    console.log("hhhhhhhhhhhhhhhhhhhhhhhhhhhh"+token)
    return this.http.get<DonorModel[]>(url, {headers}).pipe(map(l => this.donorsList = l));
  }

  deleteDonor(donorId: number): Observable<boolean> {
    let url = `http://localhost:5187/api/Donors/${donorId}`;
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.delete<boolean>(url,{headers});
  }

  addDonor(newDonor: DonorModel): Observable<any> {
    const url = 'http://localhost:5187/api/Donors';
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(url, newDonor,{headers});
  }

  
    updateDonor(updatedDonor: DonorModel): Observable<any> {
      const url = 'http://localhost:5187/api/Donors/';
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      });
      return this.http.put(url, updatedDonor,{headers});
    }
  
    async getDonorByFilter(type: string, value: string): Promise<DonorModel> {
      let url = 'http://localhost:5187/api/Donors/search/' + type + '/' + value;
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      });
      const response = await this.http.get<DonorModel>(url,{headers}).toPromise();
      this.donor = response as DonorModel;
      return this.donor;
    }

    getGiftsByDonor(donorId: number): Observable<GiftModel[]> {
      let url = 'http://localhost:5187/api/Donors/list_of_gift/'+donorId;
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      });
      return this.http.get<GiftModel[]>(url,{headers}).pipe(map(l => this.giftsListForDonor = l));
    }

    getOrderDetails(): Observable<OrderDetailsModel[]> {
      let url = 'http://localhost:5187/api/OrderDetails';
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      });
      return this.http.get<OrderDetailsModel[]>(url, {headers}).pipe(map(od => this.orderDetails = od));
    }
}