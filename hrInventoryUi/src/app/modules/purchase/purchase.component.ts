import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ProductService } from 'src/app/shared/services/product.service';
import * as _ from 'lodash';
import { materialGrid, purchaseOrdermodel, purchaseOrderMaster, purchaseDetails } from 'src/app/shared/models/purchase';
import { PurchaseService } from 'src/app/shared/services/purchase';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';


@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss'],
 
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
  dateofBirth :Date;
  discount:number = 0;
  materialFillGrid: Array<materialGrid> = [];
  totalAmountText:number;
  discountMaster:number=0;
  finalAmount:number;
  materialSaveList:any=[];
  dataSource = new MatTableDataSource();
  // poId
  poid:number;
  autogenratedPoId:number;
  
  
  checkAddOrEdit:any="";

  


  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['poNumber', 'poDate','totalAmount','discount','finalAmount','action'];
  
  constructor(private productService:ProductService,private purchaseService :PurchaseService,private notificationService : NotificationService) {
    
  }

  ngOnInit() {
    this.detailsProduct();
    this.PurchaseList();
  }

  openPoList(){
    this.hidemainGrid = false;
    this.checkAddPoDetails = false;
    this.PurchaseList();
    this.getdefault();
  }

  getdefault(){
    this.materialFillGrid =[];
    this.materialSaveList=[];
    this.categoryName = "";
    this.totalAmountText = 0;
    this.quantity = 0;
    this.rate =0;
    this.discount=0;
    this.amount=0;
    this.discountMaster=0;
    this.finalAmount=0;
  }

  openPoDetails(){
    if(this.dataSource.data.length === 0 ){
      this.poid = 0;
      this.poid = this.poid + 1;
    } else {
      this.poid = Math.max.apply(Math, this.dataSource.data.map(function(o){return o.poid}))
      this.poid = this.poid + 1;
    }
    this.dateofBirth = new Date();
    this.checkAddPoDetails = true;
    this.hidemainGrid = true;
    this.checkAddOrEdit = 'add';
  }
  
  changeProduct(id:any){
    this.productId = id;
    let data = this.selectedProduct.filter(k=> k.productid === id );
    this.categoryName = data[0].category.categoryname;
    this.ProductName = data[0].productname;
  }

  getQuantity(qty:any){
   this.quantity = qty;
   if(this.discount === 0 ){
      this.amount = this.quantity * this.rate;
    } else if(this.discount > 0) {
      this.amount = this.quantity * this.rate;
      this.amount = this.amount - (this.amount * this.discount / 100);
    }
   
  }

  getRate(rate:any){
    this.rate= rate;
    if(this.discount === 0 ){
      this.amount = this.quantity * this.rate;
    } else if(this.discount > 0) {
      this.amount = this.quantity * this.rate;
      this.amount = this.amount - (this.amount * this.discount / 100);
    }
  }

  discountOnTotalAmount(dis:any ){
    this.discountMaster = dis === "0" ||  dis === ""  ? 0 : dis;
    if(this.discountMaster === 0){
      this.finalAmount = this.totalAmountText ;
    } else if (this.discountMaster > 0){
      this.finalAmount = this.totalAmountText - (this.totalAmountText * this.discountMaster / 100);
    }
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

  getPoDataEdit(poData:any,checkLabelt:any){
    this.materialFillGrid =[];
    this.materialSaveList=[];
    this.poid= poData.poid;
    this.checkAddPoDetails = true;
    this.hidemainGrid = true;
    this.dateofBirth  = poData.podate;
    this.totalAmountText = poData.totalamount;
    this.discountMaster = poData.discount;
    this.finalAmount = poData.finalamount;
    for(var i=0 ; i< poData.podetailModels.length;i++){
        let customObj = new materialGrid();
          customObj.Productid=poData.podetailModels[i].productid;
          let data = this.selectedProduct.filter(k=> k.productid == poData.podetailModels[i].productid );
          customObj.ProductName = data[0].productname;
          customObj.Quantity=poData.podetailModels[i].quantity;
          customObj.PoRate=poData.podetailModels[i].porate;
          customObj.Discount= poData.podetailModels[i].discount;
          customObj.Amount=poData.podetailModels[i].amount;
          customObj.Userid = poData.podetailModels[i].userid;
          customObj.Createddate = poData.podetailModels[i].createddate;
          customObj.Isdeleted =poData.podetailModels[i].isdeleted;
          customObj.Podetailid =poData.podetailModels[i].podetailid;
          customObj.poId = poData.podetailModels[i].poid;
          this.materialFillGrid.push(customObj);
          this.materialSaveList.push(customObj);
    }
    this.checkAddOrEdit=checkLabelt;
  }

  getDiscount(number:any){
    debugger;
    this.discount = number === "0" ||  number === ""  ? 0 : number;
    if(this.discount === 0  ){
      this.amount = this.quantity * this.rate;
    } else if(this.discount > 0) {
      this.amount = this.quantity * this.rate;
      this.amount = this.amount - (this.amount * this.discount / 100);
    }
  
  }

  deletematerialonGird(i:number){
    this.totalAmountText -= this.materialFillGrid[i].Amount;
    if(this.discountMaster === 0){
      this.finalAmount = this.totalAmountText;
    } else if (this.discountMaster > 0){
      this.finalAmount = this.totalAmountText - (this.totalAmountText * this.discountMaster / 100);
    }
    this.materialFillGrid.splice(i,1);
    this.materialSaveList.splice(i,1);
  }

  addData(){
    var addPoModel = new purchaseOrdermodel();
    addPoModel.pomasterModel = new purchaseOrderMaster();
    addPoModel.pomasterModel.Poid= this.poid;
    addPoModel.pomasterModel.Podate = this.dateofBirth ;
    addPoModel.pomasterModel.Totalamount = this.totalAmountText;
    addPoModel.pomasterModel.Discount = this.discountMaster;
    addPoModel.pomasterModel.Finalamount = this.finalAmount;
    addPoModel.pomasterModel.Userid = '1';
    addPoModel.pomasterModel.Createddate = new Date();
    addPoModel.pomasterModel.Isdeleted = "1";
    addPoModel.podetailModel = new Array<purchaseDetails>();
    addPoModel.podetailModel = this.materialSaveList;
    if(this.checkAddOrEdit !=="edit"){
      addPoModel.pomasterModel.Poid= 0;
      this.purchaseService.postRequest(addPoModel)
      .subscribe(
        success => {
         this.notificationService.success("Successfully Saved");
         this.openPoList();
       },
        error => {
       }
      );
    } else {
      this.purchaseService.patchRequest(addPoModel,this.poid)
      .subscribe(
        success => {
         this.notificationService.success("Successfully Saved");
         this.openPoDetails();
       },
        error => {
       }
      );
    }
    
  }

  PurchaseList(){
    this.purchaseService.getPodetail()
     .subscribe(
      data => {
        this.dataSource.data = data;
        console.log(this.dataSource.data);
      }
    );
  }

  addDataOnGrid(){
    let customObj = new materialGrid();
    customObj.Productid=this.productId;
    customObj.ProductName = this.ProductName ;
    customObj.Quantity=this.quantity;
    customObj.PoRate=this.rate;
    customObj.Discount= this.discount;
    customObj.Amount=this.amount;
    customObj.Userid = "1";
    customObj.Createddate = new Date();
    customObj.Isdeleted ="true";
    customObj.poId =this.poid;
    this.totalAmountText +=  customObj.Amount;
    if(this.discountMaster === 0){
      this.finalAmount = this.totalAmountText;
    } else if (this.discountMaster > 0){
      this.finalAmount = this.totalAmountText - (this.totalAmountText * this.discountMaster / 100);
    }
    this.materialFillGrid.push(customObj);
    this.materialSaveList.push(customObj);
  }




 

  

}
