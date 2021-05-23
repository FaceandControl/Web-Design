import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { UserToken } from '../models/usertoken';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
  private currentUserSubject: BehaviorSubject<UserToken>;
  public currentUser: Observable<UserToken>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<UserToken>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserToken {
    return this.currentUserSubject.value;
  }

  login(
    email: string, password: string) {
    return this.http.post<UserToken>(`${environment.apiUrl}/users/login`, { email, password })
      .pipe(map(userToken => {
        localStorage.setItem('currentUser', JSON.stringify(userToken));
        this.currentUserSubject.next(userToken);
        return userToken;
      }));
  }

  register(
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    country: string,
    city: string,
    school: string,
    university: string,
    speciality: string,
    job: string
  ) {
    return this.http.post(`${environment.apiUrl}/users/register`, {
      firstName,
      lastName,
      email,
      password,
      country,
      city,
      school,
      university,
      speciality,
      job
    });
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  //getUsers(): Observable<User[]> {
  //  return this.http.get<User[]>(`${environment.apiUrl}/users/`);
  //}

  //getUserById(id: number): Observable<User> {
  //  return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
  //}

  //deleteUser(id: number) {
  //  return this.http.delete(`${environment.apiUrl}/users/${id}`);
  //}
}
