import { Routes } from '@angular/router';
import { ACOCUNT_ROUTERS, HOME_ROUTERS } from './core/constants/route.config';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
        path: ACOCUNT_ROUTERS.root,
        loadChildren: () => import('./account/account.routes')
    },
    {
        path: HOME_ROUTERS.root,
        canActivate: [authGuard],
        loadChildren: () => import('./home/home.routes')
    },
    {
        path: '',
        redirectTo: HOME_ROUTERS.root,
        pathMatch: 'full'
    }
];
