import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserAuthentication } from '../services/user-authentication';

export const instructorOrStudentAuthGuardGuard: CanActivateFn = (route, state) => {
  let _authService = inject(UserAuthentication);
  let _router = inject(Router);

  if(_authService.isStudentLogged() || _authService.isInstructorLogged()){
    return true;
  }else{
    _router.navigateByUrl("/login");
    return false
  }
};
