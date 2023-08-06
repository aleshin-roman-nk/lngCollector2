import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { INode } from '../Models/node';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment';
import { INodeDetail } from '../Models/nodedetail';
import { ResponseExt } from '../Models/response';

@Injectable({
  providedIn: 'root'
})
export class NodeService {

/*   private dataNodesSubject: BehaviorSubject<INode[]> = new BehaviorSubject<INode[]>([]);
  public dataNodes$: Observable<INode[]> = this.dataNodesSubject.asObservable(); */

  constructor(private http: HttpClient) { }

/*   loadNodesOf(terrId: number){
    const url = `${environment.apiUrl}/terrain/${terrId}/nodes`;

    this.http.get<INode[]>(url).subscribe(resp => {
      this.dataNodesSubject.next(resp)
    })
  } */

  getNodesByTerrainId(terrId: number): Observable<ResponseExt<INode[]>>{
    const url = `${environment.apiUrl}/terrain/${terrId}/nodes`
    return this.http.get<ResponseExt<INode[]>>(url)
  }

}
