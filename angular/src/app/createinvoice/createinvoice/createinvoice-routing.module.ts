import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateinvoiceComponent } from './createinvoice.component';


const routes: Routes = [{ path: '', component: CreateinvoiceComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CreateinvoiceModuleRoutingModule {}
