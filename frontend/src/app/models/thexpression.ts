import { WhoMade } from "./whomade";

export interface IThExpression {
    id: number
    thoughtId: number
    text: string
    createdDate: Date,
    lngId: number,
    scores: number,
    madeBy: WhoMade
}