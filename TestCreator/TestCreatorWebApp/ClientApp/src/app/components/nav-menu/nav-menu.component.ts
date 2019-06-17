import { Component } from '@angular/core';
import { faPlus, faSignInAlt, faInfoCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  faPlus = faPlus;
  faSignInAlt = faSignInAlt;
  faInfoCircle = faInfoCircle;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
