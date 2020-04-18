import { Component, Inject, Input, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { faRandom, faFire, faSortAlphaDown } from '@fortawesome/free-solid-svg-icons';

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

    var url = this.baseUrl + "api/test/";

    switch (this.class) {
    case "latest":
    default:
      url += "latest";
      this.title = "Latest tests";
      break;
    case "random":
      url += "random";
      this.title = "Random tests";
      break;
    case "byTitle":
      url += "byTitle";
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
