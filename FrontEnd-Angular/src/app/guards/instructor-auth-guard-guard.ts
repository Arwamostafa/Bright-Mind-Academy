import { CanActivateFn, Router } from '@angular/router';
import { UserAuthentication } from '../services/user-authentication';
import { inject } from '@angular/core';

export const instructorAuthGuardGuard: CanActivateFn = (route, state) => {
  let _authService = inject(UserAuthentication);
  let _router = inject(Router);

  if(_authService.isInstructorLogged()){
    return true;
  }else{
    _router.navigateByUrl("/login");
    return false
  }
};
