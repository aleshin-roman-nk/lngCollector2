import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, tap } from 'rxjs';
import { INodeDetail } from '../models/nodedetail';
import { environment } from 'src/environment';
import { HttpClient } from '@angular/common/http';
import { IThought } from '../models/thought';

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

  constructor(private http: HttpClient) { }


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

    return this.http.get<INodeDetail>(url)
  }

  createThought(nodeId: number, th: IThought): Observable<IThought> {
    const url = `${environment.apiUrl}/thought?nodeId=${nodeId}`;

    return this.http.post<IThought>(url, th)
  }

}
