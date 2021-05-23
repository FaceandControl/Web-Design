import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { UserService } from 'src/app/services/user.service';
import { UserToken } from '../../models/usertoken';

@Component({
  selector: 'app-user-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  error: string = '';

  get f() { return this.loginForm.controls; }

  currentUser: UserToken;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService) {
    this.userService.currentUser.subscribe(x => this.currentUser = x);
    if (this.currentUser) {
      this.router.navigate(['/profile']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(40)]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
    });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.userService.login(this.f.email.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        () => {
          this.router.navigate(['/profile']);
        },
        err => {
          this.error = err.status === 400 ? err.error.message : "Try again later";
          this.loading = false;
        });
  }
}
