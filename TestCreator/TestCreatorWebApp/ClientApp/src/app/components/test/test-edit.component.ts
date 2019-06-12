import { Component, Inject, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "test-edit",
  templateUrl: './test-edit.component.html',
  styleUrls: ['./test-edit.component.css']
})

export class TestEditComponent {
  title: string;
  test: Test;

  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {

    this.test = <Test>{};

    var id = +this.activatedRoute.snapshot.params["id"];
    if (id) {
      this.editMode = true;

      var url = this.baseUrl + "api/test/" + id;
      this.http.get<Test>(url).subscribe(result => {
          this.test = result;
          this.title = "Editing - " + this.test.Title
        },
        error => console.error(error));

    } else {
      this.editMode = false;
      this.title = "Create new test";
    }
  }

  onSubmit(test: Test) {
    var url = this.baseUrl + "api/test";

    if (this.editMode) {
      this.http.post<Test>(url, test).subscribe(result => {
          var res = result;
        console.log("Test " + res.Id + " was updated");
        this.router.navigate(["home"]);
        },
        error => console.error(error));
    } else {
      this.http.put<Test>(url, test).subscribe(result => {
        var res = result;
        console.log("Test " + res.Id + " was created");
          this.router.navigate(["home"]);
        },
        error => console.error(error));
    }
  }

  onBack() {
    this.router.navigate(["home"]);
  }

}
