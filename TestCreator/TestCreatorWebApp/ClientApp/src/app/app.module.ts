import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { TestListComponent } from './components/test/test-list.component';
import { TestComponent } from './components/test/test.component';
import { TestEditComponent } from './components/test/test-edit.component';
import { QuestionListComponent } from './components/question/question-list.component';
import { QuestionEditComponent } from './components/question/question-edit.component';
import { AnswerListComponent } from './components/answer/answer-list.component';
import { AnswerEditComponent } from './components/answer/answer-edit.component';
import { AboutComponent } from './components/about/about.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './components/pagenotfound/pagenotfound.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    TestListComponent,
    TestComponent,
    TestEditComponent,
    QuestionListComponent,
    QuestionEditComponent,
    AnswerListComponent,
    AnswerEditComponent,
    AboutComponent,
    LoginComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'test/create', component: TestEditComponent },
      { path: 'test/:id', component: TestComponent },
      { path: 'test/edit/:id', component: TestEditComponent },
      { path: 'question/edit/:id', component: QuestionEditComponent },
      { path: 'question/create/:id', component: QuestionEditComponent },
      { path: 'answer/edit/:id', component: AnswerEditComponent },
      { path: 'answer/create/:id', component: AnswerEditComponent },
      { path: 'about', component: AboutComponent },
      { path: 'login', component: LoginComponent },
      { path: '**', component: PageNotFoundComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
