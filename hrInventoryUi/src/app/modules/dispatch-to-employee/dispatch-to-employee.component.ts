import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator, MatDialog } from '@angular/material';
import { ProductService } from 'src/app/shared/services/product.service';
import * as _ from 'lodash';
import { dispatchmaterialGrid, DispatchToEmployeemodel, dispatchToEmployeeMaster, dispatchDetails} from 'src/app/shared/models/dispatch-to-employee';
import { DispatchToEmployeeService } from 'src/app/shared/services/dispatch-to-employee.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';
import { RequestService } from 'src/app/shared/services/request.service';
import { requestDetailonGrid, RequestViewModel, requestMaster, requestDetail, fillrequestGirdData } from 'src/app/shared/models/request';
import { ShowDispatchInfoComponent } from './show-dispatch-info/show-dispatch-info.component';

@Component({
  selector: 'app-dispatch-to-employee',
  templateUrl: './dispatch-to-employee.component.html',
  styleUrls: ['./dispatch-to-employee.component.scss']
})

export class DispatchToEmployeeComponent implements OnInit {

  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['employeeName','dispatchdate','totalqty','action'];
  dataSource = new MatTableDataSource();
  dispatchDate :Date;
  checkAddDispatchDetails:boolean=false;
  hidemainGrid :boolean=false;
  checkAddOrEdit:any="";
  employeeOption:any;
  employeeName:string;
  selectedEmployee:any=[];
  ProductName:string;
  selectedProduct:any=[];
  productOption:any;
  categoryName:string ="";
  productId:number;
  dispatchFillGrid: Array<dispatchmaterialGrid> = [];
  dispatchid:number;
  quantity:number = 0;
  dispatchSaveList:any=[];
  totalQuantity:number=0;
  stock:number;
  employeeid:number;
  selectedValue: string;
  selectedRequestDetails:any=[];
  checkOOSStatus :any;
  fetchData: any = [];
  getallrequestData:any;
  requetDetailsForGetProduct:any=[];
  userInfo: any;
    
  constructor(private route: ActivatedRoute,private productService:ProductService,private dispatchToEmployeeService :DispatchToEmployeeService,private notificationService : NotificationService,public request:RequestService,public dialog: MatDialog) { 
    this.dispatchList();
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.userInfo = JSON.parse(localStorage.getItem("user"));
    this.dispatchList();
    this.getEmployee();
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
    

  dispatchList(){
    this.dispatchToEmployeeService.getDispatchdetail()
     .subscribe(
      data => {
        this.dataSource.data = data;
        this.dataSource.filterPredicate = function(data:any, filter: string): boolean {
            return data.employeeName.toLowerCase().includes(filter)
        };
      }
    );
  }

  getDispatchDataEdit(dispatchData:any,checkLabel:any){
      console.log( this.selectedRequestDetails);
      this.dispatchFillGrid =[];
      this.dispatchSaveList =[];
      this.dispatchid= dispatchData.dispatchid;
      this.dispatchDate = dispatchData.dispatchdate;
      this.employeeid = dispatchData.employeeid;
      this.employeeName = dispatchData.employeeName;
      this.checkAddDispatchDetails = true;
      this.hidemainGrid = true;
      for(var i=0 ; i< dispatchData.dispatchdetailsModels.length;i++){
        let customObj = new dispatchmaterialGrid();
        customObj.Productid=dispatchData.dispatchdetailsModels[i].productid;
        let data = this.selectedRequestDetails.filter(k=> k.productid == dispatchData.dispatchdetailsModels[i].productid && k.status == "delivered");
        customObj.ProductName = data[0].productname;
        customObj.Quantity=dispatchData.dispatchdetailsModels[i].quantity;
        customObj.Dispatchdetailid =dispatchData.dispatchdetailsModels[i].dispatchdetailid;
        customObj.Dispatchid = dispatchData.dispatchdetailsModels[i].dispatchid;
        customObj.Userid = this.userInfo.id;
        customObj.Createddate = new Date();
        customObj.Isdeleted ="false";
        this.dispatchFillGrid.push(customObj);
        this.dispatchSaveList.push(customObj);
        this.totalQuantity += dispatchData.dispatchdetailsModels[i].quantity
      }
      this.checkAddOrEdit=checkLabel;
  }

  openDispatchDetails(){
    this.dispatchDate = new Date();
    this.checkAddDispatchDetails = true;
    this.hidemainGrid = true;
    this.checkAddOrEdit = 'add';
    this.getdefault();
  }

  addData(){
    debugger;
    var adddispatchEmployeeModel = new DispatchToEmployeemodel();
    adddispatchEmployeeModel.DispatchmasterVmodel =new  dispatchToEmployeeMaster();
    adddispatchEmployeeModel.DispatchmasterVmodel.Dispatchid = this.dispatchid;
    adddispatchEmployeeModel.DispatchmasterVmodel.Dispatchdate = this.dispatchDate;
    adddispatchEmployeeModel.DispatchmasterVmodel.Employeeid = this.employeeid;
    adddispatchEmployeeModel.DispatchmasterVmodel.EmployeeName =this.employeeName;
    adddispatchEmployeeModel.DispatchmasterVmodel.Userid =this.userInfo.id;
    adddispatchEmployeeModel.DispatchmasterVmodel.Createddate=new Date;
    adddispatchEmployeeModel.DispatchmasterVmodel.Isdeleted = 'false';
    adddispatchEmployeeModel.DispatchmasterVmodel.Totalqty =this.totalQuantity;
    adddispatchEmployeeModel.DispatchdetailsVModel = new Array<dispatchDetails>();
    adddispatchEmployeeModel.DispatchdetailsVModel = this.dispatchSaveList;
    if(this.checkAddOrEdit !=="edit"){
      adddispatchEmployeeModel.DispatchmasterVmodel.Dispatchid= 0;
      this.dispatchToEmployeeService.postRequest(adddispatchEmployeeModel)
      .subscribe(
        success => {
         this.openDispatchList();
         this.deliveredStatus();
         this.notificationService.success("Successfully Saved");
      },
        error => {
       }
      );
    } else {
      this.dispatchToEmployeeService.patchRequest(adddispatchEmployeeModel,this.dispatchid)
      .subscribe(
        success => {
         this.openDispatchList();
         this.notificationService.success("Successfully Saved");
      },
        error => {
       }
      );
    
    }

  }
  
  changeEmployee(event:any){
    debugger;
    this.employeeid = event;
    // let data = this.fetchData.filter(k=> k.id === event);
    //this.employeeName =  data[0].displayName;
    this.requetDetailsForGetProduct =[];
    debugger;
    let  employeeSelected = this.getallrequestData.filter(k=>k.employeeid == event);
    for (let index = 0; index < employeeSelected.length; index++) {
      for (let j = 0; j < employeeSelected[index].requestDetailModels.length; j++) {
        
        if(employeeSelected[index].requestDetailModels[j].status == "Approved"){
          this.requetDetailsForGetProduct.push(employeeSelected[index].requestDetailModels[j]);
        }
        this.employeeName = employeeSelected[index].users[0].displayName;
      }   
    }
    
    this.detailsProduct(this.requetDetailsForGetProduct);
  }

  deliveredStatus(){
    var addrequestViewModel = new RequestViewModel();
    var  requestid;
    debugger;
    for (let index = 0; index < this.getallrequestData.length; index++) {
      for (let i = 0; i < this.getallrequestData[index].requestDetailModels.length; i++) {
        if(this.getallrequestData[index].requestDetailModels[i].status === "Approved" && this.getallrequestData[index].requestDetailModels[i].productid == this.productId ){
          addrequestViewModel.Reqestmastermodel =new  requestMaster();
          addrequestViewModel.Reqestmastermodel.Requestid = this.getallrequestData[index].requestid;
          addrequestViewModel.Reqestmastermodel.Employeeid = this.getallrequestData[index].employeeid;
          addrequestViewModel.Reqestmastermodel.Isread = this.getallrequestData[index].isread;
          addrequestViewModel.Reqestmastermodel.Createddate = this.getallrequestData[index].createddate;
          addrequestViewModel.Reqestmastermodel.Isdeleted = this.getallrequestData[index].isdeleted;
          addrequestViewModel.Reqestmastermodel.Userid = this.getallrequestData[index].userid;
          this.getallrequestData[index].requestDetailModels[i].status ="delivered";
          requestid = this.getallrequestData[index].requestDetailModels[i].requestdetailid;
          addrequestViewModel.RequestdetailModel = new Array<requestDetail>();
          let customObj = new fillrequestGirdData();
          customObj.Requestdetailid = this.getallrequestData[index].requestDetailModels[i].requestdetailid;
          customObj.Requestid = this.getallrequestData[index].requestDetailModels[i].requestid;
          customObj.Productid  = this.getallrequestData[index].requestDetailModels[i].productid; 
          customObj.Quantity =this.getallrequestData[index].requestDetailModels[i].quantity;
          customObj.Status = this.getallrequestData[index].requestDetailModels[i].status ;
          customObj.Isdeleted= this.getallrequestData[index].requestDetailModels[i].isdeleted;
          customObj.Userid = this.getallrequestData[index].requestDetailModels[i].userid;
          customObj.Createddate = this.getallrequestData[index].requestDetailModels[i].createddate;
          addrequestViewModel.RequestdetailModel.push(customObj);
        }
      }
    }
    this.request.patchRequest(addrequestViewModel,requestid).subscribe(
        success => {
          
        },
        error => {
      }
    );
  }

  getEmployee(){
    this.request.getRequestdetail()
     .subscribe(
      (data:Array<any>) => {
        for (let index = 0; index < data.length; index++) {
          this.getallrequestData = data;
          data = data.filter((el, i, a) => i === a.indexOf(el))
          for (let j = 0; j < data[index].requestDetailModels.length; j++) {
            this.selectedRequestDetails.push(data[index].requestDetailModels[j]) ;
            if(data[index].requestDetailModels[j].status == "Approved"){
              this.selectedEmployee.push(data[index].users);
            }
          }
        }
        this.EmployeeNameForDropDown(this.selectedEmployee);
        },
        error => {
      }
    );
  }
  EmployeeNameForDropDown(employee:any){
    
    for (let index = 0; index < employee.length; index++) {
     this.fetchData.push(employee[index][0]);
     this.fetchData =  this.fetchData.filter((obj:any, index, self) =>
       index === self.findIndex((el:any) => (
         el[index] == obj[index] &&  el.id == obj.id
      ))
     )
     this.employeeOption = _.map(this.fetchData, item =>{
      const option = {
        value:'',
        label:''
      };
      option.value = item.id;
      option.label = `${item.displayName}`;
      return option;
      });
    }
  }

  openDispatchList(){
    this.hidemainGrid = false;
    this.checkAddDispatchDetails = false;
    this.getdefault();
  }

  detailsProduct(product:any){
      this.selectedProduct=[];
      for (let i = 0; i < product.length; i++) {
        this.selectedProduct.push(product[i].productModels);
      }
      this.productOption = _.map(this.selectedProduct, item =>{
        const option = {
          value:'',
          label:''
      };
        option.value = item.productid;
        option.label = `${item.productname}`;
        return option;
      });
     
  }

  changeProduct(id:any){
    //this.detailsProduct();
    this.productId = id;
    let data = this.selectedProduct.filter(k=> k.productid === id );
    let getQuantiy = this.selectedRequestDetails.filter(t=>t.productid == id && t.status !== "delivered" && t.status !== "Out of Stock" && t.status !== "Pending");
    this.quantity = getQuantiy[0].quantity;
    this.checkOOSStatus = this.selectedRequestDetails.filter(j=>j.productid == id && j.status == "Out of Stock" );
    this.categoryName = data[0].category.categoryname;
    this.ProductName = data[0].productname;
    this.stock = data[0].balance;
    
  }

  deleteDispatchOnGird (i:number){
    this.totalQuantity -= this.dispatchFillGrid[i].Quantity;
    this.dispatchFillGrid.splice(i,1);
    //this.materialSaveList.splice(i,1);
    this.dispatchSaveList[i].isdeleted = "true";
   
  }

  addDataOnGrid(){
    debugger;
    let checkAlreadyExist = this.dispatchFillGrid.find(ob => ob.Productid === this.productId);
    let customObj = new dispatchmaterialGrid();
    customObj.Productid=this.productId;
    customObj.ProductName = this.ProductName;
    customObj.Quantity=this.quantity;
    customObj.Dispatchid =this.dispatchid;
    customObj.Userid = this.userInfo.id;
    customObj.Createddate = new Date();
    customObj.Isdeleted ="false";
    if(this.quantity > this.stock){
      this.notificationService.error("Quantity Should not be grater than currunt stock.");
      return false;
    } else if(checkAlreadyExist !== undefined){
      this.notificationService.error("Selected product"+'  ' + this.ProductName +' ' +"already Added.");
      return false;
    } if(this.checkOOSStatus.length > 0 ){
      this.notificationService.error("Please set approved status for selected product" +'  ' + this.ProductName +' ');
      return false;
    } 
    else {
      this.totalQuantity += customObj.Quantity;
      this.dispatchFillGrid.push(customObj);
      this.dispatchSaveList.push(customObj);
    }
     this.ProductName = "";
     this.quantity = 0;
     this.stock = 0;
     this.categoryName = "";
     this.selectedValue= "";
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
        this.dispatchToEmployeeService.deleteRequest(id)
        .subscribe(
            success => {
              this.notificationService.error("Successfully Deleted")
              this.dispatchList();
            },
            error => {  
            }       
          );
      }
    })
  }


  getdefault(){
    this.dispatchFillGrid=[];
    this.dispatchSaveList =[];
    this.totalQuantity = 0;
    this.quantity = 0 ;
    this.stock = 0;
    this.categoryName="";
    this.employeeName = "";
    this.ProductName = "";
    this.selectedValue ="";
  }

  showDialogue(element:dispatchmaterialGrid){
    var detailsProduct =this.selectedRequestDetails
    const dialogRef = this.dialog.open(ShowDispatchInfoComponent,{
      width: '550px',
      panelClass: 'full-width-dialog',
      disableClose: true,
      data: {element,detailsProduct},
      
    });
    
    dialogRef.afterClosed().subscribe(result => {
      
    });
  }
 
}