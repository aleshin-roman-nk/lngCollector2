import { ILanguage } from "./language";

export interface IFlashCardAnswer {
  id: number;
  cardId: number;
  text?: string;
  language?: ILanguage;
}

export interface ICreateFlashCardAnswer{

    cardId: number,
    lngId: number,
    text: string
}

export class UpdateCardAnswerDto
{
  id: number
  text?: string
  languageId: number
}
