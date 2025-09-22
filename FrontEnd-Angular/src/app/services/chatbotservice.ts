import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Chatbotservice {
  constructor(private _HttpClient: HttpClient) {}

    getanswerspesefic(prompt:string): Observable<any> {
      return this._HttpClient.get(`https://localhost:7092/api/Chatbot/ask?prompt=${encodeURIComponent(prompt)}`);
    }

}
