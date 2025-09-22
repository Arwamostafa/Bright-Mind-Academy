import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { ITrack } from '../models/itrack.ts';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TrackService {
  constructor(private _httpClient: HttpClient){}

  getAllTracks(): Observable<ITrack[]>{
    return this._httpClient.get<ITrack[]>(`${environment.baseUrl}/track`);
  }

  getTrackById(id: number): Observable<ITrack> {
    return this._httpClient.get<ITrack>(`${environment.baseUrl}/track/${id}`);
  }

  getTrackByName(name: string): Observable<ITrack> {
    return this._httpClient.get<ITrack>(`${environment.baseUrl}/track/${name}`);
  }

  addTrack(_track: ITrack): Observable<ITrack> {

    return this._httpClient.post<ITrack>(`${environment.baseUrl}/track`, JSON.stringify(_track),{
      headers: new HttpHeaders({
        'content-type': "application/json"
      })
    });
  }

  updateTrack(id: number, _track: ITrack): Observable<string> {
    return this._httpClient.put<string>(`${environment.baseUrl}/track/${id}`, JSON.stringify(_track),{
      headers: new HttpHeaders({
        'content-type': "application/json"
      })
    });
  }

  deleteTrack(id: number): Observable<string> {
    return this._httpClient.delete<string>(`${environment.baseUrl}/track/${id}`);
  }

}
