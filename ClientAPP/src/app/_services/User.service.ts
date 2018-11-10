import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../_models/Person';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl: string = "http://localhost:5000/api";
 

  constructor(private http: HttpClient) { }
  
  getAll() {
    return this.http.get<Person[]>(this.baseUrl + '/userservice/getusers');
  }
}