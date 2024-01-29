export interface IFlashCardCheck{
  cardId: number
  solution: string
  helpIsUsed: boolean
}

export interface  IFlashCardCheckResult{
  isCorrect: boolean;
  cardId: number;
  nextExamDate: Date;
  totalHits: number;
  hitsInRow: number;
  level: number
}
