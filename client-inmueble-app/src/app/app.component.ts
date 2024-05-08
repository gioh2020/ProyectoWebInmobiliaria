import { Component, OnInit } from '@angular/core';
import { environment } from '@src/environments/environment';
import { test } from './test';
import { AngularFirestore } from '@angular/fire/compat/firestore';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'cli-inmueble-app';
  showSpinner = false;

  constructor(private angularFireStore: AngularFirestore) {
    
    
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
}
