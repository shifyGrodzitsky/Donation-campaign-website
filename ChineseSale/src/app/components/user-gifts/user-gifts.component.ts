import { Component, OnInit, ViewChild } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { PrimeNGConfig } from 'primeng/api';
import { DraftModel } from 'src/app/models/draft.model';
import { GiftModel } from 'src/app/models/gift.model';
import { CartService } from 'src/app/services/cart.service';
import { GiftsService } from 'src/app/services/gifts.service';
import {MessageService} from 'primeng/api';

@Component({
  selector: 'app-user-gifts',
  templateUrl: './user-gifts.component.html',
  styleUrls: ['./user-gifts.component.css']
})
export class UserGiftsComponent implements OnInit{
  @ViewChild('myDropdown') myDropdown: any;
  gifts: GiftModel[] = [];
  layout: string = 'list';
  sortKey:string=""
  sortOptions: SelectItem[] = [];
  event: any
  sortOrder: number = 0;
  searchText: string = ""
  sortField: string = "";
  d:DraftModel=new DraftModel()

  constructor(private giftService: GiftsService,private cartService:CartService, private primengConfig: PrimeNGConfig,   private messageService: MessageService) { }

  ngOnInit() {
    this.giftService.callToGetGifts$.subscribe(x => {
      this.giftService.getGifts().subscribe(lg => this.gifts = lg)
    });
    this.giftService.setGetGifts();
    
  }

  onSortChange(event: any) {
  }

   addToCart(gift:GiftModel){
    this.d.quentity=1
    this.d.giftId=gift.id
    this.d.cartId=this.cartService.cart.id
    this.cartService.addDraft(this.d).subscribe(d=>d)
    this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'The gift get successfully into your cart!' });
  }
}

