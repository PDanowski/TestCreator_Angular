import { Component, Inject, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "question-edit",
  templateUrl: './question-edit.component.html',
  styleUrls: ['./question-edit.component.css']
})

export class QuestionEditComponent {
  title: string;
  question: Question;

  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {

    this.question = <Question>{};

    var id = +this.activatedRoute.snapshot.params["id"];

    this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

    if (this.editMode) {

      var url = this.baseUrl + "api/question/" + id;

      this.http.get<Question>(url).subscribe(result => {
          this.question = result;
          this.title = "Editing - " + this.question.Text;
        },
        error => console.error(error));
    } else {
      this.question.TestId = id;
      this.title = "Create new question";
    }
  }

  onSubmit(question: Question) {

    var url = this.baseUrl + "api/question";

    if (this.editMode) {

      this.http.post<Question>(url, question).subscribe(result => {
          var v = result;
          console.log("Question " + v.Id + " was updated");
          this.router.navigate(["test/edit", question.TestId]);
        },
        error => console.log(error));
    } else {
      this.http.put<Question>(url, question).subscribe(result => {
        var v = result;
        console.log("Question " + v.Id + " was created");
        this.router.navigate(["test/edit", question.TestId]);
      })
    }
  }

  onBack() {
    this.router.navigate(["test/edit", this.question.TestId]);
  }
}
