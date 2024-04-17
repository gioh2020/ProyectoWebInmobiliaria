import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-files-upload',
  templateUrl: './files-upload.component.html',
  styleUrls: ['./files-upload.component.scss']
})
export class FilesUploadComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  isHovering!: boolean
  toggleHover(event:boolean){
    this.isHovering = event
  }

}
