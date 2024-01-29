export interface ITerrain {
  id?: number
  name: string
  description: string
  userId: number
}

export interface ITerrainUpdateDto {
  id?: number
  name: string
  description: string
}
