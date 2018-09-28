import { HttpHeaders, HttpClient } from '@angular/common/http';


export class BaseService
{
    protected  httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*',  })
    };

    protected _baseUrl = 'http://localhost:57913/api/';

    constructor(protected _http: HttpClient) {
    
    }
}