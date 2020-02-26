import { Component, OnInit,ViewChild } from '@angular/core';
import {MatDialog,MatDialogRef,MatDialogModule } from '@angular/material';
import { MatSort, MatTableDataSource, MatPaginator } from '@angular/material';
import { product, showOnGrid } from 'src/app/shared/models/product';
import { EditProductComponent } from './edit-product/edit-product/edit-product.component';
import Swal from 'sweetalert2';
import { ProductService } from 'src/app/shared/services/product.service';
import { ProductCategoryService } from 'src/app/shared/services/product-category.service';
import { NotificationService } from 'src/app/shared/services/notification.service';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['productname', 'categoryname','productdescription','action'];
  dataSource = new MatTableDataSource();
  categoryData : any;
  categoryName:any;
  showDataOnGrid: Array<showOnGrid> = [];
 
  constructor(public dialog: MatDialog,private productService:ProductService,private productCategoryService: ProductCategoryService,private notificationService : NotificationService) {
    this.productList();
  }

  ngOnInit() {
    this.productList();
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
 
  productList(){
  
    this.productService.getProduct()
     .subscribe(
      data => {
        this.showDataOnGrid =[];
        for (let index = 0; index < data.length; index++) {
          let  customObj = new  showOnGrid();
          customObj.productid = data[index].productid;
          customObj.productname= data[index].productname;
          customObj.categoryid = data[index].categoryid;
          customObj.categoryname = data[index].category.categoryname;
          customObj.productdescription = data[index].productdescription;
          customObj.userid=data[index].userid;
          customObj.isdeleted=data[index].isdeleted;
          customObj.createddate =data[index].createddate;
          this.showDataOnGrid.push(customObj);
        }
        this.dataSource.data =  this.showDataOnGrid;
      }
     
     );
    
  }
  
  openDialog(element:product){
    const dialogRef = this.dialog.open(EditProductComponent,{
      width: '500px',
      panelClass: 'full-width-dialog',
      disableClose: true,
       data: element
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      if(result !== ""){
        this.productList();
        this.notificationService.success("Successfully Saved")
      }
    });
    this.productList();
  }

  openDialogForAdd(){
    const dialogRef = this.dialog.open(EditProductComponent,{
      width: '500px',
      panelClass: 'full-width-dialog',
      disableClose: true,
      
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result.productname}`);
      if(result !== ""){
        this.productList();
        this.notificationService.success("Successfully Saved")
      }
    });
    this.productList();
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.toLowerCase().trim();
  }

  delete(id:any,name:any){
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
        this.productService.deleteRequest(id)
        .subscribe(
          success => {
            this.notificationService.error("Product "+ name + " deleted successfully")
            this.productList();
          },
          error => {  
          }       
        );
      }
    })
  }
}
