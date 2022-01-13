import React, { useState } from 'react';
import { PencilFill, XCircle } from 'react-bootstrap-icons';
import { useHistory } from 'react-router';
import ButtonIcon from 'src/components/ButtonIcon';
import { NotificationManager } from 'react-notifications';

import Table, { SortType } from 'src/components/Table';
import Info from './Info';
import ConfirmModal from 'src/components/ConfirmModal';
import { EDIT_USER_ID } from '../../../constants/pages';
import { DisableUserRequest } from 'src/containers/UsersList/sagas/requests';
import IUser from 'src/interfaces/User/IUser';
import IColumnOption from 'src/interfaces/IColumnOption';
import IPagedModel from 'src/interfaces/IPagedModel';
import { useAppDispatch } from 'src/hooks/redux';
import { disableUser } from '../reducer';

const columns: IColumnOption[] = [
    { columnName: 'Staff Code', columnValue: 'staffCode' },
    { columnName: 'Full Name', columnValue: 'fullName' },
    { columnName: 'Username', columnValue: 'username' },
    { columnName: 'Joined Date', columnValue: 'joinedDate' },
    { columnName: 'Type', columnValue: 'Type' },
];

type Props = {
    brands: IPagedModel<IUser> | null;
    handlePage: (page: number) => void;
    handleSort: (colValue: string) => void;
    sortState: SortType;
    fetchData: Function;
};

const UserTable = ({ users, handlePage, handleSort, sortState, fetchData }) => {
    const dispatch = useAppDispatch();

    const [showDetail, setShowDetail] = useState(false);
    const [userDetail, setUserDetail] = useState(null as IUser | null);
    const [disableState, setDisableState] = useState({
        isOpen: false,
        id: 0,
        title: '',
        message: '',
        isDisable: true,
    });

    const handleShowDisableBox = async (id) => {
        setDisableState({
            id,
            isOpen: true,
            title: 'Are you sure?',
            message: 'Do you want to disable this user?',
            isDisable: true,
        });
    };

    const handleCancelDisable = () => {
        setDisableState({
            isOpen: false,
            id: 0,
            title: '',
            message: '',
            isDisable: true,
        });
    };

    
    const handleShowInfo = (userId: string) => {
        const user = users?.items.find((item) => item.staffCode === userId);

        if (user) {
            setUserDetail(user);
            setShowDetail(true);
        }
    };

    const handleCloseDetail = () => {
        setShowDetail(false);
    };

    const handleResult = async (result, message) => {
        if (result) {
            handleCancelDisable();
            await fetchData();
            NotificationManager.success(
                `Disable user successful`,
                `Disable successful`,
                2000
            );
        } else {
            setDisableState({
                ...disableState,
                title: 'Can not disable user',
                message,
                isDisable: result,
            });
        }
    };

    const handleSubmitDisable = async () => {
        //const isSuccess = await DisableUserRequest(disableState.id);
        dispatch(
            disableUser({
                handleResult,
                userId: disableState.id
            })
        );
        /*if (isSuccess) {
            console.log("Disable success");
            await handleResult(true, '');
        }*/
    };

    const history = useHistory();
    const handleEdit = (staffCode: number) => {
        const existUser = users?.items.find((item) => item.staffCode === Number(staffCode));
        history.push(EDIT_USER_ID(staffCode), {
            existUser: existUser,
        });
    };

    // const history = useHistory();
    // const handleEdit = (staffCode: number) => {
    //     // const existUser = users?.items.find((item) => item.staffCode === Number(staffCode));
    //     history.push(EDIT_USER_ID(staffCode), {
    //         // existUser: existUser,
    //     });
    // };

    return (
        <>
            <Table
                columns={columns}
                handleSort={handleSort}
                sortState={sortState}
                page={{
                    currentPage: users?.currentPage,
                    totalPages: users?.totalPages,
                    handleChange: handlePage,
                }}
            >
                {users &&
                    users?.items?.map((data, index) => (
                        <tr
                            key={data.staffCode}
                            style={{ fontWeight: 'normal' }}
                            onClick={() => handleShowInfo(data.staffCode)}
                        >
                            <td>
                                {'SD' + String(data.staffCode).padStart(4, '0')}
                            </td>
                            {/* <td>{data.staffCode}</td> */}
                            <td>{data.fullName}</td>
                            <td>{data.username}</td>
                            <td>
                                {new Date(data.joinedDate).toLocaleString(
                                    'en-GB',
                                    {
                                        year: 'numeric',
                                        month: 'numeric',
                                        day: 'numeric',
                                    }
                                )}
                            </td>
                            <td>{data.type}</td>

                            <td className="d-flex">
                                <ButtonIcon onClick={() => handleEdit(data.staffCode)}>
                                    <PencilFill className="text-black" />
                                </ButtonIcon>
                                <ButtonIcon
                                    onClick={() => handleShowDisableBox(data.staffCode)}
                                >
                                    <XCircle className="text-danger mx-2" />
                                </ButtonIcon>
                            </td>
                        </tr>
                    ))}
            </Table>
            {userDetail && showDetail && (
                <Info user={userDetail} handleClose={handleCloseDetail} />
            )}
            <ConfirmModal
                title={disableState.title}
                isShow={disableState.isOpen}
                onHide={handleCancelDisable}
            >
                <div>
                    <div className="text-center">{disableState.message}</div>
                    {disableState.isDisable && (
                        <div className="text-center mt-3">
                            <button
                                className="btn btn-danger mr-3"
                                onClick={handleSubmitDisable}
                                type="button"
                            >
                                Disable
                            </button>

                            <button
                                className="btn btn-outline-secondary"
                                onClick={handleCancelDisable}
                                type="button"
                            >
                                Cancel
                            </button>
                        </div>
                    )}
                </div>
            </ConfirmModal>
        </>
    );
};

export default UserTable;
