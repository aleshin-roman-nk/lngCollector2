import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, first } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment';
import { INodeDetail, IUpdateNodeNameAndDescriptionDto } from '../Models/node-detail';
import { ErrorHandlerService } from './error-handler.service';
import { ICreateNodeDto } from '../Models/create-node-dto';
import { INodeTitle } from '../Models/node-title';

@Injectable({
  providedIn: 'root'
})
export class NodeService {

  constructor(
    private http: HttpClient,
    private errorService: ErrorHandlerService
    ) { }

  addNode(node: ICreateNodeDto): Observable<INodeTitle>{
    const url = `${environment.apiUrl}/node`

    return this.http
    .post<INodeTitle>(url, node)
    .pipe(
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  deleteNode(id: number): Observable<void>{
    const url = `${environment.apiUrl}/node/${id}`
    return this.http
    .delete<void>(url)
    .pipe(
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  getNodeDetail(nodeId: number): Observable<INodeDetail> {
    //const url = `${environment.apiUrl}/node/${nodeId}/detail`;
    const url = `${environment.apiUrl}/node/${nodeId}`;

    return this.http
    .get<INodeDetail>(url)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  updateNodeNameAndDescription(nodedto: IUpdateNodeNameAndDescriptionDto): Observable<void>{
    const url = `${environment.apiUrl}/node`;

    return this.http
    .put<void>(url, nodedto)
    .pipe(
      first(),
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

}
