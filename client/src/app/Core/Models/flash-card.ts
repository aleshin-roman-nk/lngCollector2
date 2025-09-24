import { IFlashCardAnswer } from "./flash-card-answer";
import { ILanguage } from "./language";

export interface IFlashCard{
  id: number;
  nodeId: number;
  question?: string;
  description?: string;
  answers?: IFlashCardAnswer[] | null; // Assuming FlashCardAnswer is a TypeScript type/interface
  language?: ILanguage | null; // Assuming Language is a TypeScript type/interface
  hitsInRow: number;
  requiredHits: number;
  totalHits: number;
  level: number;
  nextExamDate: Date;
  isCompleted: boolean;
  questPrice: number;
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
