import { Component } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { FraudModel } from '../shared/Models/FraudModel';
import { CaseService } from '../shared/Services/case-service';
import { Router } from '@angular/router';

@Component({
  selector: 'fraud-component',
  templateUrl: './fraud.component.html',
  styleUrls: ['./fraud.component.css']
})
export class FraudComponent {
  caseForm: FormGroup;
  formModel: FraudModel;
  loaded: boolean = false;
  submitted: boolean = false;
  modelName: string = "";
  constructor(private formBuilder: FormBuilder, private service: CaseService, private route: Router) {
    this.createForm();
  }

  createForm() {
    this.caseForm = this.formBuilder.group({
      title: ['', Validators.required],
      fullName: ['', Validators.required],
      detailsAboutPerson: ['', Validators.required]
    });
    this.loaded = true;
  }

  setBaseProps() {
    this.formModel.caseType = "Fraud";
    this.formModel.caseTypeId = 1;
    this.formModel.status = "Submitted";
    this.formModel.statusId = 1;
  }

  collectData() {
    this.formModel = new FraudModel();
    this.formModel.title = this.caseForm.value.title;
    this.formModel.fullName = this.caseForm.value.fullName;
    this.formModel.detailsAboutPerson = this.caseForm.value.detailsAboutPerson;
  }

  checkValidationError(fieldName: string, validation: string) {
      return this.caseForm.get(fieldName).hasError(validation);
  }

  submitForm() {
    this.submitted = true;
    this.caseForm.updateValueAndValidity();
    if(!this.caseForm.valid) {
        return;
    }

    this.collectData();
    this.service.SubmitCase(this.formModel, this.modelName).subscribe(result => {
        this.route.navigate(['success']);
    }, err => {
        console.error(err);
    });

  }

}
