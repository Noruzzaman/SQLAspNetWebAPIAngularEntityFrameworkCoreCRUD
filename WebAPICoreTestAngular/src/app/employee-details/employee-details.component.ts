import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../shared/employee.service';
import { Employee } from '../shared/employee.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.css']
})
export class EmployeeDetailsComponent implements OnInit {

  constructor(public empService: EmployeeService, public datepipe: DatePipe) {

  }

  ngOnInit() {
    this.empService.getEmployee().subscribe(data => {
      this.empService.listEmployee = data;
    });
  }
  populateEmployee(selecetedEmployee: Employee) {

    let df = this.datepipe.transform(selecetedEmployee.doj, 'yyyy-MM-dd');
    selecetedEmployee.doj = df;
    this.empService.employeeData = selecetedEmployee;


  }

  delete(id: number) {
    if (confirm('Are you really want to delete this record?')) {
      this.empService.deleteEmployee(id).subscribe(data => {
        this.empService.getEmployee().subscribe(data => {
          this.empService.listEmployee = data;

        });
      },
        err => {
        });
    }
  }
}