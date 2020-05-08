import { CaseBaseModel } from "../Models/BaseCaseModel";
import { FraudModel } from "../Models/FraudModel";
import { FormGroup } from "@angular/forms";
import { CaseService } from "../Services/case-service";
import { Router } from "@angular/router";
import { ICaseForm } from "./ICaseForm";

export class FormBase<T extends CaseBaseModel> {
  caseForm: FormGroup;
  formModel: T;
  loaded: boolean = false;
  submitted: boolean = false;
  modelName: string = "";

  cases: Array<T>;

  // Delegates
  setBasePropertiesFn() { }
  createFormFn() { }
  collectDataFn() { }

  constructor(
    private baseService: CaseService,
    private baseRoute: Router,
    modelType: string
  ) {
    this.modelName = modelType;
    this.initializeForm();
  }

  public registerSetBasePropertiesFunction(fn: any) {
    this.setBasePropertiesFn = fn;
  }

  public registerCreateFormFunction(fn: any) {
    this.createFormFn = fn;
  }

  public registerCollectDataFunction(fn: any) {
    this.collectDataFn = fn;
  }

  initializeForm() {
    this.setBasePropertiesFn();
    this.createFormFn();
    this.loaded = true;
  }

  public checkValidationError(fieldName: string, validation: string) {
    return this.caseForm.get(fieldName).hasError(validation);
  }

  public submitForm() {
    this.submitted = true;
    this.caseForm.updateValueAndValidity();
    if (!this.caseForm.valid) {
      return;
    }

    this.collectDataFn();
    this.baseService.SubmitCase<T>(this.formModel, this.modelName).subscribe(result => {
      this.baseRoute.navigate(['success']);
    }, err => {
        alert("Failed, Please try again later.");
      console.error(err);
    });

  }

}
