import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppUserService {
  url = environment.apiUrl;
  constructor(private httpClient:HttpClient) {}

  login(data:any){
    return this.httpClient.post(this.url + "/appUser/login" , data , {
      headers:new HttpHeaders().set('Content-Type' , 'application/json')
    })
  }

  add(data:any){
    return this.httpClient.post(this.url + "/appUser/add" , data , {
      headers:new HttpHeaders().set('Content-Type' , 'application/json')
    })
  }

  get(){
    return this.httpClient.get(this.url + "/appUser/get")
  }

  update(data:any){
    return this.httpClient.post(this.url + "/appUser/update" , data , {
      headers:new HttpHeaders().set('Content-Type' , 'application/json')
    })
  }

  updateStatus(data:any){
    return this.httpClient.post(this.url + "/appUser/updateStatus" , data , {
      headers:new HttpHeaders().set('Content-Type' , 'application/json')
    })
  }

}
