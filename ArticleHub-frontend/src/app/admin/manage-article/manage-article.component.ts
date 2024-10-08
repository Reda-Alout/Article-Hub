import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { SnackbarService } from '../../services/snackbar.service';
import { Router } from '@angular/router';
import { ArticleService } from '../../services/article.service';
import { ThemeService } from '../../services/theme.service';
import { MatTableDataSource } from '@angular/material/table';
import { GlobalConstants } from '../../shared/global-constants';
import { ConfirmationComponent } from '../dialog/confirmation/confirmation.component';
import { ArticleComponent } from '../dialog/article/article.component';
import { ViewArticleComponent } from '../dialog/view-article/view-article.component';

@Component({
  selector: 'app-manage-article',
  templateUrl: './manage-article.component.html',
  styleUrl: './manage-article.component.scss'
})
export class ManageArticleComponent {

  displayedColumns: string[] = ['title','categoryName','status','publication_date',  'edit'];
  dataSource: any;
  responseMessage: any;
  constructor(private ngxService: NgxUiLoaderService,
    private dialog: MatDialog,
    private snackbarService: SnackbarService,
    private router: Router,
    private articleService: ArticleService,
    public themeService: ThemeService) { }
  ngOnInit(): void {
    this.ngxService.start();
    this.tableData();

  }

  tableData() {
    this.articleService.get().subscribe((response: any) => {
      this.ngxService.stop();
      this.dataSource = new MatTableDataSource(response);
    }, (error: any) => {
      this.ngxService.stop();
      console.log(error);
      if (error.error?.message) {
        this.responseMessage = error.error?.message;
      }
      else {
        this.responseMessage = GlobalConstants.genericError;
      }
      this.snackbarService.openSnackBar(this.responseMessage, "action");
    })
  }

  applyFilter(event: any) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }


  handleAddAction() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      action: 'Add'
    };
    dialogConfig.width = "850px";
    const dialogRef = this.dialog.open(ArticleComponent, dialogConfig);
    this.router.events.subscribe(() => {
      dialogRef.close();
    });
    const res = dialogRef.componentInstance.onAddArticle.subscribe(
      (response) => {
        this.tableData();
      }
    )

  }

  handleViewAction(values: any) {
    const dialogConfig = new MatDialogConfig();
    console.log(values);
    dialogConfig.data = {
      action: 'View',
      data: values
    };
    dialogConfig.width = "850px";
    const dialogRef = this.dialog.open(ViewArticleComponent, dialogConfig);
    this.router.events.subscribe(() => {
      dialogRef.close();
    });

  }

  handleEditAction(values: any) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      action: 'Edit',
      data:values
    };
    dialogConfig.width = "850px";
    const dialogRef = this.dialog.open(ArticleComponent, dialogConfig);
    this.router.events.subscribe(() => {
      dialogRef.close();
    });
    const res = dialogRef.componentInstance.onEditArticle.subscribe(
      (response) => {
        this.tableData();
      }
    )
  }

  onDelete(value:any){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
    message: 'delete ' + value.title + ' article'
    };
    const dialogRef = this.dialog.open(ConfirmationComponent, dialogConfig);
    const res = dialogRef.componentInstance.onEmitStatusChange.subscribe((response:any)=>{
    this.ngxService.start();
    this.deleteProduct(value.id);
    dialogRef.close();
    })
    }

    deleteProduct(id:any){
      this.articleService.delete(id).subscribe((response:any)=>{
      this.ngxService.stop();
      this.tableData();
      this.responseMessage = response.message;
      this.snackbarService.openSnackBar(this.responseMessage,"v");
      }, (error:any)=> {
      this.ngxService.stop();
      console.log(error);
      if (error.error?.message) {
      this.responseMessage = error.error?.message;
      }
      else
      this.responseMessage = GlobalConstants.genericError;
      this.snackbarService.openSnackBar(this.responseMessage,"e");
    })

}
}
