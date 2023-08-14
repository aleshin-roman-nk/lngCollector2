import { Component } from '@angular/core';
import { ITerrain } from 'src/app/Core/Models/terrain';
import { ModalService } from 'src/app/Presentation/services/modal.service';
import { TerriansService } from 'src/app/Core/services/terrians.service';

@Component({
  selector: 'app-terrains-page',
  templateUrl: './terrains-page.component.html',
  styleUrls: ['./terrains-page.component.css']
})
export class TerrainsPageComponent {

  terrains: ITerrain[] = []

  loading: boolean = false

  constructor(public terrSrv: TerriansService,
    public modalService: ModalService
    ) { }

  ngOnInit(): void {

    this.loading = true;

    this.terrSrv
      .getAll()
      //.subscribe((result: ITerrain[]) => this.terrains = result)
      .subscribe(data => {
        //console.log(data)

        if(data.Success){
          this.loading = false
          this.terrains = data.Content
        }
      })
  }
}
