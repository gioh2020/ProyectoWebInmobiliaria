import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserResponse } from '@app/store/user';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  @Output() menuToggle = new EventEmitter<void>();
  @Input() user ! : UserResponse | null;
  @Input() isAuthorized ! : boolean | null;
  @Output() signOut = new  EventEmitter<void>();


  constructor() { }

  ngOnInit(): void {
    console.log('user',this.user)
  }

  onMenuToggleDispatch(){
    this.menuToggle.emit()
  }

  onSignOut(): void{
    this.signOut.emit()
  }

}
