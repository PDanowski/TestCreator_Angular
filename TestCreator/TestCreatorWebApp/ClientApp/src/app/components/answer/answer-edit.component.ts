import { Component, Inject, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "answer-edit",
  templateUrl: './answer-edit.component.html',
  styleUrls: ['./answer-edit.component.css']
})

export class AnswerEditComponent {
  title: string;
  answer: Answer;

  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {

    this.answer = <Answer>{};

    var id = +this.activatedRoute.snapshot.params["id"];

    this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");

    if (this.editMode) {

      var url = this.baseUrl + "api/answer/" + id;

      this.http.get<Answer>(url).subscribe(result => {
        this.answer = result;
        this.title = "Editing - " + this.answer.Text;
        },
        error => console.error(error));
    } else {
      this.answer.QuestionId = id;
      this.title = "Create new answer";
    }
  }

  onSubmit(answer: Answer) {

    var url = this.baseUrl + "api/answer";

    if (this.editMode) {

      this.http.post<Answer>(url, answer).subscribe(result => {
          var v = result;
        console.log("Answer " + v.Id + " was updated");
        this.router.navigate(["question/edit", answer.QuestionId]);
        },
        error => console.log(error));
    } else {
      this.http.put<Answer>(url, answer).subscribe(result => {
        var v = result;
        console.log("Answer " + v.Id + " was created");
        this.router.navigate(["question/edit", answer.QuestionId]);
      })
    }
  }

  onBack() {
    this.router.navigate(["question/edit", this.answer.QuestionId]);
  }
}
