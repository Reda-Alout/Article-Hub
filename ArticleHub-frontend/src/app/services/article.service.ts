import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  url = environment.apiUrl;
  constructor(private httpClient:HttpClient) {}



  add(data:any){
    return this.httpClient.post(this.url + "/article/add" , data , {
      headers:new HttpHeaders().set('Content-Type' , 'application/json')
    })
  }

  get(){
    return this.httpClient.get(this.url + "/article/get")
  }

  update(data:any){
    return this.httpClient.post(this.url + "/article/update" , data , {
      headers:new HttpHeaders().set('Content-Type' , 'application/json')
    })
  }

  getPublished(){
    return this.httpClient.get(this.url + "/article/getPublic")
  }

  delete(id:any){
    return this.httpClient.get(this.url + "/article/delete/"+id)
  }
}
