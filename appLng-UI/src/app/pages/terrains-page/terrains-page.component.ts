import { Component } from '@angular/core';
import { ITerrain } from 'src/app/models/terrain';
import { ModalService } from 'src/app/services/modal.service';
import { TerriansService } from 'src/app/services/terrians.service';

@Component({
  selector: 'app-terrains-page',
  templateUrl: './terrains-page.component.html',
  styleUrls: ['./terrains-page.component.css']
})
export class TerrainsPageComponent {

  //terrains: ITerrain[] = []

  loading: boolean = false

  constructor(public terrSrv: TerriansService,
    public modalService: ModalService
    ) { }

  ngOnInit(): void {

    this.loading = true;

    this.terrSrv
      .getAll()
      //.subscribe((result: ITerrain[]) => this.terrains = result)
      .subscribe(() => {
        this.loading = false
      })
  }
}
