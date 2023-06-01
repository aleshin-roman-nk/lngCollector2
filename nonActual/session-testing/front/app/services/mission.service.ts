import { Injectable } from '@angular/core';
import { missions } from '../data/missions';
import { IMission } from '../models/mission';

@Injectable({
  providedIn: 'root'
})
export class MissionService {

  constructor() { }
  getOf(bldId: number): IMission[] {
    return missions.filter(msn => msn.buildingId == bldId)
  }

}
