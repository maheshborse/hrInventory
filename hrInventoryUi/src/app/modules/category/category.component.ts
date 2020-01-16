import { Component, OnInit, ViewChild } from '@angular/core';
import { category } from 'src/app/shared/models/category';
import { MatSort, MatPaginator, MatTableDataSource, MatDialog, MatIconModule} from '@angular/material';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import Swal from 'sweetalert2';

import { DataSource } from '@angular/cdk/table';
import { Observable } from 'rxjs';
import { ProductCategoryService } from 'src/app/shared/services/product-category.service';

const Category_Data:category[]=[
  {category_name:"ABC",category_description:"aaaaaaaaaaaaaaaaaaaaaa"},
  {category_name:"PQR",category_description:"bbbbbbbbbbbbbbbbbbbbbb"},
  {category_name:"IOP", category_description:"ccccccccccccccccccccc"},
  {category_name:"JKL",category_description:"dddddddddddddddddddddd"},
  {category_name:"FGH",category_description:"eeeeeeeeeeeeeeeeeeeeee"},
  {category_name:"SDF",category_description:"ffffffffffffffffffffff"}
];

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['category_name', 'category_description','action'];
  
  dataSource: MatTableDataSource<category>;
 
  searchKey:string;

  constructor(public dialog: MatDialog,private productCategoryService:ProductCategoryService) { 
    this.dataSource = new MatTableDataSource(Category_Data);
   //this.dataSource = new MatTableDataSource(this.productCategoryService.connect());
  }
  // connect():Observable<category[]>{
  //   return this.productCategoryService.getCategory();
  // }


  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  openDialog(element:category){
    const dialogRef = this.dialog.open(EditCategoryComponent,{
      width: '500px',
      panelClass: 'full-width-dialog',
      disableClose: true,
      data: element
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result.category_name}`);
    });
  }

  
  onSearchClear(){
    this.searchKey="";
    this.applyFilter();
  }

  applyFilter(){
    this.dataSource.filter=this.searchKey.trim().toLowerCase();
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

