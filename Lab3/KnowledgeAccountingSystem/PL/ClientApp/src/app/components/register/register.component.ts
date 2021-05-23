import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  loading: boolean = false;
  submitted: boolean = false;
  hasSchoolEducation: boolean = false;
  hasUniversityEducation: boolean = false;
  hasJob: boolean = false;
  error: string = "";

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService,
  ) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20)]],
      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20)]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(40)]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      country: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20)]],
      city: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20)]],
      school: ['', [Validators.maxLength(40)]],
      university: ['', [Validators.maxLength(40)]],
      speciality: ['', [Validators.maxLength(40)]],
      job: ['', [Validators.maxLength(40)]]
    },
      {
        validator: this.isMatch('password', 'confirmPassword')
      });
  }

  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;
    this.userService.register(
      this.f.firstName.value,
      this.f.lastName.value,
      this.f.email.value,
      this.f.password.value,
      this.f.country.value,
      this.f.city.value,
      this.f.school.value,
      this.f.university.value,
      this.f.speciality.value,
      this.f.job.value
    ).pipe(first())
      .subscribe(
        () => {
          this.router.navigate(['/login']);
        },
        error => {
          this.error = error.status === 400 ? error.error.message : "Try again later";
          this.loading = false;
        });
  }

  isMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustmatch) {
        return;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustmatch: true });
      }
    }
  }
}
