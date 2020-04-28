import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'success-component',
  templateUrl: './success-component.html',
  styleUrls: ['./success-component.css']
})
export class SuccessComponent {

    constructor(private router: Router) {

    }

    gotoHome() {
        this.router.navigate(['']);
    }
}
