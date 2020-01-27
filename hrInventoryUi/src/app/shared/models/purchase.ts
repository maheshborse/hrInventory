export class purchaseOrdermodel{
    public pomasterModel:purchaseOrderMaster;
    public podetailModel :purchaseDetails[];
} 

export class materialGrid {
    Podetailid: number;
    Productid: number;
    poId:number;
    ProductName:string;
    Quantity:number;
    PoRate:number;
    Discount:number;
    Amount:number;
    Userid: string;
    Createddate: Date;
    Isdeleted: string;
}

export class purchaseOrderMaster{
    public Poid: number;
    public Podate: Date;
    public Totalamount: number;
    public Discount: number;
    public Finalamount: number;
    public Userid: string;
    public Createddate: Date;
    public Isdeleted: string
}

export class purchaseDetails{
    public Podetailid: number;
    public Poid: number;
    public Productid: number;
    public Quantity: number;
    public Porate: number;
    public Discount: number;
    public Amount: number;
    public Userid: number;
    public Createddate: Date;
    public Isdeleted: string
}

 



