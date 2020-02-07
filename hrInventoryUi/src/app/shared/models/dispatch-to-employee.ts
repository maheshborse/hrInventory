export class DispatchToEmployeemodel{
    public DispatchmasterVmodel:dispatchToEmployeeMaster;
    public DispatchdetailsVModel :dispatchDetails[];
}

export class dispatchmaterialGrid {
    Dispatchid: number;
    Productid: number;
    ProductName:string;
    Quantity:number;
    Userid:string;
    Isdeleted: string;
    Createddate: Date;
    Dispatchdetailid: number;
}



export class dispatchToEmployeeMaster{
    public Dispatchid: number;
    public Dispatchdate: Date;
    public Employeeid: number;
    public EmployeeName: string;
    public Totalqty:number;
    public Userid: string;
    public Createddate: Date;
    public Isdeleted: string;
}

export class dispatchDetails{
    public Dispatchdetailid: number;
    public Dispatchid: number;
    public Productid: number;
    public Quantity: number;
    public Userid: string;
    public Createddate: Date;
    public Isdeleted: string;
}

export interface dispatchToEmployee {
    employeeName: string;
   
  }
  