import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatTableDataSource, MatIconRegistry, MatRadioChange } from '@angular/material';
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
  chooseStatus :string;
  searchOption:string="";
  requestDetails :Array<requestDetailonGrid> =[];
  tempFilter = [];
  checkStock:any=[];
  
  constructor(public dialog: MatDialog,public request:RequestService,private notificationService : NotificationService,iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    
  }

  ngOnInit() {
    this.userInfo = JSON.parse(localStorage.getItem("user"));
    this.productList();
    this.checkIsAdmin = this.userInfo.isAdmin;
  }


  openDialog(element:RequestViewModel,event:any){
    const dialogRef = this.dialog.open(EditRequestComponent,{
      width: '550px',
      panelClass: 'full-width-dialog',
      disableClose: true,
      data: {element,event},
      
    });
    
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      if(result === "error"){
      } else {
        if(result !== ""){
          this.productList();
          this.notificationService.success("Request Successfully Saved")
        }
      }
    });
  }

  productList(){
    this.request.getRequestdetail()
     .subscribe(
      data => {
        var showonGrid = new showongrid();
        debugger;
        showonGrid.Reqestmastermodelongrid = new Array<showOnGridmaster>();
        for (let index = 0; index < data.length; index++) {
          if(data[index].employeeid ==  this.userInfo.id || this.userInfo.isAdmin == true ){
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
              child.Productid = data[index].requestDetailModels[i].productid;
              child.ProductName = data[index].requestDetailModels[i].productModels.productname;
              child.Status  =data[index].requestDetailModels[i].status;
              child.Quantity = data[index].requestDetailModels[i].quantity;
              child.Isdeleted = data[index].requestDetailModels[i].isdeleted;
              child.Userid = data[index].requestDetailModels[i].userid;
              child.Createddate =data[index].requestDetailModels[i].createddate;
              child.Requestid =data[index].requestDetailModels[i].requestid;
              child.Requestdetailid = data[index].requestDetailModels[i].requestdetailid;
              this.checkStock.push(data[index].requestDetailModels[i].productModels)
              customObj.RequestdetailModelongrid.push(child);
              this.requestDetails.push(child);
              
            }
          }
        }
        
          debugger;
          this.dataSource.data = showonGrid.Reqestmastermodelongrid;
          this.tempFilter = this.dataSource.data;
          
          
      }
     );
  }

  radioChange(event:any,data:any,id:any){
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
           debugger;
            if(data.RequestdetailModelongrid[index].Status === "Approved" || data.RequestdetailModelongrid[index].Status=="Pending"){
            var  stock = this.checkStock.filter(k=> k.productid === addrequestViewModel.RequestdetailModel[index].Productid);
            var  curruntStock = stock[0].balance; 
            data.RequestdetailModelongrid[index].Status = "Pending";
            }
            let  alradyapproved =false;
            for (let j = 0; j < this.requestDetails.length; j++) {
              if(this.requestDetails[j].Productid === addrequestViewModel.RequestdetailModel[index].Productid  &&  this.requestDetails[j].Status === "Approved" ){
                alradyapproved =true;
              }
            }
            if(alradyapproved == true){
              this.notificationService.error("Please dispatch already approved record for selected product");
            } else  if(curruntStock === 0 && event !== 'Out of Stock'){
              this.notificationService.error("You are not able to change status beacuse  is " + curruntStock );
            } else {
                data.RequestdetailModelongrid[index].Status = event;
                this.request.patchRequest(addrequestViewModel,id).subscribe(
                  success => {
                    this.notificationService.success("Status Set Succesfully for selected product");
                  },
                  error => {
                }
              );
            }
        }
      }
  }
 

  applyFilter(filterValue: string) {
    const val = this.searchOption.toLowerCase().trim();
    this.dataSource.data = this.tempFilter.filter(function (d:any) {
        return d.EmployeeName.toLowerCase().indexOf(val) !== -1 || !val;
    });
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
