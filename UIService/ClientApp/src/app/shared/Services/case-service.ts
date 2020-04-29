import { Injectable } from "@angular/core";
import { UrlResolver } from "./UrlResolver";
import { HttpClient } from "@angular/common/http";
import { CaseBaseModel } from "../Models/BaseCaseModel";

@Injectable()
export class CaseService {

    constructor(private urlRelover: UrlResolver, private http: HttpClient) {
        
    }

    SubmitCase(caseModel: any, modelName: string) {
        let url = this.urlRelover.getUrlForSaveCase(modelName);
        return this.http.post(url, caseModel);
    }

    GetAllCases(modelName: string) {
        let url = this.urlRelover.getUrlForCase(modelName);
        return this.http.get<CaseBaseModel>(url);
    }
}
