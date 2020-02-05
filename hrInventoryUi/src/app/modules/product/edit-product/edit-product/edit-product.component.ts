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
    Isdeleted:"false"
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
      this.dummyProduct.Isdeleted = "false";
    }
  }

  ngOnInit() {
    this.catategorydetails();
  }

  productNameValidator: FormControl =  new FormControl('', [Validators.required, this.noWhitespaceValidator]);
  productDescriptionValidator: FormControl =  new FormControl('', [Validators.required, this.noWhitespaceValidator]);
  categryNameValidator: FormControl =  new FormControl('', [Validators.required, this.noWhitespaceValidator]);

  public noWhitespaceValidator(control: FormControl) {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { 'whitespace': true };
  }

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
     this.dummyProduct.Isdeleted="false";
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
