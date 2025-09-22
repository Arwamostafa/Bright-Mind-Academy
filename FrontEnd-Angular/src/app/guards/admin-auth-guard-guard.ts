import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserAuthentication } from '../services/user-authentication';

export const adminAuthGuardGuard: CanActivateFn = (route, state) => {
  let _authService = inject(UserAuthentication);
  let _router = inject(Router);

  if(_authService.isAdminLogged()){
    return true;
  }else{
    _router.navigateByUrl("/login");
    return false
  }
};
