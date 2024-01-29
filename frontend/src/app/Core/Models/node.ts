export interface INode {
  id: number
  terrainId: number
  name: string
  description: string
  x: number
  y: number
}

export interface INodeNameAndDescriptionUpdateDto {
  id: number
  name: string
  description: string
}
