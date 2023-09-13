import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Designation, Employee } from './employee.model';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private myhttp: HttpClient) { }
  employeeUrl: string = 'https://localhost:44308/api/Employee';
  designationUrl: string = 'https://localhost:44308/api/Designation';
  listEmployee: Employee[] = []; //getting data
  listDesignation: Designation[] = [];
  employeeData: Employee = new Employee(); //Post data
  saveEmployee() {
    return this.myhttp.post(this.employeeUrl, this.employeeData)
  }

  updateEmployee() {
    return this.myhttp.put(`${this.employeeUrl}/${this.employeeData.id}`, this.employeeData)
  }

  getEmployee(): Observable<Employee[]> {

    return this.myhttp.get<Employee[]>(this.employeeUrl)
  }

  getDesignation(): Observable<Designation[]> {

    return this.myhttp.get<Designation[]>(this.designationUrl)
  }

  deleteEmployee(id:number)
  {
    return this.myhttp.delete(`${this.employeeUrl}/${id}`);
  }
}
