import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DonorModel } from 'src/app/models/donor.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
//import { CostValidator } from 'src/app/validators/costValidator';
import { DonorsService } from 'src/app/services/donors.service';
import { GiftModel } from 'src/app/models/gift.model';
import { ViewChild } from '@angular/core';
import { OverlayPanel, OverlayPanelModule } from 'primeng/overlaypanel';
import { OrderDetailsModel } from 'src/app/models/orderDetails.model';
import { GiftsService } from 'src/app/services/gifts.service';




@Component({
  selector: 'app-donor-management',
  templateUrl: './donor-management.component.html',
  styleUrls: ['./donor-management.component.css']
})
export class DonorManagementComponent implements OnInit {
  @ViewChild('op', { static: true }) op!: OverlayPanel;
  frmDonor: FormGroup = new FormGroup({});
  donorDialog: boolean = false;
  emailReg: RegExp = new RegExp('[]@[a-z].[a-z]')
  donors: DonorModel[] = [];
  donor: DonorModel = new DonorModel();

  selectedDonor: DonorModel[] = [];
  giftsForDonor: GiftModel[] = [];
  selectedGiftForDonor: GiftModel = new GiftModel();
  giftListDialog: boolean = false;
  submitted: boolean = false;

  statuses: any[] = [];
  searchText: string = '';
  filterByGift: string = ''
  filterByEmail: string = ''
  secces: boolean = false;
  copyDonor: DonorModel = new DonorModel();
  activityValues: number[] = [0, 100];
  orderDetailsList: OrderDetailsModel[] = []
  gifts: GiftModel[] = [];

  constructor(private giftService: GiftsService, private donorService: DonorsService, private messageService: MessageService, private confirmationService: ConfirmationService) {
    this.frmDonor = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      address: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      phone: new FormControl('', [Validators.required]),

    });
  }
  ngOnInit() {
    this.donorService.callToGetDonors$.subscribe(x => {
      this.donorService.getDonors().subscribe(lg => {
        this.donors = lg
        console.log("donor", this.donors)
      })
    });
    this.giftService.callToGetGifts$.subscribe(x => {
      this.giftService.getGifts().subscribe(lg => this.gifts = lg)
    });
    this.donorService.getOrderDetails().subscribe(od => this.orderDetailsList = od)
  }
  isNameDonorExists(): boolean {
    return this.donors.some((d) => d.firstName === this.donor.firstName && d.lastName === this.donor.lastName);
  }

  isDonorDelete(donor: DonorModel): boolean {
    var gifts = this.gifts.filter(v => v.donorId === donor.id);
    for (let v of gifts) {
      if (this.orderDetailsList.some(od => od.giftId === v.id)) {
        return true;
      }
    }
    return false;
  }

  openNew() {
    this.donor = new DonorModel();
    this.donor;
    this.submitted = false;
    this.donorDialog = true;
  }



  editDonor(donor: DonorModel) {
    Object.assign(this.copyDonor, donor)
    this.donor = this.copyDonor;
    this.donorDialog = true;
  }


  deleteDonor(donor: DonorModel) {
    if (this.isDonorDelete(donor)) {
      confirm("Sorry, but this donor cannot be deleted🤔")
    }
    else {
      this.confirmationService.confirm({
        message: 'Are you sure you want to delete ' + donor.firstName + ' ' + donor.lastName + '?',
        header: 'Confirm',
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
          this.donorService.deleteDonor(donor.id).subscribe(d => {
            this.donorService.setGetDonors()
          })
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Donor Deleted', life: 3000 });
        }
      });
    }
  }

  hideDialog() {
    this.donorDialog = false;
    this.submitted = false;
    this.donor = new DonorModel();
  }

  saveDonor() {

    this.submitted = true;

    if (this.donor.firstName && this.donor.lastName && this.donor.address && this.donor.email && this.donor.phone) {
      if (this.donor.id) {
        this.donorService.updateDonor(this.donor).subscribe(d => {
          this.donorService.setGetDonors()
        })
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'donor Updated', life: 3000 });

        this.donorDialog = false;
        this.donor = new DonorModel();
      }
      else {
        if (!this.isNameDonorExists()) {
          this.donorService.addDonor(this.donor).subscribe(d => {
            this.donorService.setGetDonors()
          })
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'donor Created', life: 3000 });
          this.donorDialog = false;
          this.donor = new DonorModel();
        }

      }
    }
  }

  async filterByEmailFunc() {
    await this.donorService.getDonorByFilter("email", this.filterByEmail);
    this.donor = this.donorService.donor;
    this.donors = [];
    this.donors.push(this.donor);
  }

  async filterByGiftFunc() {
    await this.donorService.getDonorByFilter("gift", this.filterByGift)
    this.donor = this.donorService.donor;
    this.donors = [];
    this.donors.push(this.donor);

  }
  getDonorGifts(d: DonorModel) {
    this.donorService.getGiftsByDonor(d.id).subscribe((lg: GiftModel[]) => this.giftsForDonor = lg)
    this.giftListDialog = true;
  }

  //   onRowSelect(event) {
  //     this.messageService.add({severity: 'info', summary: 'Product Selected', detail: event.data.name});
  // }

  openDonorGiftsPanel(donor: DonorModel) {
    this.getDonorGifts(donor); // הפעלת פונקציה "getDonorGifts" עם הפרמטר "donor"
    this.op.show(event);
  }


}
