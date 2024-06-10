import { Component, OnInit } from '@angular/core';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { GiftModel } from 'src/app/models/gift.model';
import { RandomModel } from 'src/app/models/random.model';
import { UserModel } from 'src/app/models/user.model';
import { GiftsService } from 'src/app/services/gifts.service';
import { RandomService } from 'src/app/services/random.service';

@Component({
  selector: 'app-random',
  templateUrl: './random.component.html',
  styleUrls: ['./random.component.css']
})

export class RandomComponent implements OnInit {
  gifts: GiftModel[] = [];
  updateGifts: GiftModel[] = [];
  randomResult: RandomModel[] = [];
  users: UserModel[] = [];
  gift: GiftModel = new GiftModel();
  user: UserModel = new UserModel();
  searchText: string = "";

  constructor(
    private randomService: RandomService,
    private giftService: GiftsService,
  ) {}

  async ngOnInit() {
    this.giftService.callToGetGifts$.subscribe(x => {
      this.giftService.getGifts().subscribe(lg => (this.gifts = lg));
    });
    this.giftService.setGetGifts();

    await Promise.all([
      this.randomService.getRandomResult().then(r => (this.randomResult = r)),
      this.randomService.getUsers().then(u => (this.users = u))
    ]);

    this.loadGifts();
  }

  loadGifts() {
    this.updateGifts = [];
    this.randomResult.forEach(x => {
      const gift = this.gifts.find(g => x.giftId === g.id);
      this.gift = gift as GiftModel;
      console.log(x);
      const user = this.users.find(u => x.userID === u.id);

      if (user) {
        this.user = user as UserModel;
        this.gift.category = this.user.firstName + " " + this.user.lastName;
      }

      this.updateGifts.push(this.gift);
    });
  }
}



// export class RandomComponent implements OnInit {
//   gifts: GiftModel[] = []
//   updateGifts: GiftModel[] = []
//   randomResult: RandomModel[] = []
//   users: UserModel[] = [];
//   gift: GiftModel = new GiftModel
//   user: UserModel = new UserModel
//   searchText: string = ""
//   constructor(private randomServise: RandomService, private giftService: GiftsService, private primengConfig: PrimeNGConfig, private messageService: MessageService) { }

//   async ngOnInit() {
//     this.giftService.callToGetGifts$.subscribe(x => {
//       this.giftService.getGifts().subscribe(lg => this.gifts = lg)
//     });
//     this.giftService.setGetGifts()
//     await this.randomServise.getRandomResult().then(r => this.randomResult = r)

//     await this.randomServise.getUsers().then(u => this.users = u)

//     this.randomResult.forEach(x => {
//       var gift = this.gifts.find(g => x.giftId === g.id)
//       this.gift = gift as GiftModel
//       console.log(x)
//       var user = this.users.find(u => x.userID === u.id)

//       if (user) {
//         this.user = user as UserModel
//         this.gift.category = this.user.firstName + " " + this.user.lastName
//       }

//       this.updateGifts.push(this.gift)
//     })
//   }

// }
