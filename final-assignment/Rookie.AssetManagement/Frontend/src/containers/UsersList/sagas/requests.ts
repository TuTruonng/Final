import IUser from 'src/interfaces/User/IUser';
import IUserForm from 'src/interfaces/User/IUserForm';
import axios, { AxiosResponse } from 'axios';
import qs from 'qs';
import RequestService from 'src/services/request';
import EndPoints from 'src/constants/endpoints';
import IQueryUserModel from 'src/interfaces/User/IQueryUserModel';

export function createUserRequest(
    UserForm: IUserForm
): Promise<AxiosResponse<IUser>> {
    return RequestService.axios.post(EndPoints.users, UserForm).then((response) => {
        if (response.data) {
            localStorage.setItem("onTopStaffCode", response.data.staffCode)
        }
        return response
    });
}

export function getUsersRequest(
    query: IQueryUserModel
): Promise<AxiosResponse<IUser>> {
    return RequestService.axios.get(EndPoints.users, {
        params: query,
        paramsSerializer: (params) => qs.stringify(params),
    });
}

export function UpdateUserRequest(
    UserForm: IUserForm
): Promise<AxiosResponse<IUser>> {
    const formData = new FormData();
    Object.keys(UserForm).forEach((key) => {
        if (key === 'joinedDate' && UserForm.joinedDate instanceof Date) {
            formData.append(key, (UserForm[key] as Date).toISOString())
        }
        else if (key === 'dateOfBirth' && UserForm.dateOfBirth instanceof Date) {
            formData.append(key, (UserForm[key] as Date).toISOString())
        } else {
            formData.append(key, UserForm[key]);
        }
    });

    return RequestService.axios.put(
        EndPoints.usersId(UserForm.staffCode ?? -1),
        formData
    ).then((response) => {
        if (response.data) {
            localStorage.setItem("onTopStaffCode", response.data.staffCode)
        }
        return response
    });
}

export function DisableUserRequest(
    id: number
): Promise<AxiosResponse<Boolean>> {
    return RequestService.axios.put(EndPoints.disableId(id));
}
