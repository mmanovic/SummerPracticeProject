import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-model-listing',
  templateUrl: './model-listing.component.html',
  styleUrls: ['./model-listing.component.css']
})
export class ModelListingComponent implements OnInit {

  @Input()
  private name: string;
  @Input()
  private path: string;
  @Input()
  private modelsGetter: () => Observable<any[]>;

  showList = false;
  models: any[];

  constructor() { }

  ngOnInit() {
    if (!this.name || !this.path || !this.modelsGetter) {
      throw new Error('Not all input binding properties are set!');
    }
  }

  toggle(): void {
    this.showList = !this.showList;
    if (this.showList) {
      this.loadModels();
    }
  }

  private loadModels(): void {
    if (!this.models) {
      this.modelsGetter().subscribe(m => this.models = m);
    }
  }

}
