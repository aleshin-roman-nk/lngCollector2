import { Injectable } from '@angular/core';
import { buildings } from '../data/buldings';
import { IBuilding } from '../models/building';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor() { }

  getAll(terrId: number): IBuilding[] {
    return buildings.filter(bld => bld.terrainId == terrId)
  }

  getOne(bldId: number): IBuilding | undefined {
    return buildings.find(bld => bld.id == bldId)
  }
}
