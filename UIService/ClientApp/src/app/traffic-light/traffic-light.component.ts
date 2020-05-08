import { Component } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

import { CaseService } from '../shared/Services/case-service';
import { Router } from '@angular/router';
import { FormBase } from '../shared/form/FormBase';
import { TrafficLightModel } from '../shared/Models/TrafficLightModel';
import { ICaseForm } from '../shared/form/ICaseForm';

@Component({
  selector: 'traffic-light',
  templateUrl: './traffic-light.component.html',
  styleUrls: ['./traffic-light.component.css']
})
export class TrafficLightComponent extends FormBase<TrafficLightModel> implements ICaseForm {

  constructor(private formBuilder: FormBuilder, private service: CaseService, private route: Router) {
    super(service, route, TrafficLightModel.name);

    this.registerSetBasePropertiesFunction(this.setBaseProps);
    this.registerCreateFormFunction(this.createForm);
    this.registerCollectDataFunction(this.collectData);
    this.initializeForm();
  }

  createForm() {
    this.caseForm = this.formBuilder.group({
      title: ['', Validators.required],
      trafficLightIssue: ['', Validators.required],
      locationDetails: ['', Validators.required]
    });
  }

  setBaseProps() {
    this.formModel = new TrafficLightModel();
    this.formModel.caseType = "Traffic Light Issue";
    this.formModel.caseTypeId = 2;
    this.formModel.status = "Submitted";
    this.formModel.statusId = 1;
    this.formModel.history =[];
  }

  collectData() {
    this.formModel.title = this.caseForm.value.title;
    this.formModel.trafficLightIssue = this.caseForm.value.trafficLightIssue;
    this.formModel.locationDetails = this.caseForm.value.locationDetails;
  }

}
