import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Movie } from '../models/movie';
import { AuthenticationService } from './authentication.service';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class MovieService {
    constructor(private httpClient: HttpClient, authService: AuthenticationService) {
    }

    private path = environment.apiUrl

    getMovies(): Observable<any> {
        const header = new HttpHeaders().set('Content-type', 'application/json');
        return this.httpClient.get(this.path + "api/movies", { headers: header });
    }
}