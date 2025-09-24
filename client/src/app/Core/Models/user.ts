export interface IUser{
  Username: string
  Password: string
  Email: string
}

export interface IAuthorizedUser{
  Name: string
  Email: string
  Id: string
}

export interface IAuthorizationResult{
  token: string
  authUser: IAuthorizedUser
}
