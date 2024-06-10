import { Component, OnInit, ViewChild, AfterViewInit, QueryList, ElementRef, ViewChildren } from '@angular/core';
import { LazyLoadEvent, PrimeNGConfig, SelectItem } from 'primeng/api';
import { CartModel } from 'src/app/models/cart.model';
import { DraftModel } from 'src/app/models/draft.model';
import { GiftModel } from 'src/app/models/gift.model';
import { CartService } from 'src/app/services/cart.service';
import { GiftsService } from 'src/app/services/gifts.service';

import { ConfirmationService, ConfirmEventType, MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  [x: string]: any;

  //virtualorderDetails: OrderDetailsModel[] = [];
  @ViewChild('myDropdown') myDropdown: any;
  @ViewChildren('myDiv') divElements!: QueryList<ElementRef>;
  sortKey: string = "";
  gifts: GiftModel[] = [];
  draftGifts: GiftModel[] = [];
  gift: GiftModel = new GiftModel()
  giftSelected: GiftModel = new GiftModel()
  sortOptions: SelectItem[] = [];
  cart: CartModel = new CartModel();
  drafts: DraftModel[] = [];
  draft: DraftModel = new DraftModel();
  responsiveOptions: any[] | undefined;
  value1: number = 0;
  value2: number = 0;
  searchText: string = ""
  index1: number = 0;
  blockedDocument: boolean = false;
  blocked:boolean=false;
  creditCardDialog: boolean = false;
  submitted: boolean = false;
  creditCardForm: FormGroup=new FormGroup({});
  constructor(private router: Router,private cartServise: CartService, private giftService: GiftsService, private primengConfig: PrimeNGConfig, private confirmationService: ConfirmationService, private messageService: MessageService,private formBuilder: FormBuilder
    ) {
      this.creditCardForm = this.formBuilder.group({
        cardNumber: ['', Validators.required],
        cardHolder: ['', Validators.required],
        expirationDate: ['', Validators.required],
        cvv: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(4)]]
      });
     }

  async ngOnInit() {

    this.giftService.callToGetGifts$.subscribe(x => {
      this.giftService.getGifts().subscribe(lg => this.gifts = lg)
    });
    this.cartServise.callToGetCart$.subscribe(() => {
      this.cartServise.getCart().then(cart => {
        this.cart = cart;
      });
    });

    this.cartServise.callToGetDraft$.subscribe(() => {
      this.cartServise.getDrafts().then(drafts => {
        this.drafts = drafts;
        this.draftGifts = []
        this.drafts.forEach(v => {
          var g = this.gifts.find(g => v.giftId === g.id)
          this.gift = g as GiftModel;
          this.gift.numOfPurchases = v.quentity;
          this.draftGifts.push(this.gift);
        })
      });
    });
    await this.cartServise.getCart()
    this.cart = this.cartServise.cart
    this.cartServise.setGetDrafts()


    this.responsiveOptions = [
      {
        breakpoint: '1199px',
        numVisible: 1,
        numScroll: 1
      },
      {
        breakpoint: '991px',
        numVisible: 2,
        numScroll: 1
      },
      {
        breakpoint: '767px',
        numVisible: 1,
        numScroll: 1
      }
    ];
  }
  confirm1() {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to proceed?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: async () => {
        this.cartServise.confirmOrder().subscribe();
        this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'The purchase was successfully completed!' });
        this.cartServise.setGetCart();
        this.draftGifts = [];
        await this.tempBlock();
        this.router.navigate(['/home']);
        
      },
      reject: (type: ConfirmEventType) => {
        switch (type) {
          case ConfirmEventType.REJECT:
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
            break;
          case ConfirmEventType.CANCEL:
            this.messageService.add({ severity: 'warn', summary: 'Cancelled', detail: 'You have cancelled' });
            break;
        }
      }
    });

  }

  async decreaseQuantity1(giftId: number) {
    var d = this.drafts.find(d => giftId === d.giftId)
    this.draft = d as DraftModel
    if (this.draft.quentity > 1) {
      await this.cartServise.decreaseQuantity(this.draft.id).then(d => {
        this.cartServise.setGetDrafts();
        this.cartServise.setGetCart();
      })
    }
    else
      confirm("you cant decrease Quantity😪")

  }
  async addQuantity1(giftId: number) {
    var d = this.drafts.find(d => giftId === d.giftId);
    this.draft = d as DraftModel;
    await this.cartServise.addQuantity(this.draft.id);
    this.cartServise.setGetDrafts();
    this.cartServise.setGetCart();
  }
  deleteDraftFromTheCart(giftId: number) {
    var d = this.drafts.find(d => giftId === d.giftId);
    this.draft = d as DraftModel;
    this.cartServise.deleteDraftFromCart(this.draft.id).subscribe(d => {
      this.cartServise.setGetDrafts();
      this.cartServise.setGetCart();
    });

  }



tempBlock(): Promise<void> {
  return new Promise<void>((resolve) => {
    this.blocked = true;
    setTimeout(() => {
      this.blocked = false;
      resolve(); 
    }, 2000);
  });
}
hideDialog() {
  this.creditCardDialog = false;
  this.submitted = false;
}
openNew() {
  this.submitted = false;
  this.creditCardDialog = true;
}

}
