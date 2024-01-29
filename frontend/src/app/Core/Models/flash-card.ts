import { IFlashCardAnswer } from "./flash-card-answer";
import { ILanguage } from "./language";

export interface IFlashCard{
  id: number;
  nodeId: number;
  question?: string;
  description?: string;
  answers?: IFlashCardAnswer[];
  nextExamDate: Date;
  hitsInRow?: number;
  requiredHits?: number;
  totalHits?: number
  language?: ILanguage;
  level?: number
}

export interface ICreateFlashCardDto{
  nodeId: number;
  question?: string;
  description?: string;
  languageId: number;
  nextExamDate: Date;
  requiredHits: number;
}

export interface IUpdateFlashCardDto{
  id: number;
  question?: string | null;
  description?: string | null;
  languageId: number;
  //hitsInRow: number;
  //requiredHits: number;
  //totalHits: number;
  //nextExamDate: Date;
}
