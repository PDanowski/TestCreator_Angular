import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "test-list",
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
}
)

export class TestListComponent {
  title: string;
  selectedTest: Test;
  tests: Test[];

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.title = "Newest tests";
    var url = baseUrl + "api/test/latest";

    http.get<Test[]>(url).subscribe(result => {
      this.tests = result;
      console.log(this.tests);
      },
      error => console.error(error));
  }

  onSelect(test: Test) {
    this.selectedTest = test;
    console.log("Selected test: " + this.selectedTest.Id);
  }
}
