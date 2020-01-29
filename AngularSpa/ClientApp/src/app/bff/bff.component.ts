import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-bff',
  templateUrl: './bff.component.html',
})
export class BffComponent {
  data;
  constructor(private http: HttpClient) { }

  getFrontend(): void {
    const url = 'user/info';
    this.http
      .get<any>(url)
      .subscribe(this.setData, this.handleError);
  }

  getBackend(): void {
    const url = 'user/infoFromExternal';
    this.http
      .get<any>(url)
      .subscribe(this.setData, this.handleError);
  }

  setData = data => {
    this.data = data;
  }

  handleError = error => {
    console.info('Something went wrong');
    console.error(error);
  }
}
