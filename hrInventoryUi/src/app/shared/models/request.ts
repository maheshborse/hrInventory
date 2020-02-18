export class RequestViewModel
    {
      public Reqestmastermodel:requestMaster;
      public RequestdetailModel :requestDetail[];
    }
   

export class fillrequestGirdData {
      Requestid : number;
      Requestdetailid :number;
      Productid: number;
      productName:string;
      Quantity :number;
      Employeeid :number;
      Status : string;
      Isread : boolean;
      Userid : string;
      Createddate : Date;
      Isdeleted : string;
}

export class requestMaster {
      public  Requestid : number;
      public  Employeeid :number;
      public  Isread :boolean;
      public  Userid :number;
      public  Createddate:Date;
      public  Isdeleted :string;
  }


  export class requestDetail
    {
        public  Requestdetailid :number;
        public  Requestid :number;
        public  Productid:number;
        public  Quantity:number;
        public  Status :string;
        public  Userid :string;
        public  Createddate :Date
        public  Isdeleted :string;
    }

  export class showOnGridmaster{
    Requestid : number;
    Employeeid :number;
    EmployeeName: string;
    Isread :boolean;
    Userid :number;
    Createddate :Date;
    Isdeleted :string;
    public RequestdetailModelongrid :requestDetailonGrid[];
  }

  export class requestDetailonGrid {
    Requestdetailid :number;
    Requestid :number;
    Productid:number;
    ProductName:string;
    Quantity:number;
    Status :string;
    Userid :number;
    Createddate :Date;
    Isdeleted :string;
  }

  export class showongrid
  {
    public Reqestmastermodelongrid:showOnGridmaster[];
  }


   
   
