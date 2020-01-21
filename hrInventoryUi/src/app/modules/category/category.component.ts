import { Component, OnInit, ViewChild } from '@angular/core';
import { category } from 'src/app/shared/models/category';
import { MatSort, MatPaginator, MatTableDataSource, MatDialog, MatIconModule} from '@angular/material';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import Swal from 'sweetalert2';
import {ProductCategoryService} from '../../shared/services/product-category.service'

import { DataSource } from '@angular/cdk/table';
import { Observable } from 'rxjs';
import {MatSnackBar} from '@angular/material/snack-bar';
import { NotificationService } from 'src/app/shared/services/notification.service';


@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['category_name', 'category_description','action'];
  dataSource = new MatTableDataSource();
  searchKey:string;
  durationInSeconds = 5;

  constructor(public dialog: MatDialog,private productCategoryService:ProductCategoryService,private _snackBar: MatSnackBar,public notificationService:NotificationService) { 
     this.categoryList();
  }
  
  ngOnInit() {
    this.categoryList();
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  categoryList(){
    
    this.productCategoryService.getCategory()
     .subscribe(
      data => {
        this.dataSource.data = data;
       }
      );
  }

  openDialog(element:category){
    const dialogRef = this.dialog.open(EditCategoryComponent,{
      width: '500px',
      panelClass: 'full-width-dialog',
      disableClose: true,
      data: element
    });
    dialogRef.afterClosed().subscribe(result => {
     if(result !== ""){
        this.categoryList();
        this.notificationService.success("Successfully Saved user.")
      }
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  delete(id:number){
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't delete this ?",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.productCategoryService.deleteRequest(id)
        .subscribe(
            success => {
              this.notificationService.error("Successfully Deleted")
              this.categoryList();
            },
            error => {  
            }       
          );
      }
    })
    
  }
}

