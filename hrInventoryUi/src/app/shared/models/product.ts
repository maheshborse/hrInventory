export interface product {
  Productid:number,
  Productname:string;
  Productdescription:string;
  Categoryid:string;
  Userid:string;
  Createddate:Date;
  Isdeleted:string
      
}

export class showOnGrid{
  productid: number;
  categoryid: number;
  productname: string;
  productdescription: string;
  userid: number;
  createddate: Date;
  isdeleted: string;
  balance: number;
  categoryname: string;
}