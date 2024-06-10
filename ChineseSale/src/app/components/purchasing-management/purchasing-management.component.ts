import { Component,OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { GiftModel } from 'src/app/models/gift.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CostValidator } from 'src/app/validators/costValidator';
import { GiftsService } from 'src/app/services/gifts.service';
import {PurchasingService} from 'src/app/services/purchasing.service';
import { ViewChild } from '@angular/core'; 
import { OverlayPanel, OverlayPanelModule } from 'primeng/overlaypanel';
import { UserModel } from 'src/app/models/user.model';
import { OrderDetailsModel } from 'src/app/models/orderDetails.model';


@Component({
  selector: 'app-purchasing-management',
  templateUrl: './purchasing-management.component.html',
  styleUrls: ['./purchasing-management.component.css']
})
export class PurchasingManagementComponent implements OnInit{
  @ViewChild('op', { static: true }) op!: OverlayPanel;
  @ViewChild('op1', { static: true }) op1!: OverlayPanel;
  frmGift: FormGroup = new FormGroup({});
  giftDialog: boolean = false;
  emailReg: RegExp = new RegExp('[]@[a-z].[a-z]')
  gifts: GiftModel[] = [];

  gift: GiftModel = new GiftModel();

  selectedGift: GiftModel[] = [];

  submitted: boolean = false;
  placeholder="Sort By..."
  statuses: any[] = [];
  searchText: string = '';
  filterByNumOfPurchase: string=''
  filterByDonorName:string=''
  secces: boolean = false;
  copyGift: GiftModel = new GiftModel();
  activityValues: number[] = [0, 100];
  usersList:UserModel[]=[];
  orderDetailsList:OrderDetailsModel[]=[];
  selectedUser: UserModel = new UserModel();
  selectedOrderDetails: OrderDetailsModel = new OrderDetailsModel();
  sortOptions: any[] = [
    { name: 'Sort by most purchased gift', code: 'most-purchased' }, 
    { name: 'Sort by most expensive gift', code: 'most-expensive' } ]; 
  selectedSort: any={ name: '', code: '' }

  constructor(public giftService: GiftsService,public purchasingService: PurchasingService, private messageService: MessageService, private confirmationService: ConfirmationService) {
      this.frmGift = new FormGroup({
          giftName: new FormControl('', [Validators.required]),
          description: new FormControl('', [Validators.required]),
          category: new FormControl('', [Validators.required]),
          donorId: new FormControl(0, [Validators.required, Validators.maxLength(10)]),
          ticketPrice: new FormControl(null, [CostValidator(10)]),
      });
  }
  ngOnInit() {
      this.giftService.callToGetGifts$.subscribe(x => {
          this.giftService.getGifts().subscribe(lg => this.gifts = lg)
      });

  }

  hideDialog() {
      this.giftDialog = false;
      this.submitted = false;
  }
  openUsersGiftsPanel(gift: GiftModel) {
    this.getUsersByGift(gift);
    //this.getDonorGifts(donor); // הפעלת פונקציה "getDonorGifts" עם הפרמטר "donor"
    this.op.show(event);
  }
  openOrderDetailsGiftsPanel(gift: GiftModel) {
    this.getOrderDetailsByGift(gift);
    //this.getDonorGifts(donor); // הפעלת פונקציה "getDonorGifts" עם הפרמטר "donor"
    this.op1.show(event);
  }

  getUsersByGift(gift:GiftModel){
    this.purchasingService.getUsersByGiftPurchasing(gift.id).subscribe((lg:UserModel[]) => this.usersList = lg)
    //this.giftListDialog=true;
  }

  getOrderDetailsByGift(gift:GiftModel){
    this.purchasingService.getOrderDetailsByGiftPurchasing(gift.id).subscribe((lg:OrderDetailsModel[]) => this.orderDetailsList = lg)
    //this.giftListDialog=true;
  }

  SortByMostExpensive(){
    this.purchasingService.GetSortByMostExpensive().subscribe((g:GiftModel[])=>this.gifts=g)
  }

  SortByMostPurchased(){
    this.purchasingService.GetSortByMostPurchased().subscribe((g:GiftModel[])=>this.gifts=g)
 
  }
  onSortChange(){
    // console.log(this.selectedSort)
    // switch (this.selectedSort.code) {
    //   case 'most-purchased':
    //     this.SortByMostPurchased() 
    //     break;
    //   case 'most-expensive':
    //     this.SortByMostExpensive()
    //     break;

    //   default:
    //     // הוראות ברירת מחדל
    //     break;
    // }
if(this.selectedSort.code==='most-purchased')
{
  this.SortByMostPurchased() 
}
else{
  this.SortByMostExpensive()
}
  }

}
