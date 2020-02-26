import { Component, OnInit, Optional, Inject } from '@angular/core';
import { ProductService } from 'src/app/shared/services/product.service';
import * as _ from 'lodash';
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
import { startWith, debounceTime, switchMap, map } from 'rxjs/operators';
import { fillrequestGirdData,  RequestViewModel, requestDetail, requestMaster, requestDetailonGrid, showOnGridmaster } from 'src/app/shared/models/request';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { RequestService } from 'src/app/shared/services/request.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-edit-request',
  templateUrl: './edit-request.component.html',
  styleUrls: ['./edit-request.component.scss']
})
export class EditRequestComponent implements OnInit {
  myControl = new FormControl();
  selectedProduct :any; 
  productOption :any;
  categoryName :string="";
  productId:number=0;
  requestFillGrid: Array<fillrequestGirdData> = [];
  quantity :number=0;
  requestSaveList:any=[];
  userInfo: any;
  productName:string;
  checkAlredy:boolean=false ;
  selectedValue:string;
  fetchData: Array<requestDetailonGrid> = [];
  checkEdit :boolean =false;
  getAllData:Array<showOnGridmaster> =[];
  requestId:number;
  
  constructor(private productService:ProductService,private notificationService : NotificationService,public request:RequestService,public dialogRef: MatDialogRef<EditRequestComponent>,@Optional()  @Inject(MAT_DIALOG_DATA) data:any) {
    if(data.event === 'edit' ){
      for(var i=0 ; i< data.element.RequestdetailModelongrid.length;i++){
        this.fetchData.push(data.element.RequestdetailModelongrid[i]);
      }
      this.checkEdit = true;
     } else {
       this.checkEdit =false;
       this.getAllData.push(data);
    }
    
   }

  ngOnInit() {
    this.userInfo = JSON.parse(localStorage.getItem("user"));
    this.detailsProduct();
    this.fetchDataonGrid();
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
  

  changeProduct(id:any){
    
    this.detailsProduct();
    this.productId = id;
    let data = this.selectedProduct.filter(k=> k.productid === id );
    this.categoryName = data[0].category.categoryname;
    this.productName = data[0].productname;
  }

  clickEditOrSave(){
    debugger;
      if(this.checkEdit === false){
        this.checkAlredyExist(this.getAllData);
      } 
      var addrequestViewModel = new RequestViewModel();
      addrequestViewModel.Reqestmastermodel =new  requestMaster();
      addrequestViewModel.Reqestmastermodel.Employeeid = this.userInfo.id;
      addrequestViewModel.Reqestmastermodel.Isread = false;
      addrequestViewModel.Reqestmastermodel.Createddate =new Date();
      addrequestViewModel.Reqestmastermodel.Isdeleted = "false";
      addrequestViewModel.Reqestmastermodel.Userid = this.userInfo.id;
      addrequestViewModel.RequestdetailModel = new Array<requestDetail>();
      addrequestViewModel.RequestdetailModel = this.requestSaveList;
      if(this.checkAlredy === true){
        this.notificationService.error("Selected Produt is already  Added ");
        this.dialogRef.close('error');
      } else{
        if(this.checkEdit === false ){
          this.request.postRequest(addrequestViewModel).subscribe(
            success => {
              
            },
            error => {
           }
    
          );
        } else if (this.checkEdit == true) {
          addrequestViewModel.Reqestmastermodel.Requestid = this.requestId;
          this.request.patchRequest(addrequestViewModel,this.requestId).subscribe(
            success => {
              
            },
            error => {
           }
    
          );
        } 
      }
  }

  fetchDataonGrid(){
     this.requestFillGrid= [];
     this.requestSaveList = [];
     for(var i=0 ; i< this.fetchData.length;i++){
      let customObj = new fillrequestGirdData();
      customObj.Productid = this.fetchData[i].Productid;
      customObj.productName = this.fetchData[i].ProductName;
      customObj.Employeeid  = this.userInfo.id; 
      customObj.Isread = false;
      customObj.Quantity =this.fetchData[i].Quantity;
      customObj.Status = this.fetchData[i].Status;
      customObj.Isdeleted= this.fetchData[i].Isdeleted;
      customObj.Userid = this.userInfo.id;
      customObj.Createddate =this.fetchData[i].Createddate;
      customObj.Requestid = this.fetchData[i].Requestid;
      customObj.Requestdetailid = this.fetchData[i].Requestdetailid;
      this.requestFillGrid.push(customObj);
      this.requestSaveList.push(customObj);
      this.requestId =  this.fetchData[i].Requestid;

     }
      
  }

  deletematerialonGird(i:any){
    this.requestFillGrid.splice(i,1);
    //this.materialSaveList.splice(i,1);
    this.requestSaveList[i].isdeleted = "true";
  }
  

  addDataOnGrid(){
    let checkAlreadyExist = this.requestFillGrid.find(ob => ob.Productid === this.productId);
    if(checkAlreadyExist !== undefined){
      this.notificationService.error("Selected product"+'  ' + this.productName +' ' +"already Added.")
    } else {
      let customObj = new fillrequestGirdData();
      customObj.Productid = this.productId;
      customObj.productName = this.productName;
      customObj.Employeeid  = this.userInfo.id; 
      customObj.Isread = false;
      customObj.Quantity =this.quantity;
      customObj.Status = "Pending";
      customObj.Isdeleted= "false";
      customObj.Userid = this.userInfo.id;
      customObj.Isdeleted = "false";
      customObj.Createddate = new Date();
      customObj.Requestid = this.requestId;
      this.requestFillGrid.push(customObj);
      this.requestSaveList.push(customObj);
      this.productName = "";
      this.quantity =0;
      this.categoryName = "";
      this.selectedValue="";
    }
    
  }


  checkAlredyExist(all:any){
   
      for (let index = 0; index < all[0].element.length; index++) {
        for (let i = 0; i < all[0].element[index].RequestdetailModelongrid.length; i++) {
          if (all[0].element[index].RequestdetailModelongrid[i].Productid === this.productId && (all[0].element[index].RequestdetailModelongrid[i].Status === "Out of Stock" || all[0].element[index].RequestdetailModelongrid[i].Status ==="Pending" || all[0].element[index].RequestdetailModelongrid[i].Status =="Approved" )){
            this.checkAlredy = true;
          } 
        }
      }
  }

   
}
