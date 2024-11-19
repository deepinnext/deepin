import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  return authService.isSignedIn().pipe(map(isSignedIn => {
    if (!isSignedIn) {
      router.navigate(['/account/sign-in'], { queryParams: { returnUrl: state.url } });
    }
    return isSignedIn;
  }));
};
