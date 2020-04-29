"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var FormBase = /** @class */ (function () {
    function FormBase(baseService, baseRoute, modelType) {
        this.baseService = baseService;
        this.baseRoute = baseRoute;
        this.loaded = false;
        this.submitted = false;
        this.modelName = "";
        this.modelName = modelType;
        this.initializeForm();
    }
    // Delegates
    FormBase.prototype.setBasePropertiesFn = function () { };
    FormBase.prototype.createFormFn = function () { };
    FormBase.prototype.collectDataFn = function () { };
    FormBase.prototype.registerSetBasePropertiesFunction = function (fn) {
        this.setBasePropertiesFn = fn;
    };
    FormBase.prototype.registerCreateFormFunction = function (fn) {
        this.createFormFn = fn;
    };
    FormBase.prototype.registerCollectDataFunction = function (fn) {
        this.collectDataFn = fn;
    };
    FormBase.prototype.initializeForm = function () {
        this.setBasePropertiesFn();
        this.createFormFn();
        this.loaded = true;
    };
    FormBase.prototype.checkValidationError = function (fieldName, validation) {
        return this.caseForm.get(fieldName).hasError(validation);
    };
    FormBase.prototype.submitForm = function () {
        var _this = this;
        this.submitted = true;
        this.caseForm.updateValueAndValidity();
        if (!this.caseForm.valid) {
            return;
        }
        this.collectDataFn();
        debugger;
        this.baseService.SubmitCase(this.formModel, this.modelName).subscribe(function (result) {
            _this.baseRoute.navigate(['success']);
        }, function (err) {
            alert("Failed, Please try again later.");
            console.error(err);
        });
    };
    return FormBase;
}());
exports.FormBase = FormBase;
//# sourceMappingURL=FormBase.js.map