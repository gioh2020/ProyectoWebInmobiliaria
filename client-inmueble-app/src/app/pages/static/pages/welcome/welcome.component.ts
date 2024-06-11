import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import * as fromRoot from '../../../../store'
import * as fromUser from '../../../../store/user'
import { Observable } from 'rxjs';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {


  constructor(private store: Store<fromRoot.State>, ) { }

  ngOnInit(): void {
    console.log('bb', this.store.pipe(select(fromUser.getIsAunthorized)) as Observable<boolean>)

  }

}
