import { Injectable } from "@angular/core";
import { UrlResolver } from "./UrlResolver";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class CaseService {

    constructor(private urlRelover: UrlResolver, private http: HttpClient) {
        
    }

    SubmitCase(caseModel: any, modelName: string) {
        let url = this.urlRelover.getUrlForSaveCase(modelName);
        return this.http.post(url, caseModel);
    }
}