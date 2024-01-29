import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError } from 'rxjs';
import { INode } from '../Models/node';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment';
import { INodeDetail } from '../Models/nodedetail';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class NodeService {

/*   private dataNodesSubject: BehaviorSubject<INode[]> = new BehaviorSubject<INode[]>([]);
  public dataNodes$: Observable<INode[]> = this.dataNodesSubject.asObservable(); */

  constructor(
    private http: HttpClient,
    private errorService: ErrorHandlerService
    ) { }

/*   loadNodesOf(terrId: number){
    const url = `${environment.apiUrl}/terrain/${terrId}/nodes`;

    this.http.get<INode[]>(url).subscribe(resp => {
      this.dataNodesSubject.next(resp)
    })
  } */

  getNodesByTerrainId(terrId: number): Observable<INode[]>{
    const url = `${environment.apiUrl}/terrain/${terrId}/nodes`
    return this.http
    .get<INode[]>(url)
    .pipe(
      catchError(error => this.errorService.httpErrorHandle(error))
    )
  }

  addNode(text: string, terrId: number): Observable<INode>{
    const url = `${environment.apiUrl}/node`

    const n: INode = {
      description: "",
      id: 0,
      name: text,
      terrainId: terrId,
      x: 0,
      y: 0
    }

    return this.http
    .post<INode>(url, n)
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

}
