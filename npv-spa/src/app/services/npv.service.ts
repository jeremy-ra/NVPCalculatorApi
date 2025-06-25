import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NpvInput } from '../models/npv-input.model';
import { NpvResult } from '../models/npv-result.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NpvService {
  private apiUrl = 'https://localhost:7227/api/npv/calculate'; // Update as needed based on localhost:port

  constructor(private http: HttpClient) { }

  calculate(input: NpvInput): Observable<NpvResult[]> {
    return this.http.post<NpvResult[]>(this.apiUrl, input);
  }
}
