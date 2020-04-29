export class CaseBaseModel {
    public id: string;
    public title: string;
    public caseTypeId: number;
    public caseType: string;
    public statusId: number;
    public status: string;
    public history: Array<History>
}

export class History {
    public statusDate: Date;
    public status: string;
    public statusId: number;
}