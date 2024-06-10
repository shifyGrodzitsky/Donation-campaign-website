import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { GiftModel } from 'src/app/models/gift.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CostValidator } from 'src/app/validators/costValidator';
import { GiftsService } from 'src/app/services/gifts.service';


@Component({
    selector: 'app-gift-management',
    templateUrl: './gift-management.component.html',
    styleUrls: ['./gift-management.component.css'],
    providers: [ConfirmationService, MessageService]
})
export class GiftManagementComponent implements OnInit {
    frmGift: FormGroup = new FormGroup({});
    giftDialog: boolean = false;
    emailReg: RegExp = new RegExp('[]@[a-z].[a-z]')
    gifts: GiftModel[] = [];

    gift: GiftModel = new GiftModel();

    selectedGift: GiftModel[] = [];

    submitted: boolean = false;

    statuses: any[] = [];
    searchText: string = '';
    filterByNumOfPurchase: string = ''
    filterByDonorName: string = ''
    secces: boolean = false;
    copyGift: GiftModel = new GiftModel();
    activityValues: number[] = [0, 100];


    constructor(public giftService: GiftsService, private messageService: MessageService, private confirmationService: ConfirmationService) {
        this.frmGift = new FormGroup({
            giftName: new FormControl('', [Validators.required]),
            description: new FormControl('', [Validators.required]),
            category: new FormControl('', [Validators.required]),
            donorId: new FormControl(0, [Validators.required, Validators.maxLength(10)]),
            ticketPrice: new FormControl(null, [CostValidator(10)]),
            // email: new FormControl('', [Validators.pattern(this.emailReg)])
        });
    }
    ngOnInit() {
        this.giftService.callToGetGifts$.subscribe(x => {
            this.giftService.getGifts().subscribe(lg => this.gifts = lg)
        });

    }
    isNameGiftExists(): boolean {
        return this.gifts.some((g) => g.name === this.gift.name);
    }
    openNew() {
        this.gift=new GiftModel();
        this.submitted = false;
        this.giftDialog = true;
    }


    editGift(gift: GiftModel) {
        Object.assign(this.copyGift, gift)
        this.gift = this.copyGift;
        this.giftDialog = true;
    }


    deleteGift(gift: GiftModel) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + gift.name + '?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.giftService.deleteGift(gift.id).subscribe(d => {
                    this.giftService.setGetGifts()
                })
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Gift Deleted', life: 3000 });
            }
        });
    }

    hideDialog() {
        this.giftDialog = false;
        this.submitted = false;
        this.gift=new GiftModel();
    }

    saveGift() {

        this.submitted = true;
        if (this.gift.name && this.gift.category && this.gift.description && this.gift.donorId && this.gift.ticketPrice) {

            if (this.gift.id) {
                this.giftService.updateGift(this.gift).subscribe(d => {
                    this.giftService.setGetGifts()
                })
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'gift Updated', life: 3000 });
                this.giftDialog = false;
                this.gift = new GiftModel();
            }
            else {
                if (!this.isNameGiftExists()) {
                    this.giftService.addGift(this.gift).subscribe(d => {
                        this.giftService.setGetGifts()
                    })
                    this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'gift Created', life: 3000 });
                    this.giftDialog = false;
                    this.gift = new GiftModel();
                }
            }

        }
    }

    filterByNumOfPurchaseFunc() {
        this.giftService.getGiftsByFilter("numofpurchase", this.filterByNumOfPurchase).subscribe(lg => this.gifts = lg)
    }

    filterByDonorNameFunc() {
        this.giftService.getGiftsByFilter("donor", this.filterByDonorName).subscribe(lg => this.gifts = lg)
    }


}


