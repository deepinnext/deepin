import { Route } from "@angular/router"; 
import { PAGE_ROUTERS } from "../core/constants/route.config";
import { NotesComponent } from "./notes.component";

export default [
    {
        path: '',
        component: NotesComponent,
        children: [
            {
                path: PAGE_ROUTERS.new,
                loadComponent: () => import('./edit/edit.component').then(c => c.EditComponent)
            },
            {
                path: PAGE_ROUTERS.edit,
                loadComponent: () => import('./edit/edit.component').then(c => c.EditComponent),
            }
        ]
    }
] satisfies Route[];
