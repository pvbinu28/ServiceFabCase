import { Injectable } from "@angular/core";
import { FraudModel } from "../Models/FraudModel";

@Injectable()
export class UrlResolver {
    getUrlForSaveCase(modelName: string) {
        let url: string = "";
        switch (modelName) {
            case FraudModel.name:
                url = 'api/fraud/save'
                break;
        
            default:
                break;
        }

        return url;
    }

    getUrlForCase(modelName: string) {
        let url: string = "";
        switch (modelName) {
            case FraudModel.name:
                url = 'api/fraud/cases'
                break;
        
            default:
                break;
        }

        return url;
    }
}
