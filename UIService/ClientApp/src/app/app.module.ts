import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FraudComponent } from './fraud/fraud.component';
import { SuccessComponent } from './success-component/success-component';
import { UrlResolver } from './shared/Services/UrlResolver';
import { CaseService } from './shared/Services/case-service';
import { SubmittedCaseComponent } from './submitted-cases/submitted-cases.component';
import { TrafficLightComponent } from './traffic-light/traffic-light.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    FraudComponent,
    SuccessComponent,
    SubmittedCaseComponent,
    TrafficLightComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'fraud', component: FraudComponent },
      { path: 'success', component: SuccessComponent },
      { path: 'submitted-cases', component: SubmittedCaseComponent},
      { path: 'traffic-light', component: TrafficLightComponent }
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    UrlResolver,
    CaseService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
