import { CaseBaseModel } from "./BaseCaseModel";

export class FraudModel extends CaseBaseModel {
    public fullName: string;
    public address: string;
    public detailsAboutPerson: string;
}