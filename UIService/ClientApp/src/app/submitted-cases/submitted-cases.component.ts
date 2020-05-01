import { Component } from '@angular/core';
import { CaseService } from '../shared/Services/case-service';
import { CaseBaseModel } from '../shared/Models/BaseCaseModel';
import { FraudModel } from '../shared/Models/FraudModel';
import { forkJoin } from 'rxjs';
import { TrafficLightModel } from '../shared/Models/TrafficLightModel';


@Component({
  selector: 'submitted-cases-component',
  templateUrl: './submitted-cases.component.html',
  styleUrls: ['./submitted-cases.component.css']
})
export class SubmittedCaseComponent {

    cases: Array<CaseBaseModel>;
    modelList: Array<string> = [
        FraudModel.name,
        TrafficLightModel.name
    ];

    constructor(private caseService: CaseService) {
        this.loadData();
    }

    loadData() {
        let tasks = [];
        this.modelList.forEach(item => {
            tasks.push(this.caseService.GetAllCases(item));
        });

        forkJoin(tasks).subscribe(resultSet => {
            this.cases = [];
            resultSet.forEach(result => {
                let formattedData = result as Array<CaseBaseModel>;
                formattedData.forEach(it => {
                    this.cases.push(it);
                });
            })
        }, err => {
            console.error(err);
            alert("Failed to get data, please try again later");
        });
    }
}
