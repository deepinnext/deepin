import { Route } from "@angular/router";
import { PageComponent } from "./page.component";
import { PAGE_ROUTERS } from "../core/constants/route.config";

export default [
    {
        path: '',
        component: PageComponent,
        children: [
            {
                path: PAGE_ROUTERS.new,
                loadComponent: () => import('./edit/edit.component').then(c => c.EditComponent)
            },
            {
                path: PAGE_ROUTERS.edit,
                loadComponent: () => import('./edit/edit.component').then(c => c.EditComponent),
            },
            {
                path: PAGE_ROUTERS.detail,
                loadComponent: () => import('./detail/detail.component').then(c => c.DetailComponent)
            }
        ]
    }
] satisfies Route[];
