import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

export const authorizationGuard: CanActivateFn = (route, state) => {
let toastr = inject(ToastrService)
let router = inject(Router)


  let token = localStorage.getItem('token')
  let user:any = localStorage.getItem('user')
  user = JSON.parse(user)

  if(token){
    if(state.url.includes('instructor')){
    if(user.RoleId == "2"){
    return true;
    }
    else{
      toastr.error('UnAuthorized')
    router.navigate(['auth/login'])
    localStorage.clear()
    return false;
    }
}
else if(state.url.includes('admin')){
  if(user.RoleId == "1"){
    return true;
    }
    else{
      toastr.error('UnAuthorized')
    router.navigate(['auth/login'])
    localStorage.clear()
    return false;
    }
}
else if(state.url.includes('learner')){
  if(user.RoleId == "3"){
    return true;
    }
    else if(state.url.includes('learner/StudentCv')){
      if(user.RoleId == "2"){
        return true;
        }
    }
    else{
      toastr.error('UnAuthorized')
    router.navigate(['auth/login'])
    localStorage.clear()
    return false;
    }
}
  }
  else if(state.url.includes('learner/StudentCv'))
    
      return true;
      
  else{
toastr.error('UnAuthorized')
router.navigate(['auth/login'])
return false;
  }
  return true;
};
