import { Component, OnInit } from '@angular/core';

import { FormControl, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  categoryNameValidator: FormControl =  new FormControl('', [Validators.required]);
  categoryDescriptionValidator: FormControl =  new FormControl('', [Validators.required]);

  categoryForm: FormGroup = new FormGroup({
    category_name: this.categoryNameValidator,
    category_description: this.categoryDescriptionValidator
  });
}
