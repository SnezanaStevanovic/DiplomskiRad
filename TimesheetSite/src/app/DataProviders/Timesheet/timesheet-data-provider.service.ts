import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class TimesheetDataProviderService extends BaseService {

  constructor(protected _client: HttpClient) {
    super(_client);

  }

  startWork(): void {

  }



}
