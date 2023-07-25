import { ButtonKind } from "./buttons-kind-enum"
import { UserOperationEnum } from "./user-operation"

export class UserResponse<TResponse>{

    private _data: TResponse | undefined
    private _userOperation: UserOperationEnum

    constructor(d: TResponse | undefined, uo: UserOperationEnum){
        this._data = d
        this._userOperation = uo
    }

    public get value() : TResponse | undefined {
        return this._data
    }
    
    public get userOperation() : UserOperationEnum {
        return this._userOperation
    }
}