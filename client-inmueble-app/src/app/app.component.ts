import { Component, OnInit } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { NotificationService } from './services';
import * as fromRoot from './store'
import * as fromUser from './store/user'
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'cli-inmueble-app';
  showSpinner = false;
  isAuthorized$ ! : Observable<boolean>;
  user$ ! : Observable<fromUser.UserResponse>;

  constructor(private angularFireStore: AngularFirestore, private notification: NotificationService, private store: Store<fromRoot.State>, private router : Router) {


  }
  ngOnInit(): void {
    this.user$ = this.store.pipe(select(fromUser.getUser)) as Observable<fromUser.UserResponse>
    this.isAuthorized$ = this.store.pipe(select(fromUser.getIsAunthorized)) as Observable<boolean>

    this.angularFireStore.collection('test').stateChanges().subscribe(personas => {
      console.log(personas?.map(x => x.payload.doc.data()))
    })
  }

  onToggleSpinner(): void {
    this.showSpinner = !this.showSpinner
  }
  onFilesChanged(urls: any): void {
    console.log("aaa", urls)
  }

  onSuccess(): void {
    this.notification.success('exitoso')
  }
  onError(): void {
    this.notification.error('error')
  }
  onSignOut (): void {
    localStorage.removeItem('token')
    this.store.dispatch(new fromUser.SignOut());
    this.router.navigate(['/auth/login'])
  }
}
