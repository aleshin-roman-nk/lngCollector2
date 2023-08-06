export class Response {
    Success: boolean
    Message: string
}

export class ResponseExt<TContent> {
    Success: boolean
    Message: string
    Content: TContent
}