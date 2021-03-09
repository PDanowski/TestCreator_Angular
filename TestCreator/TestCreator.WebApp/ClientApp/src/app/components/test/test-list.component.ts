import { Component, Inject, Input, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { faRandom, faFire, faSortAlphaDown } from '@fortawesome/free-solid-svg-icons';
import { Test } from 'src/app/interfaces/test';

@Component({
  selector: "test-list",
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
}
)

export class TestListComponent implements OnInit {
  @Input() class: string;
  title: string;
  selectedTest: Test;
  tests: Test[];

  faRandom = faRandom;
  faFire = faFire;
  faSortAlphaDown = faSortAlphaDown;

  ngOnInit(): void {

    console.log("TestListComponent create using " + this.class + " class.");

    var url = this.baseUrl + "api/test?sorting=";

    switch (this.class) {
    case "latest":
    default:
      url += "1";
      this.title = "Latest tests";
      break;
    case "random":
      url += "0";
      this.title = "Random tests";
      break;
    case "byTitle":
      url += "2";
      this.title = "Tests sorted by title";
      break;
    }

    this.http.get<Test[]>(url).subscribe(result => {
        this.tests = result;
      },
      error => console.error(error));
  }

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router) {

    this.http = http;
    this.baseUrl = baseUrl;

  }

  onSelect(test: Test) {
    this.selectedTest = test;
    console.log("Selected test: " + this.selectedTest.Id);
    this.router.navigate(["test", this.selectedTest.Id]);
  }

  getIcon() {
    switch (this.class) {
    case "latest":
    default:
        return faFire;
    case "random":
        return faRandom;
    case "byTitle":
        return faSortAlphaDown;
    }
  }
}
