import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fill'
})
export class FillPipe implements PipeTransform {

  transform(value: number, ...args: unknown[]): number[] {
    return (new Array(value)).fill(1);
  }

}
