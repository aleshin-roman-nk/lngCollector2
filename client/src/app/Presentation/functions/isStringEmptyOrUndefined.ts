export function isStringEmptyOrUndefined(str: string | undefined): boolean {
  if(!str) return true
  return str.trim().length === 0;
}
