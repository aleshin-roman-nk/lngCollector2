export interface IFlashCardTitle {
  id: number;
  nodeId: number;
  question?: string;
  hitsInRow?: number;
  requiredHits?: number;
  totalHits?: number
  lngTag?: string;
  level?: number
  answersCount: number
}
