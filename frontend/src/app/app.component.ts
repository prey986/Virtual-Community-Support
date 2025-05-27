import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,FormsModule,NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  owner="Prey"
  enrollNo: string = '';
  name: string = '';
  email: string = '';
  students: Array<{ enrollNo: string, name: string, email: string }> = [];
  addStudent() {

    this.students.push({
      enrollNo: this.enrollNo,
      name: this.name,
      email: this.email
    });

    this.enrollNo = '';
    this.name = '';
    this.email = '';
  }
}
