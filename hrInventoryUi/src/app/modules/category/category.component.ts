import { Component, OnInit, ViewChild } from '@angular/core';
import { category } from 'src/app/shared/models/category';
import { MatSort, MatPaginator, MatTableDataSource, MatDialog, MatIconModule} from '@angular/material';
import { EditCategoryComponent } from './edit-category/edit-category.component';

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

  constructor(public dialog: MatDialog) { 
    this.dataSource = new MatTableDataSource(Category_Data);
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  openDialog(element:category){
    
    const dialogRef = this.dialog.open(EditCategoryComponent,{
      width: '640px',
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

}
