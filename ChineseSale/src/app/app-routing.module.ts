import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { GiftManagementComponent } from './components/gift-management/gift-management.component';
import { DonorManagementComponent } from './components/donor-management/donor-management.component';
import { PurchasingManagementComponent } from './components/purchasing-management/purchasing-management.component';
import { RandomManagementComponent } from './components/random-management/random-management.component';
import { UserGiftsComponent } from './components/user-gifts/user-gifts.component';
import { CartComponent } from './components/cart/cart.component';
import { RandomComponent } from './components/random/random.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
const routes: Routes = [
  {path:'login', component:LoginComponent},
  {path:'register', component:RegisterComponent},
  {path:'giftManagement', component:GiftManagementComponent},
  {path:'donorManagement',component:DonorManagementComponent},
  {path:'purchasingManagment',component:PurchasingManagementComponent},
  {path:'randomManagment',component:RandomManagementComponent},
  {path:'gifts',component:UserGiftsComponent},
  {path:'cart',component:CartComponent},
  {path:'randomResult',component:RandomComponent},
  {path:'home',component:HomeComponent},



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
