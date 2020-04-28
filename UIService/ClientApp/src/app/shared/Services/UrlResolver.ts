import { Injectable } from "@angular/core";
import { FraudModel } from "../Models/FraudModel";

@Injectable()
export class UrlResolver {
    getUrlForSaveCase(modelName: string) {
        let url: string = "";
        switch (modelName) {
            case FraudModel.name:
                url = 'fraud/save'
                break;
        
            default:
                break;
        }

        return url;
    }
}