import { IThExpression } from "./thexpression"

export interface IThought {
    id: number
    nodeId: number
    text: string
    description: string
    createdDate: Date
    expressions: IThExpression[]
}