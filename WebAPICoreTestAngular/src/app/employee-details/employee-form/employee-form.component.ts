import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Employee } from 'src/app/shared/employee.model';
import { EmployeeService } from 'src/app/shared/employee.service';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnInit {

  constructor(public empService: EmployeeService) {

  }

  ngOnInit() {
    this.empService.getDesignation().subscribe(data => {
      this.empService.listDesignation = data;
    });
  }
  submit(form: NgForm) {
    this.empService.employeeData.isMarried = form.value.isMarried == true ? 1 : 0;
    this.empService.employeeData.isActive = form.value.isActive == true ? 1 : 0;

    if (this.empService.employeeData.id == 0) {
      this.insertEmployee(form);
    }
    else {
      this.updateEmployee(form);
    }
  }

  insertEmployee(myform: NgForm) {
    this.empService.saveEmployee().subscribe(d => {

      this.resetForm(myform);
      this.refreshData();
      console.log('save Success');

    });
  }

  updateEmployee(myform: NgForm) {

    this.empService.updateEmployee().subscribe(d => {

      this.resetForm(myform);
      this.refreshData();
      console.log('Update Success');

    });

  }

  resetForm(myform: NgForm) {
    myform.form.reset();
    this.empService.employeeData = new Employee();
  }

  refreshData() {
    this.empService.getEmployee().subscribe(res => {

      this.empService.listEmployee = res;
    });
  }

}
