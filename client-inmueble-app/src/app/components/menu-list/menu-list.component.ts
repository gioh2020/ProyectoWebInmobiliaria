import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-menu-list',
  templateUrl: './menu-list.component.html',
  styleUrls: ['./menu-list.component.scss']
})
export class MenuListComponent implements OnInit {
@Output() menuToggle = new EventEmitter<void>();
@Output() signOut = new EventEmitter<void>();
@Input() isAuthorized ! : boolean | null;
  constructor() { }

  ngOnInit(): void {
    console.log('aa',this.isAuthorized)
  }

  closeMenu(){
    this.menuToggle.emit()
  }

  onSignOut(){
    this.signOut.emit()
  }

}
