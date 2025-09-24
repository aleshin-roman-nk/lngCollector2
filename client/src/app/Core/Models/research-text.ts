export interface IResearchText {
  id: number;
  nodeId: number;
  text?: string | null;
}

export interface ICreateResearchTextDto {
  nodeId: number;
  text?: string | null;
}

export interface IUpdateResearchTextDto {
  id: number;
  text?: string | null;
}
