export class UserFormResultSimple<TReturnEntity>{
  private _data: TReturnEntity | undefined

  constructor(d: TReturnEntity | undefined){
      this._data = d
  }

  public get value() : TReturnEntity | undefined {
      return this._data
  }
}
