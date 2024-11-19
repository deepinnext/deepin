import { Route } from "@angular/router";
import { AccountComponent } from "./account.component";
import { ACOCUNT_ROUTERS } from "../core/constants/route.config";

export default [
    {
        path: '',
        component: AccountComponent,
        children: [
            { path: ACOCUNT_ROUTERS.login, loadComponent: () => import('./login/login.component').then(c => c.LoginComponent) },
            { path: ACOCUNT_ROUTERS.register, loadComponent: () => import('./register/register.component').then(c => c.RegisterComponent) },
            { path: ACOCUNT_ROUTERS.forgotPassword, loadComponent: () => import('./forgot-password/forgot-password.component').then(c => c.ForgotPasswordComponent) },
            { path: ACOCUNT_ROUTERS.registerConfirmation, loadComponent: () => import('./register-confirmation/register-confirmation.component').then(c => c.RegisterConfirmationComponent) },
            { path: ACOCUNT_ROUTERS.resetPassword, loadComponent: () => import('./reset-password/reset-password.component').then(c => c.ResetPasswordComponent) },
        ]
    }
] satisfies Route[];
