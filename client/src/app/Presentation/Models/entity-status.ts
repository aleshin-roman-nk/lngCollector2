/**
 * If component commits crud-aperation over its type entity, calling services for web api,
 * then entity is already chaged in data base. Then component returns notification  about that.
 * Then parent component receives changed or already created entity or is nitificated that entity is deleted.
 */
export enum EntityStatus {
  created,
  changed,
  deleted
}
