import { takeLatest } from 'redux-saga/effects';

import { getAssignments, } from '../reducer';
import {
    handleGetAssignments,
} from './handles';

export default function* AssignmentSagas() {
    yield takeLatest(getAssignments.type, handleGetAssignments);
}
