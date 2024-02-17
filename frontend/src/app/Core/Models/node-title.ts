export interface INodeTitle {
  id: number;
  terrainId: number;
  name?: string;
  description?: string;
  x: number;
  y: number;
  width: number;
  height: number;
  questCount: number;
  completedQuestCount: number;
  questPrice: number;
  completedQuestPrice: number;
  level: number;
}
