import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { INode } from '../models/node';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment';
import { INodeDetail } from '../models/nodedetail';

@Injectable({
  providedIn: 'root'
})
export class NodeService {

  private dataNodesSubject: BehaviorSubject<INode[]> = new BehaviorSubject<INode[]>([]);
  public dataNodes$: Observable<INode[]> = this.dataNodesSubject.asObservable();

  constructor(private http: HttpClient) { }

  loadNodesOf(terrId: number){
    const url = `${environment.apiUrl}/terrain/${terrId}/nodes`;

    this.http.get<INode[]>(url).subscribe(resp => {
      this.dataNodesSubject.next(resp)
    })
  }
}
