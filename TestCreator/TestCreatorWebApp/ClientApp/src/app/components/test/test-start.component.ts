import { Component, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { AuthService } from '../../services/auth.service';

@Component({
  selector: "test-start",
  templateUrl: "./test-start.component.html",
  styleUrls: ['./test-start.component.css']
})

export class TestStartComponent {
  testAttempt: TestAttempt; 
  title: string;
  selectedTestAttemptEntry: TestAttemptEntry;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    public auth: AuthService,
    @Inject('BASE_URL') private baseUrl: string) {

    var id = +this.activatedRoute.snapshot.params["id"];
    console.log(id);

    if (id) {
      var url = this.baseUrl + "api/test/start/" + id;
      this.http.get<TestAttempt>(url).subscribe(result => {
          this.testAttempt = result;
          this.title = this.testAttempt.Title;
        },
        error => console.error(error));
    } else {
      console.log("Invalid ID - redirecting to home...");
      this.router.navigate(["home"]);
    }
  }
}
