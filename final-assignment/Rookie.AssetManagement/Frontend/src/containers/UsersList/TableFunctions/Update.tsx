import React, { useEffect, useState } from 'react';
import { Redirect, useParams } from 'react-router';
import { useHistory, useLocation } from 'react-router-dom';
import { NOTFOUND } from 'src/constants/pages';
import { useAppSelector } from 'src/hooks/redux';
import IUserForm from 'src/interfaces/User/IUserForm';
import { string } from 'yup/lib/locale';
import Users from '..';
import UserFormContainer from '../UserForm';
import UserForm from '../UserForm';

const UpdateUserContainer = () => {
    const { users } = useAppSelector((state) => state.userReducer);
    const [user, setUser] = useState(undefined as IUserForm | undefined);
    const { staffCode } = useParams<{ staffCode: string }>();
    const { state } = useLocation<{existUser: IUserForm}>();
    const { existUser } = state; // Read values passed on state

    useEffect(() => {
        if (existUser) {
            setUser(
                //undefined
                {
                    firstName: existUser.firstName,
                    lastName: existUser.lastName,
                    dateOfBirth: existUser.dateOfBirth,
                    gender: existUser.gender,
                    joinedDate: existUser.joinedDate,
                    type: existUser.type,
                    location: existUser.location,
                    staffCode: existUser.staffCode,
                }
            );
        }
    }, [existUser]);

    return (
        <div className="ml-5">
            <div className="primaryColor text-title intro-x">Edit User</div>

            <div className="row">
                {/* <UserFormContainer /> */}
                {user && <UserForm initialUserForm={user} />}
            </div>
        </div>
    );
};

export default UpdateUserContainer;
