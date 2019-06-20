import { Component, Input } from "@angular/core";

@Component({
  selector: "test-search",
  templateUrl: "./test-search.component.html",
  styleUrls: ['./test-search.component.css']
})

export class TestSearchComponent {
  @Input() placeholder: string;
}
