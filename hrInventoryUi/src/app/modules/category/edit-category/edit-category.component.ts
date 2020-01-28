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


  constructor( public dialogRef: MatDialogRef<EditCategoryComponent>,
    private productCategoryService:ProductCategoryService, @Optional()  @Inject(MAT_DIALOG_DATA) data:any) {
    
      if(data){
      this.dummyCategory.categoryid = data.categoryid;
      this.dummyCategory.categoryname = data.categoryname;
      this.dummyCategory.categorydescription = data.categorydescription;
      this.dummyCategory.Userid = 1;
      this.dummyCategory.creadtedDate = new Date();
      this.dummyCategory.isdeleted=data.isdeleted;
    }

   }

  ngOnInit() {
  }

  categoryNameValidator: FormControl =  new FormControl('', [Validators.required]);
  
  categoryForm: FormGroup = new FormGroup({
    category_name: this.categoryNameValidator,
  });


  clickEditOrSave(){
   if (this.dummyCategory.categoryid === 0) {
    this.dummyCategory.isdeleted="false";
    this.productCategoryService.postRequest(this.dummyCategory)
    .subscribe(
      success => {
       this.dialogRef.close(this.dummyCategory);
      },
      error => {
       
      }
    );
   } else{
      this.productCategoryService.patchRequest(this.dummyCategory)
      .subscribe(
        success => {
         this.dialogRef.close(this.dummyCategory);
        },
        error => {
         
        }
      );
    }
  }
}
