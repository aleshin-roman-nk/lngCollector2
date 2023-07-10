export class UserResponse<TResponse>{

    private data: TResponse
    private userAccepted: boolean = false

    constructor(d: TResponse, ua: boolean){
        this.data = d
        this.userAccepted = ua
    }

    public get value() : TResponse {
        return this.data
    }
    
    public get hasUserAccepted() : boolean {
        return this.userAccepted
    }
}