import { Injectable } from '@angular/core';
import { buildings } from '../data/buldings';
import { ITerobject } from '../models/terobject';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor() { }

  getAll(terrId: number): ITerobject[] {
    return buildings.filter(bld => bld.terrainId == terrId)
  }

  getOne(bldId: number): ITerobject | undefined {
    return buildings.find(bld => bld.id == bldId)
  }
}
