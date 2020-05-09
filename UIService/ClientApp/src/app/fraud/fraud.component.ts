import { Component } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { FraudModel } from '../shared/Models/FraudModel';
import { CaseService } from '../shared/Services/case-service';
import { Router } from '@angular/router';
import { FormBase } from '../shared/form/FormBase';
import { ICaseForm } from '../shared/form/ICaseForm';

@Component({
  selector: 'fraud-component',
  templateUrl: './fraud.component.html',
  styleUrls: ['./fraud.component.css']
})
export class FraudComponent extends FormBase<FraudModel> implements ICaseForm {

  constructor(private formBuilder: FormBuilder, private service: CaseService, private route: Router) {
    super(service, route, FraudModel.name);

    this.registerSetBasePropertiesFunction(this.setBaseProps);
    this.registerCreateFormFunction(this.createForm);
    this.registerCollectDataFunction(this.collectData);
    this.initializeForm();
  }

  createForm() {
    this.caseForm = this.formBuilder.group({
      title: ['', Validators.required],
      fullName: ['', Validators.required],
      detailsAboutPerson: ['', Validators.required]
    });
  }

  setBaseProps() {
    this.formModel = new FraudModel();
    this.formModel.caseType = "Fraud";
    this.formModel.caseTypeId = 1;
    this.formModel.status = "Submitted";
    this.formModel.statusId = 1;
    this.formModel.history =[];
  }

  collectData() {
    this.formModel.title = this.caseForm.value.title;
    this.formModel.fullName = this.caseForm.value.fullName;
    this.formModel.detailsAboutPerson = this.caseForm.value.detailsAboutPerson;
  }

}
