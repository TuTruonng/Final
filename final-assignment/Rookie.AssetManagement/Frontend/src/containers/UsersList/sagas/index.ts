import { takeLatest } from 'redux-saga/effects';

import { createUser, getUsers, updateUser, disableUser } from '../reducer';
import {
    handleCreateUser,
    handleGetUsers,
    handleUpdateUser,
    handleDisableUser,
} from './handles';

export default function* UserSagas() {
    yield takeLatest(createUser.type, handleCreateUser);
    yield takeLatest(getUsers.type, handleGetUsers);
    yield takeLatest(updateUser.type, handleUpdateUser);
    yield takeLatest(disableUser.type, handleDisableUser);
}
