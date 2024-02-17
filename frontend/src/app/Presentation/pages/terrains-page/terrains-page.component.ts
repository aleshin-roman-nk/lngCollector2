import { Component, ViewChild } from '@angular/core';
import { ITerrainTitle } from 'src/app/Core/Models/terrain';
import { TerriansService } from 'src/app/Core/services/terrians.service';
import { EditTerrainComponent } from '../../comps-edit/edit-terrain/edit-terrain.component';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-terrains-page',
  templateUrl: './terrains-page.component.html',
  styleUrls: ['./terrains-page.component.css']
})
export class TerrainsPageComponent {

  @ViewChild("editTerrainDlg", { static: false }) editTerrainDlg!: EditTerrainComponent

  terrains: ITerrainTitle[] = []

  loading: boolean = false

  constructor(public terrSrv: TerriansService
    ) { }

  ngOnInit() {

    this.loading = true;

    this.terrSrv
      .getAll()
      .subscribe((data) => {
        this.loading = false
        this.terrains = data
      })
  }

  ngAfterViewInit(){
    this.editTerrainDlg.finished
    .subscribe(data => {
      this.terrains.push(data)
    })
  }

}
