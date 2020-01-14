import { Component, OnInit,ViewChild } from '@angular/core';
import {MatDialog,MatDialogRef,MatDialogModule } from '@angular/material';
import { MatSort, MatTableDataSource, MatPaginator } from '@angular/material';
import { product } from 'src/app/shared/models/product';
import { EditProductComponent } from './edit-product/edit-product/edit-product.component';
import Swal from 'sweetalert2';

const ELEMENT_DATA: product[] = [
  {id:1,productName:"Pen",categryName:"Stationery",ProductDescription:"Test"},
  {id:2,productName:"duster",categryName:"Stationery",ProductDescription:"Test"},
  {id:3,productName:"pencile",categryName:"Stationery",ProductDescription:"Test"},
  {id:4,productName:"notebook",categryName:"Stationery",ProductDescription:"Test"},
  {id:5,productName:"coffeemug",categryName:"Stationery",ProductDescription:"Test"},
  {id:6,productName:"Pen",categryName:"Stationery",ProductDescription:"Test"}
];

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['productName', 'categryName','ProductDescription','action'];
  dataSource: MatTableDataSource<product>;
 
  constructor(public dialog: MatDialog) {
    this.dataSource = new MatTableDataSource(ELEMENT_DATA);
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  openDialog(element:product){
    const dialogRef = this.dialog.open(EditProductComponent,{
      width: '500px',
      panelClass: 'full-width-dialog',
      disableClose: true,
       data: element
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result.lastName}`);
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  delete(){

    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        Swal.fire(
          'Deleted!',
          'Your file has been deleted.',
          'success'
        )
      }
    })
    
  }
}
