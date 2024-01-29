import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, catchError, first, tap } from 'rxjs';
import { INodeDetail, IUpdateNodeNameAndDescriptionDto } from '../Models/nodedetail';
import { environment } from 'src/environment';
import { HttpClient } from '@angular/common/http';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class NodeDetailService {

  //nodeDetail: INodeDetail

  /*
  В этом сервисе возможно обслуживать две отдельные сущности: INode и IThought[] в разных BehaviourSubject
  */

/*   private nodeDetailSubject: BehaviorSubject<INodeDetail> = new BehaviorSubject<INodeDetail>({
    node: {
      id: 0,
      name: "",
      terrianId: 0,
      description: '',
      x: 0,
      y: 0
    },
    thoughts: []
  });
  public nodeDetail$: Observable<INodeDetail> = this.nodeDetailSubject.asObservable(); */

  constructor(
    private http: HttpClient,
    private errorService: ErrorHandlerService
    ) { }


  /**
   * Оставлю так: в компоненте переменные
   * - обновляются вызовом getNodeDetail
   * -- так же можно добавить метод только добавить thought
   * -- подписавшись на это можно отдельно добавлять в массив объект
   * И не хранить в сервисе объекты
   *
   *
   */
  getNodeDetail(nodeId: number): Observable<INodeDetail> {
    const url = `${environment.apiUrl}/node/${nodeId}/detail`;

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
