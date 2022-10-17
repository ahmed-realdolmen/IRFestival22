import { Injectable } from '@angular/core';
import { Schedule } from '../api/models/schedule.model';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Artist } from '../api/models/artist.model';

@Injectable({
  providedIn: 'root'
})
export class FestivalApiService {
  private baseUrl = environment.apiBaseUrl + 'festival';

  constructor(private httpClient: HttpClient) { }

  getSchedule(): Observable<Schedule> {
    const headers = new HttpHeaders().set("Ocp-Apim-Subscription-Key", "68a2263d19f8449cbf5a1172be2b7d21");
    return this.httpClient.get<Schedule>(`${this.baseUrl}/lineup`, {headers});
  }

  getArtists(): Observable<Artist[]> {
    const headers = new HttpHeaders().set("Ocp-Apim-Subscription-Key", "68a2263d19f8449cbf5a1172be2b7d21");
    
    return this.httpClient.get<Artist[]>(`${this.baseUrl}/artists`, {headers});
  }
}
