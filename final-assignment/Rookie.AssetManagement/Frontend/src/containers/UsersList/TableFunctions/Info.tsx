import React from 'react';
import { Modal } from 'react-bootstrap';

import IUser from '../../../interfaces/User/IUser';
// import formatDateTime from 'src/utils/formatDateTime';
// import {
//     NormalBrandType,
//     NormalBrandTypeLabel,
//     LuxuryBrandType,
//     LuxyryBrandTypeLabel,
// } from 'src/constants/Brand/BrandConstants';

type Props = {
    user: IUser;
    handleClose: () => void;
};

const Info: React.FC<Props> = ({ user, handleClose }) => {
    // const getBrandTypeName = (id: number) => {
    //     return id == LuxuryBrandType
    //         ? LuxyryBrandTypeLabel
    //         : NormalBrandTypeLabel;
    // };

    return (
        <>
            <Modal show={true} onHide={handleClose} dialogClassName="modal-90w">
                <Modal.Header closeButton>
                    <Modal.Title id="login-modal">
                        Detailed User Information
                    </Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <div>
                        <div className="row -intro-y">
                            <div className="col-4">Staff Code:</div>
                            <div>{'SD' + String(user.staffCode).padStart(4, '0')}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Full Name:</div>
                            <div>{user.fullName}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Username:</div>
                            <div>{user.username}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Date of Birth:</div>
                            <div>{new Date(user.dateOfBirth).toLocaleString(
                                'en-GB',
                                {
                                    year: 'numeric',
                                    month: 'numeric',
                                    day: 'numeric',
                                }
                            )}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Gender:</div>
                            <div>{user.gender}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Joined Date:</div>
                            {new Date(user.joinedDate).toLocaleString('en-GB', {
                                year: 'numeric',
                                month: 'numeric',
                                day: 'numeric',
                            })}
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Type:</div>
                            {/* <div>{getBrandTypeName(user.type)}</div> */}
                            <div>{user.type}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Location:</div>
                            <div>{user.location}</div>
                        </div>
                    </div>
                </Modal.Body>
            </Modal>
        </>
    );
};

export default Info;
