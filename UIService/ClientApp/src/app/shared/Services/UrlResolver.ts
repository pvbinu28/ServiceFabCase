import { Injectable } from "@angular/core";
import { FraudModel } from "../Models/FraudModel";
import { TrafficLightModel } from "../Models/TrafficLightModel";

@Injectable()
export class UrlResolver {
    getUrlForSaveCase(modelName: string) {
        let url: string = "";
        switch (modelName) {
            case FraudModel.name:
                url = 'api/fraud/save'
                break;
            case TrafficLightModel.name:
                url = 'api/trafficlight/save';
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
                url = 'api/fraud/cases';
                break;
            case TrafficLightModel.name:
                url = 'api/trafficlight/cases';
                break;
            default:
                break;
        }

        return url;
    }
}
