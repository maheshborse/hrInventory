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
  displayedColumns: string[] = ['categoryname', 'categorydescription','action'];
  dataSource = new MatTableDataSource();
  searchKey:string;
  userInfo:any;
  durationInSeconds = 5;
  tempFilter =[];
  

  constructor(public dialog: MatDialog,private productCategoryService:ProductCategoryService,private _snackBar: MatSnackBar,public notificationService:NotificationService) { 
     this.categoryList();
  }
  
  ngOnInit() {
    debugger;
    this.userInfo = JSON.parse(localStorage.getItem("user"));
    this.categoryList();
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  categoryList(){
    
    this.productCategoryService.getCategory()
     .subscribe(
      data => {
          this.dataSource.data = data;
          this.tempFilter = this.dataSource.data;
        }
      );
  }

  openDialogForAdd(){
    const dialogRef = this.dialog.open(EditCategoryComponent,{
      width: '500px',
      panelClass: 'full-width-dialog',
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((result :any) => {
      this.categoryList();
      if(result !== "" ){
        this.notificationService.success("Category"+" "+`${dialogRef.componentInstance.dummyCategory.categoryname}`+"  " +"Successfully saved.");
      }
    });
    
  }

  openDialog(element:category){
   
    const dialogRef = this.dialog.open(EditCategoryComponent,{
      width: '500px',
      panelClass: 'full-width-dialog',
      disableClose: true,
      data: element
    });
    dialogRef.afterClosed().subscribe(result => {
      this.categoryList();
      if(result !== "" ){
        this.notificationService.success("Category"+" "+`${dialogRef.componentInstance.dummyCategory.categoryname}`+" "+"Successfully saved.");
      }
    });
    
  }

  applyFilter(filterValue: string) {
    const val = filterValue.toLowerCase().trim();
    this.dataSource.data = this.tempFilter.filter(function (d:any) {
        return d.categoryname.toLowerCase().indexOf(val) !== -1 || !val;
    });
  }

  delete(id:any,name:any){
    Swal.fire({
      text: "Are you sure? You want to delete this Category?",
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
            this.notificationService.error("Category "+ name + " deleted successfully")
                      this.categoryList();
                    },
            error => {  
            }       
          );
      }
    })
    
  }
}

