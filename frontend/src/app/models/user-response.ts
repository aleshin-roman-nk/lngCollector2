import { ButtonKind } from "./buttons-kind-enum"

export class UserResponse<TResponse>{

    private data: TResponse | undefined
    private userAccepted: boolean = false

    constructor(d: TResponse | undefined, ua: boolean){
        this.data = d
        this.userAccepted = ua
    }

    public get value() : TResponse | undefined {
        return this.data
    }
    
    public get hasUserAccepted() : boolean {
        return this.userAccepted
    }
}