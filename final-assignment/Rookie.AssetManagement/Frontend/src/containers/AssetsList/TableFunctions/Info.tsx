import React from 'react';
import { Modal, Table } from 'react-bootstrap';
import IColumnOption from 'src/interfaces/IColumnOption';
import IAsset from '../../../interfaces/Asset/IAsset';

type Props = {
    asset: IAsset;
    handleClose: () => void;
};

const Info: React.FC<Props> = ({ asset, handleClose }) => {
    return (
        <>
            <Modal show={true} onHide={handleClose} dialogClassName="modal-90w">
                <Modal.Header closeButton>
                    <Modal.Title id="login-modal">
                        Detailed Asset Information
                    </Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <div>
                        <div className="row -intro-y">
                            <div className="col-4">Asset Code</div>
                            <div>
                                {String(asset.assetCode).padStart(4, '0')}
                            </div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Asset Name</div>
                            <div>{asset.assetName}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Category</div>
                            <div>{asset.category}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Installed Date</div>
                            <div>
                                {new Date(asset.installedDate).toLocaleString(
                                    'en-GB',
                                    {
                                        year: 'numeric',
                                        month: 'numeric',
                                        day: 'numeric',
                                    }
                                )}
                            </div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">State</div>
                            <div>{asset.state}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Location</div>
                            <div>{asset.location}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">Specification</div>
                            <div>{asset.specification}</div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-3">History
                            </div>
                            <div className="col-5">
                                <Table>
                                    <thead>
                                        <tr>
                                            <th>Date</th>
                                            <th>Assigned to</th>
                                            <th>Assigned by</th>
                                            <th>Returned Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>12/10/2018</td>
                                            <td>hungtv1</td>
                                            <td>binhnv</td>
                                            <td>07/03/2019</td>
                                        </tr>
                                        <tr>
                                            <td>10/03/2019</td>
                                            <td>thinhptx</td>
                                            <td>tuanha</td>
                                            <td>22/03/2020</td>
                                        </tr>
                                    </tbody>
                                </Table>
                            </div>
                            <div>

                            </div>
                        </div>
                    </div>
                </Modal.Body>
            </Modal>
        </>
    );
};

export default Info;
