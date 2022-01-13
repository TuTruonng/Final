import axios, { AxiosResponse } from 'axios';
import qs from 'qs';

import RequestService from '../../services/request';

import EndPoints from '../../constants/endpoints';

export function Login(Form) {
    return RequestService.axios.post(EndPoints.authentication, Form);
}

export function createUserRequest(Form) {
    return RequestService.axios.post(EndPoints.users, Form);
}

export function getUsersRequest(query) {
    return RequestService.axios
    .get(EndPoints.users, {
        params: query,
        paramsSerializer: (params) => qs.stringify(params),
    })
}

export function UpdateUserRequest(Form) {
    const formData = new FormData();

    Object.keys(Form).forEach((key) => {
        formData.append(key, Form[key]);
    });

    return RequestService.axios.put(EndPoints.usersId(Form.staffCode ?? -1), formData);
}

export function DisableUserRequest(id) {
    return RequestService.axios
    .put(EndPoints.disableId(id ?? -1), id);
}

export function DeleteUserRequest(id) {
    return RequestService.axios.delete(EndPoints.usersId(id));
}
