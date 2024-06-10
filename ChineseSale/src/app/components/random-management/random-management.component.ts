import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { GiftModel } from 'src/app/models/gift.model';
import { GiftsService } from 'src/app/services/gifts.service';
import { RandomService } from 'src/app/services/random.service';
import { UserModel } from 'src/app/models/user.model';
import { RandomModel } from 'src/app/models/random.model';


@Component({
  selector: 'app-random-management',
  templateUrl: './random-management.component.html',
  styleUrls: ['./random-management.component.css']
})
export class RandomManagementComponent implements OnInit {
  emailReg: RegExp = new RegExp('[]@[a-z].[a-z]')
  gifts: GiftModel[] = [];
  blocked: boolean = false; 
  

  gift: GiftModel = new GiftModel();
  user: UserModel = new UserModel();
  selectedGift: GiftModel[] = [];
  searchText: string = '';
  randon: RandomModel[] = []
  constructor(public giftService: GiftsService, public randomService: RandomService, private messageService: MessageService, private confirmationService: ConfirmationService) {
    // this.frmGift = new FormGroup({
    //     giftName: new FormControl('', [Validators.required]),
    //     description: new FormControl('', [Validators.required]),
    //     category: new FormControl('', [Validators.required]),
    //     donorId: new FormControl(0, [Validators.required, Validators.maxLength(10)]),
    //     ticketPrice: new FormControl(null, [CostValidator(10)]),
    //     // email: new FormControl('', [Validators.pattern(this.emailReg)])
  };

  async ngOnInit() {

    await Promise.all([
      this.giftService.callToGetGifts$.subscribe(x => {
        this.giftService.getGifts().subscribe(lg => this.gifts = lg)
      }),
      this.randomService.callToGetRandomResult$.subscribe(x => {
        this.randomService.getRandomResult().then(r => (this.randon = r))
      }),

    ]);
  }
  isRandom(id: number) {
    return this.randon.some((g) => id === g.giftId);
  }

  async randomGift(g: GiftModel) {
    this.draw()
    await this.randomService.getUserByRandom(g.id)
    this.user = this.randomService.user;
    this.randomService.setGetRandomResult()
  }
  loading: boolean = false;


  draw() { 
    this.blocked = true; 
    setTimeout(() => { 
        this.blocked = false; 
    }, 3500); 
}
}