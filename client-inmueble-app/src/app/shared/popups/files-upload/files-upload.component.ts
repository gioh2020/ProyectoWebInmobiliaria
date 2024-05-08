import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData{
  multiple: boolean,
  crop: boolean
}
@Component({
  selector: 'app-files-upload',
  templateUrl: './files-upload.component.html',
  styleUrls: ['./files-upload.component.scss']
})
export class FilesUploadComponent implements OnInit {

  isHovering!: boolean
  files: File[] = [];
  imageFile!: File;
  isError!: boolean;

  filesUrls: string[] =[]


  constructor(private diaLogRef: MatDialogRef<FilesUploadComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData) { 


  }

  ngOnInit(): void {
  }

  toggleHover(event:boolean){
    this.isHovering = event
  }

  onDrop(files: FileList): void {
    this.dropGeneral(files)
  }
  onDropFile(event: FileList | any): void {
    console.log(event.target.files);
    
    this.dropGeneral(event.target.files)
  }

  dropGeneral(files: FileList | any): void {
    this.isError = false
    if (this.data.crop && files.legth>1) {
      this.isError = true;
      return;
      
    }
    for (let i = 0; i < files.length; i++) {
      this.files.push(files.item(i) as File)
      
    }
    
  }

  onUploadComplete(url: string): void{
    this.filesUrls.push(url)
  }

  onComplete(){
    console.log(this.filesUrls)
    const res = this.data.multiple ? this.filesUrls : this.filesUrls[0]
    this.diaLogRef.close(res)
  }

  onClose(){
    this.diaLogRef.close()
  }

}
