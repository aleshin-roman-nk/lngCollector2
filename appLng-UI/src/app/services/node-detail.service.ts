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

  nodeDetail: INodeDetail

  /*
  В этом сервисе возможно обслуживать две отдельные сущности: INode и Thought[] в разных BehaviourSubject
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

  getNodeDetail(nodeId: number): Observable<INodeDetail> {
    const url = `${environment.apiUrl}/node/${nodeId}/detail`;

    return this.http.get<INodeDetail>(url)
  }

  createThought(nodeId: number, th: IThought): Observable<IThought> {
    const url = `${environment.apiUrl}/thought?nodeId=${nodeId}`;

    return this.http.post<IThought>(url, th)
  }

}
