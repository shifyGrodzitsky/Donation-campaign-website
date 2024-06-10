import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { HttpClientModule } from '@angular/common/http';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DropdownModule } from 'primeng/dropdown';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CheckboxModule } from 'primeng/checkbox';
import { TabMenuModule } from 'primeng/tabmenu';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {DataViewModule} from 'primeng/dataview';
import {VirtualScrollerModule} from 'primeng/virtualscroller';
import { CardModule } from 'primeng/card';
import { CarouselModule } from 'primeng/carousel';
import {ToastModule} from 'primeng/toast';
import { BlockUIModule } from 'primeng/blockui';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { InputMaskModule } from 'primeng/inputmask';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PasswordModule } from 'primeng/password';

import { CalendarModule } from 'primeng/calendar';
import { FileUploadModule } from 'primeng/fileupload';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { GiftManagementComponent } from './components/gift-management/gift-management.component';
import { DonorManagementComponent } from './components/donor-management/donor-management.component';
import { PurchasingManagementComponent } from './components/purchasing-management/purchasing-management.component';
import { RandomManagementComponent } from './components/random-management/random-management.component';
import { UserGiftsComponent } from './components/user-gifts/user-gifts.component';
import { CartComponent } from './components/cart/cart.component';
import { RandomComponent } from './components/random/random.component';
import { HomeComponent } from './components/home/home.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    GiftManagementComponent,
    DonorManagementComponent,
    PurchasingManagementComponent,
    RandomManagementComponent,
    UserGiftsComponent,
    CartComponent,
    RandomComponent,
    HomeComponent
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    TableModule,
    HttpClientModule,
    InputTextModule,
    DialogModule,
    ToolbarModule,
    ConfirmDialogModule,
    RatingModule,
    InputNumberModule,
    InputTextareaModule,
    RadioButtonModule,
    DropdownModule,
    ButtonModule,
    CalendarModule,
    FileUploadModule,
    ReactiveFormsModule,
    CheckboxModule,
    TabMenuModule,
    OverlayPanelModule,
    DataViewModule,
    VirtualScrollerModule,
    CardModule,
    CarouselModule,
    ToastModule,
    BlockUIModule,
    AvatarModule,
    AvatarGroupModule,
    InputMaskModule,
    ProgressSpinnerModule,
    PasswordModule
  ],
  providers: [ConfirmationService,MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
