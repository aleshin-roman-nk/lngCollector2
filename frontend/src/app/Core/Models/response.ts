export class ApiResponse {
    Success: boolean
    Message: string
}

export class ApiResponseWithContent<TContent> {
    Success: boolean
    Message: string
    Content: TContent
}