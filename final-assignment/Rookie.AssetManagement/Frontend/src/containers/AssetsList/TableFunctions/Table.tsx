import React, { useState } from 'react';
import { PencilFill, XCircle } from 'react-bootstrap-icons';
import { useHistory } from 'react-router';
import ButtonIcon from 'src/components/ButtonIcon';
import { NotificationManager } from 'react-notifications';

import Table, { SortType } from 'src/components/Table';
import Info from './Info';
import ConfirmModal from 'src/components/ConfirmModal';
import { EDIT_ASSET_ID } from '../../../constants/pages';
import { DisableAssetRequest } from 'src/containers/AssetsList/sagas/requests';
import IAsset from 'src/interfaces/Asset/IAsset';
import IColumnOption from 'src/interfaces/IColumnOption';
import IPagedModel from 'src/interfaces/IPagedModel';
import { useAppDispatch } from 'src/hooks/redux';
import { disableAsset } from '../reducer';

const columns: IColumnOption[] = [
    { columnName: 'Asset Code', columnValue: 'assetCode' },
    { columnName: 'Asset Name', columnValue: 'assetName' },
    { columnName: 'Category', columnValue: 'category' },
    { columnName: 'State', columnValue: 'state' },
];

type Props = {
    assets: IPagedModel<IAsset> | null;
    handlePage: (page: number) => void;
    handleSort: (colValue: string) => void;
    sortState: SortType;
    fetchData: Function;
};

const AssetTable = ({
    assets,
    handlePage,
    handleSort,
    sortState,
    fetchData,
}) => {
    const dispatch = useAppDispatch();

    const [showDetail, setShowDetail] = useState(false);
    const [assetDetail, setAssetDetail] = useState(null as IAsset | null);
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
            message: 'Do you want to disable this asset?',
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

    const handleShowInfo = (assetCode: string) => {
        const asset = assets?.items.find(
            (item) => item.assetCode === assetCode
        );

        if (asset) {
            setAssetDetail(asset);
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
                `Disable asset successful`,
                `Disable successful`,
                2000
            );
        } else {
            setDisableState({
                ...disableState,
                title: 'Can not disable asset',
                message,
                isDisable: result,
            });
        }
    };

    const handleSubmitDisable = async () => {
        //const isSuccess = await DisableUserRequest(disableState.id);
        dispatch(
            disableAsset({
                handleResult,
                assetId: disableState.id,
            })
        );
        /*if (isSuccess) {
            console.log("Disable success");
            await handleResult(true, '');
        }*/
    };

    const history = useHistory();
    const handleEdit = (assetCode: string) => {
        const existAsset = assets?.items.find(
            (item) => item.assetCode === assetCode
        );
        history.push(EDIT_ASSET_ID(assetCode), {
            existAsset: existAsset,
        });
        console.log(existAsset);
    };

    return (
        console.log('assets'),
        console.log(assets),
        <>
            <Table
                columns={columns}
                handleSort={handleSort}
                sortState={sortState}
                page={{
                    currentPage: assets?.currentPage,
                    totalPages: assets?.totalPages,
                    handleChange: handlePage,
                }}
            >
                {assets &&
                    assets?.items?.map((data, index) => (
                        <tr
                            key={data.assetCode}
                            style={{ fontWeight: 'normal' }}
                            onClick={() => handleShowInfo(data.assetCode)}
                        >
                            <td>
                                {/* {'SD' + String(data.assetCode).padStart(4, '0')} */}
                                {data.assetCode}
                            </td>
                            {/* <td>{data.staffCode}</td> */}
                            <td>{data.assetName}</td>
                            <td>{data.category}</td>
                            <td>{data.state}</td>

                            <td className="d-flex">
                                <ButtonIcon
                                    onClick={() => handleEdit(data.assetCode)}
                                >
                                    <PencilFill className="text-black" />
                                </ButtonIcon>
                                <ButtonIcon
                                    onClick={() =>
                                        handleShowDisableBox(data.assetCode)
                                    }
                                >
                                    <XCircle className="text-danger mx-2" />
                                </ButtonIcon>
                            </td>
                        </tr>
                    ))}
            </Table>
            {assetDetail && showDetail && (
                <Info asset={assetDetail} handleClose={handleCloseDetail} />
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

export default AssetTable;
