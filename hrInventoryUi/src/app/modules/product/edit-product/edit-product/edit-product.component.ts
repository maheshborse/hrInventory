import { Component, OnInit, Optional, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { ProductCategoryService } from 'src/app/shared/services/product-category.service';
import * as _ from 'lodash';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ProductService } from 'src/app/shared/services/product.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {
  selectedCategry=[];
  catagryOption=[];
  dummyProduct =  {
    productid:0,
    productname:"",
    productdescription:"",
    categoryid:"",
    userid:1,
    createddate:new Date,
    Isdeleted:"1"
  }
  constructor(public dialogRef: MatDialogRef<EditProductComponent>,private productCategoryService:ProductCategoryService,
    private productService:ProductService, @Optional()  @Inject(MAT_DIALOG_DATA) data:any) { 
    if(data){
      this.dummyProduct.productid = data.productid;
      this.dummyProduct.productname = data.productname;
      this.dummyProduct.productdescription = data.productdescription;
      this.dummyProduct.categoryid = data.categoryid;
      this.dummyProduct.userid=1;
      this.dummyProduct.createddate = new Date();
      this.dummyProduct.Isdeleted = "1";
    }
  }

  ngOnInit() {
    this.catategorydetails();
  }

  productNameValidator: FormControl =  new FormControl('', [Validators.required]);
  productDescriptionValidator: FormControl =  new FormControl('', [Validators.required]);
  categryNameValidator: FormControl =  new FormControl('', [Validators.required]);

  productForm: FormGroup = new FormGroup({
    productName: this.productNameValidator,
  });

  catategorydetails(){
    this.productCategoryService.getCategory()
    .subscribe(
      data => {
          this.selectedCategry=data;
          this.catagryOption = _.map(this.selectedCategry, item =>{
            const option = {
              value:'',
              label:''
            };
            option.value = item.categoryid;
            option.label = `${item.categoryname}`;
            return option;
            });
          },
          error => {
          }
    )
  }

  changeCategory(selecteddata:any){
    this.dummyProduct.categoryid = selecteddata;
  }

  clickEditOrSave(){
    debugger;
    if (this.dummyProduct.productid === 0) {
     this.dummyProduct.Isdeleted="1";
     debugger;
     this.productService.postRequest(this.dummyProduct)
     .subscribe(
       success => {
        this.dialogRef.close(this.dummyProduct);
        
      },
       error => {
        
       }
     );
    } else{
       this.productService.patchRequest(this.dummyProduct)
       .subscribe(
         success => {
          this.dialogRef.close(this.dummyProduct);
         },
         error => {
         }
       );
     }
   }




}
