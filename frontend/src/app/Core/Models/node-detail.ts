import { IFlashCardTitle } from "./flash-card-title";
import { IResearchText } from "./research-text";

export interface INodeDetail {
  id: number;
  terrainId: number;
  name?: string;
  description?: string;
  level: number;
  FlashCardsTitles: IFlashCardTitle[]
  ResearchTexts: IResearchText[]
}

export interface IUpdateNodeNameAndDescriptionDto {
  id: number;
  name?: string | null;
  description?: string | null;
}
