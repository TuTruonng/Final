import { AxiosResponse } from "axios";
import RequestService from 'src/services/request';
import EndPoints from 'src/constants/endpoints';
import ILoginModel from "src/interfaces/ILoginModel";
import IAccount from "src/interfaces/IAccount";
import IChangePassword from "src/interfaces/IChangePassword";
import IChangePasswordUser from "src/interfaces/IChangePasswordUser";

export function loginRequest(login: ILoginModel): Promise<AxiosResponse<IAccount>> {
  return RequestService.axios
    .post(EndPoints.authentication, login)
    .then(response => {
      if (response.data) {
        localStorage.setItem('role', response.data.role);
        localStorage.setItem('user', response.data.user);
        localStorage.setItem('status', response.data.status);
        localStorage.setItem('location', response.data.location);
        localStorage.setItem('token', response.data.token);
        // localStorage.setItem('active', response.data.active);
      }
      return response.data;
    });
  // .catch(
  //   function (error) {
  //     console.log('Show error notification!')
  //     localStorage.setItem('message', error.response.data.message);
  //     console.log(error.response.data.message)
  //   }
  // );    
}

export function getUser(): Promise<AxiosResponse<IAccount>> {
  return RequestService.axios.get(EndPoints.users);
}


export function getMeRequest(): Promise<AxiosResponse<IAccount>> {
  return RequestService.axios.get(EndPoints.me);
}

export function putChangePassword(changePasswordModel: IChangePassword): Promise<AxiosResponse<IAccount>> {
  return RequestService.axios
    .put(EndPoints.authentication, changePasswordModel)
    .then(response => {
      if (response.data) {
        localStorage.setItem('status', response.data.status);
      }
      return response.data;

    })
}


export function putChangePasswordUser(changePasswordModel: IChangePasswordUser): Promise<AxiosResponse<IAccount>> {
  return RequestService.axios
    .put(EndPoints.changePassword, changePasswordModel)
    .then(response => {
      if (response.data)
        //     localStorage.setItem('status', response.data.status);
        //   }
        return response.data;
    });
}

