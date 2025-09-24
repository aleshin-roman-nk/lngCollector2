import { INodeTitle } from "./node-title";

export interface ITerrainTitle {
  id?: number
  name: string
  description: string
  userId: number
}

export interface ITerrainDetail {
  id: number;
  name?: string;
  description?: string;
  userId: number;
  nodes?: INodeTitle[] | null; 
}

export interface ICreateTerrainDto {
  name?: string;
  description?: string;
  userId: number;
}

export interface ITerrainUpdateDto {
  id?: number
  name: string
  description: string
}
