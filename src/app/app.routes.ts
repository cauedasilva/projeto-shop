import { Routes } from '@angular/router';
import { Inventory } from './AppComponents/inventory/inventory';
import { Customer } from './AppComponents/customer/customer';
import { Home } from './AppComponents/home/home';

export const routes: Routes = [ { path: '', component: Home }, { path: 'inventory', component: Inventory}, {path: 'customer', component: Customer}];
