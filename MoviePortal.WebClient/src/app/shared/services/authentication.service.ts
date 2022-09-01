import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private path = environment.apiUrl

  constructor(private httpClient: HttpClient, private jwtHelper: JwtHelperService) { }

  public signOutExternal = () => {
      localStorage.removeItem("token");
      console.log("token deleted")
  }

  LoginWithGoogle(credentials: string): Observable<any> {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post(this.path + "api/accounts/external-login", JSON.stringify(credentials), { headers: header });
  }

  isUserAuthenticated (): boolean {
    const token = localStorage.getItem("token");
 
    return token !== null && !this.jwtHelper.isTokenExpired(token);
  }
}
