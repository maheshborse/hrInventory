import {Injectable} from '@angular/core';

export interface NavigationItem {
  id: string;
  title: string;
  type: 'item' | 'collapse' | 'group';
  translate?: string;
  icon?: string;
  hidden?: boolean;
  url?: string;
  classes?: string;
  exactMatch?: boolean;
  external?: boolean;
  target?: boolean;
  breadcrumbs?: boolean;
  function?: any;
  badge?: {
    title?: string;
    type?: string;
  };
  children?: Navigation[];
}

export interface Navigation extends NavigationItem {
  children?: NavigationItem[];
}

const NavigationItems = [
  {
    id: 'navigation',
    title: 'Navigation',
    type: 'group',
    icon: 'feather icon-monitor',
    children: [
      {
        id: 'dashboard',
        title: 'Dashboard',
        type: 'item',
        icon: 'feather icon-home',
        url: '/dashboard/analytics',
        breadcrumbs: false
      },
      
    ],
    
  },
  {
    id: '',
    title: 'Admin module',
    type: 'group',
    icon: 'feather icon-layers',
    children: [
      {
        id: '',
        title: 'Manage Pages',
        type: 'collapse',
        icon: 'feather icon-file-text',
        children: [
          {
            id: 'category',
            title: ' Product Category',
            type: 'item',
            url: '/category'
          },
          {
            id: 'product',
            title: 'Product',
            type: 'item',
            url: '/product'
          },
        ]
      },
      {
        id: '',
        title: 'Transaction Pages',
        type: 'collapse',
        icon: 'feather icon-file-text',
        children: [
          {
            id: 'purchase',
            title: 'Purchase Order',
            type: 'item',
            url: '/purchase'
          },
          {
             id:'request',
             title:'Request',
             type:'item',
             url:'/request'
          },
          {
            id: 'dispatch-to-employee',
            title: 'Dispatch To Employee',
            type: 'item',
            url: '/dispatch-to-employee'
          },
        ]
      }
    ]
  },
 
];
const NavigationItemsForother = [
 
  {
    id: '',
    title: 'Employee Pages',
    type: 'collapse',
    icon: 'feather icon-file-text',
    children: [
      {
        id: 'request',
        title: 'Request',
        type: 'item',
        url: '/request',
      },
    ],
  },
  
 
];

@Injectable()
export class NavigationItem {
  
  public get() {
    let user=JSON.parse(localStorage.getItem('user'));
    if(user.isAdmin === true){
      return NavigationItems;
    } else{
      return NavigationItemsForother; 
    }
   
  }
}
