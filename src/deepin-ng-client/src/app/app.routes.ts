import { Routes } from '@angular/router';
import { ACOCUNT_ROUTERS, HOME_ROUTERS, NOTES_ROUTERS, PAGE_ROUTERS } from './core/constants/route.config';
import { authGuard } from './core/guards/auth.guard';
import { LayoutComponent } from './shared/components/layout/layout.component';

export const routes: Routes = [
    {
        path: ACOCUNT_ROUTERS.root,
        loadChildren: () => import('./account/account.routes')
    },
    {
        path: '',
        component: LayoutComponent,
        children: [
            {
                path: HOME_ROUTERS.root,
                canActivate: [authGuard],
                loadChildren: () => import('./home/home.routes')
            },
            {
                path: PAGE_ROUTERS.root,
                canActivate: [authGuard],
                loadChildren: () => import('./page/page.routes')
            },
            {
                path: NOTES_ROUTERS.root,
                canActivate: [authGuard],
                loadChildren: () => import('./notes/notes.routes')
            },
            {
                path: '',
                redirectTo: HOME_ROUTERS.root,
                pathMatch: 'full'
            }
        ]
    }
];
