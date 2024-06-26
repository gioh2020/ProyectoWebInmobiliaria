import { Injectable } from "@angular/core"

import * as fromActions from './user.actions'
import { HttpClient } from "@angular/common/http";
import { NotificationService } from "@app/services";
import { Router } from "@angular/router";
import { Observable, of, } from "rxjs";
import { catchError, map, switchMap, tap } from "rxjs/operators";
import { createEffect, ofType, Actions } from "@ngrx/effects";
import { UserResponse } from "./user.model";
import { environment } from "@src/environments/environment";



type Action = fromActions.ALL;

@Injectable()

export class UserEffects {


    constructor(
        private httpClient: HttpClient,
        private actions: Actions,
        private notification: NotificationService,
        private router: Router
    ) { }

    signUpEmail: Observable<Action> = createEffect(() =>

        this.actions.pipe(
            ofType(fromActions.Types.SIGN_UP_EMAIL),
            map((action: fromActions.SignUpEmail) => action.user),
            switchMap(userData =>
                this.httpClient.post<UserResponse>(`${environment.url}api/usuario/registrar`, userData)
                    .pipe(
                        tap((response: UserResponse) => {
                            localStorage.setItem('token', response.token);
                            this.router.navigate(['/'])
                        }),
                        map((response: UserResponse) => new fromActions.SignUpEmailSuccess(response.email, response || null)),
                        catchError(err => {
                            this.notification.error("Errores al registar un nuevo usuario");
                            return of(new fromActions.SignUpEmailError(err.message))
                        })
                    )
            )
        )
    );

    singInEmail: Observable<Action> = createEffect(() =>

        this.actions.pipe(
            ofType(fromActions.Types.SIGN_IN_EMAIL),
            map((action: fromActions.SignInEmail) => action.credentials),
            switchMap(userData =>
                this.httpClient.post<UserResponse>(`${environment.url}api/usuario/login`, userData)
                    .pipe(
                        tap((response: UserResponse) => {
                            localStorage.setItem('token', response.token);
                            this.router.navigate(['/'])
                        }),
                        map((response: UserResponse) => new fromActions.SignInEmailSuccess(response.email, response || null)),
                        catchError(err => {
                            this.notification.error("Credenciales incorrectas");
                            return of(new fromActions.SignInEmailError(err.message))
                        })
                    )
            )
        )
    );

    init: Observable<Action> = createEffect(() =>

        this.actions.pipe(
            ofType(fromActions.Types.INIT),
            switchMap(async () => localStorage.getItem('token')),
            switchMap(token => {

                if (token) {
                    return this.httpClient.get<UserResponse>(`${environment.url}api/usuario`)
                        .pipe(
                            tap((response: UserResponse) => {
                                console.log('Data del usuario en sesion')

                            }),
                            map((response: UserResponse) => new fromActions.InitAuthorized(response.email, response || null)),
                            catchError(err => {

                                return of(new fromActions.InitError(err.message))
                            })
                        )
                } else {


                    return of(new fromActions.InitAnauthorized());
                }

            }
            )
        )
    );

}