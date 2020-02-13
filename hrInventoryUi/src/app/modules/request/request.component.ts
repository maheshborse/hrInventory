import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatTableDataSource, MatIconRegistry } from '@angular/material';
import { RequestViewModel, requestDetailonGrid, showongrid, showOnGridmaster, requestMaster, requestDetail } from 'src/app/shared/models/request';
import { EditRequestComponent } from './edit-request/edit-request.component';
import { RequestService } from 'src/app/shared/services/request.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import Swal from 'sweetalert2';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss']
})
export class RequestComponent implements OnInit {


  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator,{static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['ProductName','Quantity','Status'];
  dataSource = new MatTableDataSource();
  panelOpenState = false;
  userInfo:any;
  checkIsAdmin:boolean =false;
  constructor(public dialog: MatDialog,public request:RequestService,private notificationService : NotificationService,iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {iconRegistry.addSvgIcon(
    'thumbs-up',
    sanitizer.bypassSecurityTrustResourceUrl('assets/images/thumbup-icon.svg'));
  
  }

  ngOnInit() {
    this.userInfo = JSON.parse(localStorage.getItem("user"));
    this.productList();
    this.checkIsAdmin = this.userInfo.isAdmin;
  }


  openDialog(element:RequestViewModel){
   
    const dialogRef = this.dialog.open(EditRequestComponent,{
      width: '550px',
      panelClass: 'full-width-dialog',
      disableClose: true,
      data: element
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      if(result !== ""){
        this.productList();
        this.notificationService.success("Request Successfully Saved")
      }
    });
  }

  productList(){
  
    this.request.getRequestdetail()
     .subscribe(
      data => {
        var showonGrid = new showongrid();
        showonGrid.Reqestmastermodelongrid = new Array<showOnGridmaster>();
        for (let index = 0; index < data.length; index++) {
           let  customObj = new  showOnGridmaster();
           customObj.Requestid =data[index].requestid;
           customObj.Employeeid =data[index].employeeid;
           customObj.EmployeeName = data[index].users[0].displayName;
           customObj.Isread =data[index].isread;
           customObj.Userid=data[index].userid;
           customObj.Createddate =data[index].createddate;
           customObj.Isdeleted =data[index].isdeleted;
           customObj.RequestdetailModelongrid =[];
           showonGrid.Reqestmastermodelongrid.push(customObj);
          for (let i = 0; i < data[index].requestDetailModels.length; i++) {
              let child = new requestDetailonGrid()
              child.Productid = data[index].requestDetailModels[i].requestdetailid;
              child.ProductName = data[index].requestDetailModels[i].productModels.productname;
              child.Status  =data[index].requestDetailModels[i].status;
              child.Quantity = data[index].requestDetailModels[i].quantity;
              child.Isdeleted = data[index].requestDetailModels[i].isdeleted;
              child.Userid = data[index].requestDetailModels[i].userid;
              child.Createddate =data[index].requestDetailModels[i].createddate;
              child.Requestid =data[index].requestDetailModels[i].requestid;
              child.Requestdetailid = data[index].requestDetailModels[i].requestdetailid;
              customObj.RequestdetailModelongrid.push(child);
            }
          }
          this.dataSource.data = showonGrid.Reqestmastermodelongrid;
          this.dataSource.filterPredicate = function(data:any, filter: string): boolean {
            return data.Employeeid.toLowerCase().includes(filter)
          };
       
          console.log(this.dataSource.data);
      }
     );
  }

  slide(data:any,id:any){
      console.log("asdasda",id);
      console.log("sdasdasd",data);
      debugger;
      var addrequestViewModel = new RequestViewModel();
      addrequestViewModel.Reqestmastermodel =new  requestMaster();
      addrequestViewModel.Reqestmastermodel.Requestid = data.Requestid;
      addrequestViewModel.Reqestmastermodel.Employeeid = data.Employeeid;
      addrequestViewModel.Reqestmastermodel.Isread = data.Isread;
      addrequestViewModel.Reqestmastermodel.Createddate =data.Createddate;
      addrequestViewModel.Reqestmastermodel.Isdeleted = data.Isdeleted;
      addrequestViewModel.Reqestmastermodel.Userid = data.Userid;
      addrequestViewModel.RequestdetailModel = new Array<requestDetail>();
      addrequestViewModel.RequestdetailModel = data.RequestdetailModelongrid;
      for (let index = 0; index < addrequestViewModel.RequestdetailModel.length; index++) {
        if(addrequestViewModel.RequestdetailModel[index].Requestdetailid === id ){
            data.RequestdetailModelongrid[index].Status = "Approved";
            this.request.patchRequest(addrequestViewModel,id).subscribe(
            success => {
            
              },
            error => {
            }
          );
        }
      }
      console.log("dsasdasdsdasd",addrequestViewModel.RequestdetailModel);
    
  }
  
  

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;

  }
  

  delete(i:any){
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
        this.request.deleteRequest(i)
        .subscribe(
          success => {
            this.notificationService.error("Request deleted successfully")
            this.productList();
          },
          error => {  
          }       
        );
      }
    })
  }
  

   

}
