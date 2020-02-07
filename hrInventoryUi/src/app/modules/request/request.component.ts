import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { request } from 'src/app/shared/models/request';
import { EditRequestComponent } from './edit-request/edit-request.component';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss']
})
export class RequestComponent implements OnInit {


  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['productName', 'categryName','ProductDescription','action'];
  
  constructor(public dialog: MatDialog) { }

  ngOnInit() {
  }


  openDialog(element:request){
    const dialogRef = this.dialog.open(EditRequestComponent,{
      width: '550px',
      panelClass: 'full-width-dialog',
      disableClose: true,
       data: element
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      if(result !== ""){
       
      }
    });
  }

  applyFilter(value:any){

  }
  

  delete(i:any){

  }

}
