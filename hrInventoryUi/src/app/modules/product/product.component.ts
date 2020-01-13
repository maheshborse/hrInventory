import { Component, OnInit,ViewChild } from '@angular/core';
import {MatDialog,MatDialogRef,MatDialogModule } from '@angular/material';
import { MatSort, MatTableDataSource, MatPaginator } from '@angular/material';
import { user } from 'src/app/shared/models/user';
import { EditProductComponent } from './edit-product/edit-product/edit-product.component';


const ELEMENT_DATA: user[] = [
  {firstName: "Mahesh", lastName: 'Borse', email: "mahesh.borse87@gmial.com", address:"23",phone:'111',userImage:"",role:1,gender:"male"},
  {firstName: "test", lastName: 'Helium', email: "test@test.com", address:"",phone:'111',userImage:"",role:1,gender:"female"},
  {firstName: "ABC", lastName: 'Lithium', email: "test@test.com", address:"",phone:'111',userImage:"",role:1,gender:"male"},
  {firstName: "rty", lastName: 'Beryllium', email: "test@test.com", address:"",phone:'111',userImage:"",role:1,gender:"female"},
  {firstName: "rty", lastName: 'Beryllium', email: "test@test.com", address:"",phone:'111',userImage:"",role:1,gender:"female"},
  {firstName: "rty", lastName: 'Beryllium', email: "test@test.com", address:"",phone:'111',userImage:"",role:1,gender:"female"},
];

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'address','phone','userImage','role','gender','action'];
  dataSource: MatTableDataSource<user>;
 
  constructor(public dialog: MatDialog) {
    this.dataSource = new MatTableDataSource(ELEMENT_DATA);
  }


  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  openDialog(element:user){
    
    const dialogRef = this.dialog.open(EditProductComponent,{
      width: '640px',
      panelClass: 'full-width-dialog',
      disableClose: true,
       data: element
    });
    
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result.lastName}`);
    });

 
  }

}
