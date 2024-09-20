import { Injectable } from '@angular/core';
import { GlobalConstants } from '../shared/global-constants';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { SnackbarService } from './snackbar.service';

@Injectable({
  providedIn: 'root'
})
export class RouterGuardService {

  constructor(
    //public auth:AuthService,
    public router:Router,
    private snackbarService:SnackbarService) { }

    canActivate(router:ActivatedRouteSnapshot):boolean{
      // let expectRoleArray = router.data;
      // expectRoleArray = expectRoleArray.expectedRole;

      const token:any = localStorage.getItem('token');

      var tokenPayload:any;

      // try{
      //   tokenPayload = jwt_decode(token);
      // }catch(err){
      //   localStorage.clear();
      //   this.router.navigate(['/']);
      // }

      // let expectedRole = '';

      // for(let i = 0 ;  i < expectRoleArray.length; i++){
      //   if(expectRoleArray[i] == tokenPayload.role){
      //     expectedRole = tokenPayload.role;
      //   }
      // }


      if(!token){
        this.router.navigate(['/']);
        return false;
      }
      else{
        // this.router.navigate(['/']);
        // localStorage.clear();
        return true;
      }
    }
}
