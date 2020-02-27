import { Component, OnInit } from '@angular/core';
import { ChartDB } from '../../../fack-db/chart-data';
import {ApexChartService} from '../../../theme/shared/components/chart/apex-chart/apex-chart.service';
import { PurchaseService } from 'src/app/shared/services/purchase';
import { RequestService } from 'src/app/shared/services/request.service';
import { DispatchToEmployeeService } from 'src/app/shared/services/dispatch-to-employee.service';
import * as _ from 'lodash';   
@Component({
  selector: 'app-dash-analytics',
  templateUrl: './dash-analytics.component.html',
  styleUrls: ['./dash-analytics.component.scss']
})
export class DashAnalyticsComponent implements OnInit {
  public chartDB: any;
  public dailyVisitorStatus: string;
  public dailyVisitorAxis: any;
  public deviceProgressBar: any;
  checkTotalPurchaseCount:any;
  tempData=[];
  checkTotalRequestCount:any;
  tempRequestData=[];
  tempDisapatchToEmployeeData=[];
  checkRequestCurruntMonthCount:any;
  checkCurruntMonthCount:any;
  checkRequestPendingCount:any;
  checkRequestPendingCurruntMonth:any
  checkDiapatchToEmployeeCurruntMonthCount:any;
  stockMonitoringData=[];
  checkTotalDispatchToEmployeeCount:any;
  constructor(public apexEvent: ApexChartService,private purchaseService:PurchaseService,public request:RequestService,public dispatchToEmployeeService:DispatchToEmployeeService) {
    this.chartDB = ChartDB;
    this.dailyVisitorStatus = '1y';
    
    this.deviceProgressBar = [
      {
        type: 'success',
        value: 66
      }, {
        type: 'primary',
        value: 26
      }, {
        type: 'danger',
        value: 8
      }
    ];
  }

  dailyVisitorEvent(status) {
    this.dailyVisitorStatus = status;
    switch (status) {
      case '1m':
        this.dailyVisitorAxis = {
          min: new Date('28 Jan 2013').getTime(),
          max: new Date('27 Feb 2013').getTime(),
        };
        break;
      case '6m':
        this.dailyVisitorAxis = {
          min: new Date('27 Sep 2012').getTime(),
          max: new Date('27 Feb 2013').getTime()
        };
        break;
      case '1y':
        this.dailyVisitorAxis = {
          min: new Date('27 Feb 2012').getTime(),
          max: new Date('27 Feb 2013').getTime()
        };
        break;
      case 'ytd':
        this.dailyVisitorAxis = {
          min: new Date('01 Jan 2013').getTime(),
          max: new Date('27 Feb 2013').getTime()
        };
        break;
      case 'all':
        this.dailyVisitorAxis = {
          min: undefined,
          max: undefined
        };
        break;
    }

    setTimeout(() => {
      this.apexEvent.eventChangeTimeRange();
    });
   
  }

  ngOnInit() {
    this.PurchaseList();
    this.productList();
    this.dispatchList();
  }

  PurchaseList(){
    this.purchaseService.getPodetail()
     .subscribe(
      data => {
        if(data.length !== 0){
          this.checkTotalPurchaseCount = data.length;
          this.tempData = data;
          this.getPurchaseCurruntMonthFirstDate();
        }
      }
    );
  }

  productList(){
    this.request.getRequestdetail()
    .subscribe(
     data => {
       if(data.length !== 0){
        this.checkTotalRequestCount = data.length;
        this.tempRequestData = data;
        this.stockMonitoringData=[];
        for (let index = 0; index < this.tempRequestData.length; index++) {
             var tempCount = this.tempRequestData[index].requestDetailModels.filter(d => {
              return d.status === 'Pending';
           });
            for (let i = 0; i < this.tempRequestData[index].requestDetailModels.length; i++) {
             this.stockMonitoringData.push(this.tempRequestData[index].requestDetailModels[i].productModels);
             this.stockMonitoringData = this.stockMonitoringData.filter(
              (thing, i, arr) => arr.findIndex(t => t.productid === thing.productid) === i
            );
            }
          this.getPendingRequestCurruntMonth(tempCount);
          this.checkRequestPendingCount = tempCount.length;
        }
        this.getRequestCurruntMonth();
       }
     });
  }


  dispatchList(){
    this.dispatchToEmployeeService.getDispatchdetail()
     .subscribe(
      data => {
        if(data.length !== 0){
         this.checkTotalDispatchToEmployeeCount = data.length;
         this.tempDisapatchToEmployeeData = data;
         this.getDiapatchCurruntMonth();
        }
      }
    );
  }
  
  getDiapatchCurruntMonth(){

    var date = new Date(); 
    var firstDay =  new Date(date.getFullYear(), date.getMonth(), 1); 
    var lastDay =   new Date(date.getFullYear(), date.getMonth() + 1, 0); 
    let start = new Date(firstDay);
    let end   = new Date(lastDay);
    var count = this.tempDisapatchToEmployeeData.filter(item => {
        let date = new Date(item.dispatchdate);
        return date >= start && date <= end;
    });
    this.checkDiapatchToEmployeeCurruntMonthCount= count.length;
  }

  getPurchaseCurruntMonthFirstDate(){

    var date = new Date(); 
    var firstDay =  new Date(date.getFullYear(), date.getMonth(), 1); 
    var lastDay =   new Date(date.getFullYear(), date.getMonth() + 1, 0); 
    let start = new Date(firstDay);
    let end   = new Date(lastDay);
    var count = this.tempData.filter(item => {
        let date = new Date(item.podate);
        return date >= start && date <= end;
    });
    this.checkCurruntMonthCount = count.length;
  }

  getRequestCurruntMonth(){
    var date = new Date(); 
    var firstDay =  new Date(date.getFullYear(), date.getMonth(), 1); 
    var lastDay =   new Date(date.getFullYear(), date.getMonth() + 1, 0); 
    let start = new Date(firstDay);
    let end   = new Date(lastDay);
    var count = this.tempRequestData.filter(item => {
        let date = new Date(item.createddate);
        return date >= start && date <= end;
    });
    this.checkRequestCurruntMonthCount = count.length;
  }

  getPendingRequestCurruntMonth(detailsData:any){
    var date = new Date(); 
    var firstDay =  new Date(date.getFullYear(), date.getMonth(), 1); 
    var lastDay =   new Date(date.getFullYear(), date.getMonth() + 1, 0); 
    let start = new Date(firstDay);
    let end   = new Date(lastDay);
    var count = detailsData.filter(item => {
        let date = new Date(item.createddate);
        return date >= start && date <= end;
    });
    this.checkRequestPendingCurruntMonth = count.length;
  }

}
