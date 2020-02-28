import { Component, OnInit, Optional, Inject } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import{ProductCategoryService} from '../../../shared/services/product-category.service'

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent implements OnInit {
 
  dummyCategory =  {
    categoryid: 0,
    categoryname:"",
    categorydescription:"",
    Userid: 1,
    creadtedDate:new Date(),
    isdeleted:"false"
  }
  userInfo:any;

  constructor( public dialogRef: MatDialogRef<EditCategoryComponent>,
    private productCategoryService:ProductCategoryService, @Optional()  @Inject(MAT_DIALOG_DATA) data:any) {
    
      if(data){
      this.dummyCategory.categoryid = data.categoryid;
      this.dummyCategory.categoryname = data.categoryname;
      this.dummyCategory.categorydescription = data.categorydescription;
      this.dummyCategory.Userid = data.userid;
      this.dummyCategory.creadtedDate = data.creadteddate;
      this.dummyCategory.isdeleted=data.isdeleted;
    }

   }

  ngOnInit() {
    this.userInfo = JSON.parse(localStorage.getItem("user"));
  }

  categoryNameValidator: FormControl =  new FormControl('', [Validators.required, this.noWhitespaceValidator]);

  public noWhitespaceValidator(control: FormControl) {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { 'whitespace': true };
  }
  
  categoryForm: FormGroup = new FormGroup({
    category_name: this.categoryNameValidator,
  });


  clickEditOrSave(){
   
   if (this.dummyCategory.categoryid === 0) {
    this.dummyCategory.isdeleted="false";
    this.dummyCategory.Userid =this.userInfo.id;
    this.productCategoryService.postRequest(this.dummyCategory)
    .subscribe(
      success => {
        this.dialogRef.close();
      },
      error => {
        
      }
    );
   } else{
      this.productCategoryService.patchRequest(this.dummyCategory)
      .subscribe(
        success => {
          this.dialogRef.close();
        },
        error => {
         
        }
      );
    }
  }
  
}
