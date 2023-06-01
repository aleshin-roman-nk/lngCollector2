import { Injectable } from '@angular/core';
import { buildings } from '../data/buldings';
import { INode } from '../models/node';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor() { }

  getAll(terrId: number): INode[] {
    return buildings.filter(bld => bld.terrainId == terrId)
  }

  getOne(bldId: number): INode | undefined {
    return buildings.find(bld => bld.id == bldId)
  }
}
