import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import * as fromUser from '@app/store/user'
import * as fromRoot from '@app/store'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loading$ !: Observable<boolean | null>

  constructor(private store: Store<fromRoot.State>) { }

  ngOnInit(): void {
  }

  loginUsuario(form: NgForm):void{
    const useloginRequest: fromUser.EmailPasswordCredentials = {

      email: form.value.email,
      password: form.value.password
    }

    this.store.dispatch(new fromUser.SignInEmail(useloginRequest))
    
  }

}
