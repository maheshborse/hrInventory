import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { ProductService } from 'src/app/shared/services/product.service';
import * as _ from 'lodash';
import { dispatchmaterialGrid, DispatchToEmployeemodel, dispatchToEmployeeMaster, dispatchDetails} from 'src/app/shared/models/dispatch-to-employee';
import { DispatchToEmployeeService } from 'src/app/shared/services/dispatch-to-employee.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';





@Component({
  selector: 'app-dispatch-to-employee',
  templateUrl: './dispatch-to-employee.component.html',
  styleUrls: ['./dispatch-to-employee.component.scss']
})




export class DispatchToEmployeeComponent implements OnInit {

  checkAddDispatchDetails : boolean =false;
  hidemainGrid:boolean =false;
  productOption:any;
  productId:number;
  categoryName:string ="";
  ProductName:string;
  dispatchdate: Date;
  quantity:number = 0;
  selectedProduct:any;
  totalQuantity: number;
  
  materialSaveList:any=[];
  materialFillGrid: Array<dispatchmaterialGrid> = [];
  dateofBirth :Date;
  

  checkAddOrEdit:any="";

  //clear
  Employeename: string;
  Productname: string="";
  Stock: any;
  Quantity: any=null;



  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  displayedColumns: string[] = ['employeeName','dispatchDate','totalQuantity','action'];
  dataSource = new MatTableDataSource();

  totalQuantityText: number;
  dpid: number;
 
  selectedEmployee: any;
  Displayname: any;
  employeeOption: any=[];
  stockOption:any=[];
  DIsplayname: any;
  ID: any;
  Id: number;
  dispatchid:number;
  employeeid: number;
  selectedStock: any;
  selectedProductStock: any;
  poid: any;
  employeeName: string;
  
  
  
  

  constructor(private route: ActivatedRoute,private productService:ProductService,private dispatchToEmployeeService :DispatchToEmployeeService,private notificationService : NotificationService) { 
    //this.DispatchList();
   
  }

  ngOnInit() {
    this.detailsProduct();
    this.DispatchList();
    this.detailsEmployee();
    //this.DispatchStockList();
    this.dataSource.paginator = this.paginator;
  }

  DispatchList(){
    debugger;
    this.dispatchToEmployeeService.getDispatchdetail()
     .subscribe(
      data => {
        this.dataSource.data = data;
        console.log(data);
      }
    );
  }

  changeEmployee(id:any){
    this.ID=id;
    let data = this.selectedEmployee.filter(k=> k.Id=== id);
    this.DIsplayname = data[0].DisplayName;
  }

   detailsEmployee(){
     
    this.dispatchToEmployeeService.getDispatchEmpdetail()
     .subscribe(
      (data:Array<any>) => {
        this.selectedEmployee=data['ActiveUsers'];
        console.log(this.selectedEmployee);
        //this.details= data['ActiveUsers'];
        this.employeeOption = _.map(this.selectedEmployee, item =>{
          const option = {
            value:'',
            label:''
          };
          option.value = item.Id;
          option.label = `${item.DisplayName}`;
          return option;
          });
        },
        error => {
      }
    );
   }

   
   changeProduct(id:any){
    debugger;
   this.productId = id;
   let data = this.selectedProduct.filter(k=> k.productid === id );
   this.categoryName = data[0].category.categoryname;
   this.ProductName = data[0].productname;
   this.Stock=data[0].stock;
   }
  
  


  detailsProduct(){
    debugger;
    this.productService.getProduct()
    .subscribe(
      data => {
          this.selectedProduct=data;
          console.log(this.selectedProduct)
          this.productOption = _.map(this.selectedProduct, item =>{
            const option = {
              value:'',
              label:''
            };
            option.value = item.productid;
            option.label = `${item.productname}`;
            return option;
            });
         
      },
      error => {
      })
    }
  

 



  openDTEDetails(){
    if(this.dataSource.data.length === 0 ){
      // this.dpid = 0;
      // this.poid = this.poid + 1;
    } else {
      // this.poid = Math.max.apply(Math, this.dataSource.data.map(function(o){return o.poid}))
      // this.poid = this.poid + 1;
    }
    this.dateofBirth = new Date();
    this.checkAddDispatchDetails = true;
    this.hidemainGrid = true;
    this.checkAddOrEdit = 'add';
  }

  
  openDTEList(){
    this.hidemainGrid = false;
    this.checkAddDispatchDetails = false;
    this.DispatchList();
    this.getdefault();
  }

  getdefault(){
    this.materialFillGrid =[];
    this.materialSaveList=[];
    this.categoryName = "";
    this.totalQuantityText = 0;
    this.quantity = 0;
  }


  


  addDataOnGrid(){
    let customObj = new dispatchmaterialGrid();
    customObj.Productid=this.productId;
    customObj.ProductName = this.ProductName ;
    customObj.Quantity=this.quantity;
    customObj.Dispatchdate=new Date();
    customObj.Userid = "1";
    customObj.Isdeleted ="false";
    this.totalQuantityText = this.totalQuantityText === undefined ? 0 : this.totalQuantityText;
    this.totalQuantityText +=  customObj.Quantity;
   
    this.materialFillGrid.push(customObj);
    this.materialSaveList.push(customObj);
    //this.Employeename='';
    this.Productname='';
    //this.Stock=null;
    this.Quantity=null;
    this.categoryName='';
  }

  


  getQuantity(qty:any){
    this.quantity = qty;
  }

  deletematerialonGird(i:number){
    this.totalQuantityText -= this.materialFillGrid[i].Quantity;
    
    this.materialFillGrid.splice(i,1);
    //this.materialSaveList.splice(i,1);
    this.materialSaveList[i].isdeleted = "true";
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
              this.DispatchList();
            },
            error => {  
            }       
          );
      }
    })
    
  }


  getDispatchDataEdit(dispatchData:any,checkLabelt:any){
    debugger;
    this.materialFillGrid =[];
    this.materialSaveList=[];
    this.poid= dispatchData.productid;
    this.dispatchid= dispatchData.dispatchid;
   
    this.checkAddDispatchDetails = true;
    this.hidemainGrid = true;
    this.employeeid= dispatchData.employeeid;
    this.Employeename=dispatchData.employeeName;
    this.dateofBirth  = dispatchData.dispatchdate;
    this.totalQuantityText = dispatchData.totalqty;
    for(var i=0 ; i< dispatchData.dispatchdetailsModels.length;i++){
        let customObj = new dispatchmaterialGrid();
        customObj.Productid=dispatchData.dispatchdetailsModels[i].productid;
          customObj.Dispatchid=dispatchData.dispatchdetailsModels[i].dispatchid;
          // customObj.Employeeid=dispatchData.dispatchdetailModels[i].employeeid;
          // customObj.EmployeeName =dispatchData.dispatchdetailModels[i].employeeName;
          // let data = this.selectedEmployee.filter(k=> k.employeeid == dispatchData.dispatchdetailModels[i].employeeid );
          // customObj.EmployeeName = data[0].employeeName;
          let data = this.selectedProduct.filter(k=> k.productid == dispatchData.dispatchdetailsModels[i].productid );
          customObj.ProductName = data[i].productname;
          customObj.Quantity=dispatchData.dispatchdetailsModels[i].quantity;
          customObj.Userid = dispatchData.dispatchdetailsModels[i].userid;
          //customObj.Dispatchdate = dispatchData.DispatchdetailsVModel[i].dispatchdate;
          customObj.Createddate = dispatchData.dispatchdetailsModels[i].createddate;
          customObj.Isdeleted =dispatchData.dispatchdetailsModels[i].isdeleted;
          customObj.Dispatchdetailid =dispatchData.dispatchdetailsModels[i].dispatchdetailid;
          customObj.Dispatchid = dispatchData.dispatchdetailsModels[i].dispatchid;
          //customObj.Productid = dispatchData.dispatchdetailsModels[i].productid;
          this.materialFillGrid.push(customObj);
          this.materialSaveList.push(customObj);
    }
    this.checkAddOrEdit=checkLabelt;
  }


  addData(){
    debugger;
    var addDispatchModel = new DispatchToEmployeemodel();
    addDispatchModel.DispatchmasterVmodel = new dispatchToEmployeeMaster();
    addDispatchModel.DispatchmasterVmodel.Dispatchid= this.dpid;
    addDispatchModel.DispatchmasterVmodel.Dispatchdate = this.dateofBirth ;
    addDispatchModel.DispatchmasterVmodel.Employeeid = this.ID;
    addDispatchModel.DispatchmasterVmodel.EmployeeName = this.DIsplayname;
    addDispatchModel.DispatchmasterVmodel.Totalqty = this.totalQuantityText;
    addDispatchModel.DispatchmasterVmodel.Userid = '1';
    addDispatchModel.DispatchmasterVmodel.Createddate = new Date();
    addDispatchModel.DispatchmasterVmodel.Isdeleted ="false";
    addDispatchModel.DispatchdetailsVModel = new Array<dispatchDetails>();
    addDispatchModel.DispatchdetailsVModel = this.materialSaveList;
    if(this.checkAddOrEdit !=="edit"){
      addDispatchModel.DispatchmasterVmodel.Dispatchid= 0;
      this.dispatchToEmployeeService.postRequest(addDispatchModel)
      .subscribe(
        success => {
         this.notificationService.success("Successfully Saved");
         this.openDTEList();
       },
        error => {
       }
      );
    } else {
      this.dispatchToEmployeeService.patchRequest(addDispatchModel,this.dispatchid)
      .subscribe(
        success => {
         this.notificationService.success("Successfully Saved");
         this.openDTEList();
       },
        error => {
       }
      );
    }
    
  }


 

}
