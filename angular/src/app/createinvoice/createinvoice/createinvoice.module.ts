import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreateinvoiceModuleRoutingModule } from './createinvoice-routing.module';

import { CreateinvoiceComponent } from './createinvoice.component';


@NgModule({
  declarations: [CreateinvoiceComponent],
  imports: [SharedModule, CreateinvoiceModuleRoutingModule],
})
export class CreateinvoiceModule {}
