import { AfterViewInit, Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[appElementFocused]'
})
export class ElementFocusedDirective implements AfterViewInit  {

  constructor(private el: ElementRef) { }
  ngAfterViewInit(): void {
    this.el.nativeElement.focus()
  }
}
