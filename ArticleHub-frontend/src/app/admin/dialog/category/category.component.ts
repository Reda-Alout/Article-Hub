import { Component, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { SnackbarService } from '../../../services/snackbar.service';
import { ThemeService } from '../../../services/theme.service';
import { CategoryService } from '../../../services/category.service';
import { GlobalConstants } from '../../../shared/global-constants';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss'
})
export class CategoryComponent {

  onAddCategory = new EventEmitter();
  onEditCategory = new EventEmitter();
  categoryForm: any = FormGroup;
  dialogAction: any = "Add";
  action: any = "Add";
  responseMessage: any;
  constructor(@Inject(MAT_DIALOG_DATA) public dialogData: any,
    private formBuilder: FormBuilder,
    private ngxService: NgxUiLoaderService,
    public dialogRef: MatDialogRef<CategoryComponent>,
    private snackbarService: SnackbarService,
    public themeService: ThemeService,
    public categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryForm = this.formBuilder.group({
      name: [null, [Validators.required]],
    });
    if (this.dialogData.action === 'Edit') {
      this.dialogAction = "Edit";
      this.action = "Update";
      this.categoryForm.patchValue(this.dialogData.data);
    }

  }

  handleSubmit() {
    if (this.dialogAction == "Edit") {
      this.edit();
    }
    else {
      this.add();
    }
  }

  add() {
    this.ngxService.start();
    var formData = this.categoryForm.value;
    var data ={
      name: formData.name,
  }
  this.categoryService.add(data).subscribe((response: any)=>{
    this.ngxService.stop();
    this.dialogRef.close();
    this.onAddCategory.emit();
    this.responseMessage = response?.message;
    this.snackbarService.openSnackBar(this.responseMessage,"action");
    }, (error)=>{
      this.ngxService.stop();
      console.log(error);
      if(error.error?.message){
      this.responseMessage = error.error?.message;
      }
      else{
      this.responseMessage = GlobalConstants.genericError;
      }
      this.snackbarService.openSnackBar(this.responseMessage,"action");
    })
}

edit() {
  this.ngxService.start();
  var formData = this.categoryForm.value;
  var data ={
    name: formData.name,
    id:this.dialogData.data.id
}
this.categoryService.update(data).subscribe((response: any)=>{
  this.ngxService.stop();
  this.dialogRef.close();
  this.onEditCategory.emit();
  this.responseMessage = response?.message;
  this.snackbarService.openSnackBar(this.responseMessage,"action");
  }, (error)=>{
    this.ngxService.stop();
    console.log(error);
    if(error.error?.message){
    this.responseMessage = error.error?.message;
    }
    else{
    this.responseMessage = GlobalConstants.genericError;
    }
    this.snackbarService.openSnackBar(this.responseMessage,"action");
  })
}

}
