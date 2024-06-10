import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api'
import { Router } from '@angular/router';
import { RandomService } from './services/random.service';
import { RandomModel } from './models/random.model';
import { CartService } from './services/cart.service';
import { CartModel } from './models/cart.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ChineseSale';
  userItems: MenuItem[] = [];
  adminItems: MenuItem[] = [];

  role = localStorage.getItem('role');
  activeItem: MenuItem = this.userItems[0];
  isLogin: boolean = false;
  randon: RandomModel[] = [];
  cart:CartModel=new CartModel();

  constructor(private router: Router, private randomService: RandomService,private cartService:CartService) { }
  async ngOnInit() {
    await Promise.all([
      this.randomService.getRandomResult().then(r => (this.randon = r))
    ]);


    this.role = localStorage.getItem('role');
    if (!this.role) {
      this.router.navigate(['/login'])
    }
    else{
      await Promise.all([
        this.cartService.getCart().then(r => (this.cart = r))
      ]);
  
    if (this.randon?.length > 0) {
      this.userItems = [
        {
          label: "home",
          icon: "pi pi-home"
        },
        {
          label: "login",
          icon: "pi pi-user-edit"
        },
        {
          label: "randomResult",
          icon: "pi pi-question-circle"
        }
      ];
      this.activeItem = this.userItems[2];
       this.router.navigate(['/home']);
    }

    else {
      this.userItems = [
        {
          label: "home",
          icon: "pi pi-home"
        },
        {
          label: "login",
          icon: "pi pi-user-edit"
        },
        {
          label: "register",
          icon: "pi pi-user-plus"
        },
        {
          label: "gifts",
          icon: "pi pi-gift"
        },
        {
          label: "cart",
          icon: "pi pi-shopping-cart"
        }
      ];
      this.activeItem = this.userItems[0];
      this.router.navigate(['/home']);
    }


    this.adminItems = [
      {
        label: "home",
        icon: "pi pi-home"
      },
      {
        label: "login",
        icon: "pi pi-user-edit"
      },
      {
        label: "giftManagement",
        icon: "pi pi-gift"
      },
      {
        label: "donorManagement",
        icon: "pi pi-users"
      },
      {
        label: "purchasingManagment",
        icon: "pi pi-dollar"
      },
      {
        label: "randomManagment",
        icon: "pi pi-question-circle"
      }
    ];
    this.activeItem = this.adminItems[0];
    this.router.navigate(['/home']);

  }
}
}
