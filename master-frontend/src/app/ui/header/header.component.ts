import { Component, OnInit } from '@angular/core';

import { KeycloakService, KeycloakEvent, KeycloakEventType } from 'keycloak-angular';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  public username: string;
  public authenticated: boolean;

  constructor(public keyclakService: KeycloakService) { }

  ngOnInit() {
    this.keyclakService.isLoggedIn().then((value) => {
      this.setOnAuthenticatedValue(value);
    });

    this.keyclakService.keycloakEvents$.subscribe((e: KeycloakEvent) => {
      this.setOnAuthenticatedValue(e.type === KeycloakEventType.OnAuthSuccess);
    });
  }

  private setOnAuthenticatedValue(value: boolean): void {
    this.authenticated = value;
    if (this.authenticated) {
      this.username = this.keyclakService.getUsername();
    } else {
      this.username = '';
    }
  }

  logout(): void {
    this.authenticated = false;
    this.keyclakService.logout('http://localhost:4200/dashboard');
  }

  login(): void {
    this.keyclakService.login();
  }

}
