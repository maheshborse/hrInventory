import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ProductService } from 'src/app/shared/services/product.service';
import * as _ from 'lodash';
import { materialGrid } from 'src/app/shared/models/purchase';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss']
})
export class PurchaseComponent implements OnInit {
  value = 'Clear me';
  checkAddPoDetails : boolean =false;
  hidemainGrid:boolean =false;
  selectedProduct:any;
  productOption:any;
  categoryName:string ="";
  amount:number=0;
  rate:number = 0;
  quantity:number = 0;
  productId:number;
  ProductName:string;
  discount:number = 0;
  materialFillGrid: Array<materialGrid> = [];
 
  

  constructor(private productService:ProductService) {

  }

  ngOnInit() {
    this.detailsProduct();
  }

  openPoList(){
    this.hidemainGrid = false;
    this.checkAddPoDetails = false;
  }

  openPoDetails(){
    this.checkAddPoDetails = true;
    this.hidemainGrid = true;
  }
  
  changeProduct(id:any){
    this.productId = id;
    let data = this.selectedProduct.filter(k=> k.productid === id );
    this.categoryName = data[0].category.categoryname;
    this.ProductName = data[0].productname;
  }

  getQuantity(qty:any){
   this.quantity = qty;
   this.amount = this.quantity * this.rate ; 
  }

  getRate(rate:any){
    this.rate= rate;
    this.amount = this.quantity *  this.rate - this.discount; 
  }

  detailsProduct(){
    this.productService.getProduct()
    .subscribe(
      data => {
          this.selectedProduct=data;
          this.productOption = _.map(this.selectedProduct, item =>{
            const option = {
              value:'',
              label:''
            };
            option.value = item.productid;
            option.label = `${item.productname}`;
            return option;
            });
          },
          error => {
       }
    )
  }

  getDiscount(number:any){
    this.discount = number ;
    this.amount = this.quantity *  this.rate - number /100;
  }

  deletematerialonGird(i:number){
    this.materialFillGrid.splice(i,1)
  }

  addDataOnGrid(){
    let customObj = new materialGrid();
    customObj.Productid=this.productId;
    customObj.ProductName = this.ProductName ;
    customObj.Quantity=this.quantity;
    customObj.PoRate=this.rate;
    customObj.Discount= this.discount;
    customObj.Amount=this.amount;
    this.materialFillGrid.push(customObj);
  }

  
  

  

}
