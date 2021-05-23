import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserToken } from '../../models/usertoken';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  currentUser: UserToken;

  constructor(
    private router: Router,
    private userService: UserService
  ) {
    this.userService.currentUser.subscribe(x => this.currentUser = x);
  }

  login(): void {
    this.router.navigate(['login']);
  }

  logout(): void {
    this.userService.logout();
    this.router.navigate(['login']);
  }

  home(): void {
    this.router.navigate(['']);
  }
}
