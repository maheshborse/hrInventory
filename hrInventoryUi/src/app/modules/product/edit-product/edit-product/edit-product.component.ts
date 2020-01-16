import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  productNameValidator: FormControl =  new FormControl('', [Validators.required]);
  productDescriptionValidator: FormControl =  new FormControl('', [Validators.required]);
  categryNameValidator: FormControl =  new FormControl('', [Validators.required]);

  productForm: FormGroup = new FormGroup({
    productName: this.productNameValidator,
    ProductDescription: this.productDescriptionValidator,
    categryName:this.productDescriptionValidator
  });

}
