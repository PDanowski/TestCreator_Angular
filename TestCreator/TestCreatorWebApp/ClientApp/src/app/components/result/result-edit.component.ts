import { Component, Inject, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "result-edit",
  templateUrl: './result-edit.component.html',
  styleUrls: ['./result-edit.component.css']
})

export class ResultEditComponent {
  title: string;
  result: Result;

  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {

    this.result = <Result>{};

    var id = +this.activatedRoute.snapshot.params["id"];

    this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

    if (this.editMode) {

      var url = this.baseUrl + "api/result/" + id;

      this.http.get<Result>(url).subscribe(res => {
        this.result = res;
        this.title = "Editing - " + this.result.Text;
        },
        error => console.error(error));
    } else {
      this.result.TestId = id;
      this.title = "Create new result";
    }
  }

  onSubmit(result: Result) {

    var url = this.baseUrl + "api/result";

    if (this.editMode) {

      this.http.post<Result>(url, result).subscribe(res => {
        var v = res;
          console.log("Result " + v.Id + " was updated");
        this.router.navigate(["test/edit", result.TestId]);
        },
        error => console.log(error));
    } else {
      this.http.put<Result>(url, result).subscribe(res => {
        var v = res;
        console.log("Result " + v.Id + " was created");
        this.router.navigate(["test/edit", result.TestId]);
      })
    }
  }

  onBack() {
    this.router.navigate(["test/edit", this.result.TestId]);
  }
}
