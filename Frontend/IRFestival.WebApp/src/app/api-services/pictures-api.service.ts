import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PicturesApiService {
  private baseUrl = environment.apiBaseUrl + 'pictures';

  constructor(private httpClient: HttpClient) { }

  getAllUrls(): Observable<string[]> {
    const headers = new HttpHeaders().set("Ocp-Apim-Subscription-Key", "68a2263d19f8449cbf5a1172be2b7d21");
    
    return this.httpClient.get<string[]>(`${this.baseUrl}`, {headers});
  }

  upload(file: File): Observable<never> {
    const headers = new HttpHeaders().set("Ocp-Apim-Subscription-Key", "68a2263d19f8449cbf5a1172be2b7d21");
    
    const data = new FormData();
    data.set('file', file);

    return this.httpClient.post<never>(`${this.baseUrl}`, data, {headers});
  }
}
