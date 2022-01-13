import IAssignment from '../../../interfaces/Assignment/IAssignment';
import IAssignmentForm from '../../../interfaces/Assignment/IAssignmentForm';
import axios, { AxiosResponse } from 'axios';
import qs from 'qs';
import RequestService from '../../../services/request';
import EndPoints from '../../../constants/endpoints';
import IQueryAssignmentModel from '../../../interfaces/Assignment/IQueryAssignmentModel';

export function getAssignmentsRequest(
    query: IQueryAssignmentModel
): Promise<AxiosResponse<IAssignment>> {
    return RequestService.axios.get(EndPoints.assignments, {
        params: query,
        paramsSerializer: (params) => qs.stringify(params),
    });
}
