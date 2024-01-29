import { IFlashCardTitle } from "./flash-card-title";
import { INode } from "./node";
import { IResearchText } from "./research-text";

export interface INodeDetail {
    Node: INode
    FlashCardsTitles: IFlashCardTitle[]
    ResearchTexts: IResearchText[]
}

export interface IUpdateNodeNameAndDescriptionDto {
  id: number;
  name?: string | null;
  description?: string | null;
}
