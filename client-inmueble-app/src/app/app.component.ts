import { Component, OnInit } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { NotificationService } from './services'; 

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'cli-inmueble-app';
  showSpinner = false;

  constructor(private angularFireStore: AngularFirestore, private notification: NotificationService) {
    
    
  }
  ngOnInit(): void {
    this.angularFireStore.collection('test').stateChanges().subscribe(personas => {
      console.log(personas?.map(x=>  x.payload.doc.data()))
    })
  }

  onToggleSpinner() : void {
    this.showSpinner = !this.showSpinner
  }
  onFilesChanged(urls: any ) : void{
    console.log("aaa", urls)
  }

  onSuccess() : void {
    this.notification.success('exitoso')
  }
  onError() : void {
    this.notification.error('error')
  }
}
