import { Component, OnInit, Optional, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { dispatchmaterialGrid } from 'src/app/shared/models/dispatch-to-employee';

@Component({
  selector: 'app-show-dispatch-info',
  templateUrl: './show-dispatch-info.component.html',
  styleUrls: ['./show-dispatch-info.component.scss']
})
export class ShowDispatchInfoComponent implements OnInit {
  dispatchFillGrid: Array<dispatchmaterialGrid> = [];
  constructor(public dialogRef: MatDialogRef<ShowDispatchInfoComponent>,@Optional()  @Inject(MAT_DIALOG_DATA) data:any) {

   debugger;

    for(var i=0 ; i< data.element.dispatchdetailsModels.length;i++){
      let customObj = new dispatchmaterialGrid();
      customObj.Productid = data.element.dispatchdetailsModels[i].productid;
      let ProductName = data.detailsProduct.filter(k=> k.productid == data.element.dispatchdetailsModels[i].productid);
      customObj.ProductName = ProductName[0].productModels.productname;
      customObj.Quantity=data.element.dispatchdetailsModels[i].quantity;
      customObj.Dispatchdetailid =data.element.dispatchdetailsModels[i].dispatchdetailid;
      customObj.Dispatchid = data.element.dispatchdetailsModels[i].dispatchid;
      customObj.Userid = data.element.dispatchdetailsModels[i].userid;
      customObj.Createddate = data.element.dispatchdetailsModels[i].createddate;
      customObj.Isdeleted =data.element.dispatchdetailsModels[i].isdeleted;
      this.dispatchFillGrid.push(customObj); 
    }
   }

  ngOnInit() {
  }

}
