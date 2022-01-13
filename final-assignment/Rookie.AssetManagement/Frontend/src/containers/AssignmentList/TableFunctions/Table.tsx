import React, { useState } from 'react';
import { Back, PencilFill, XCircle } from 'react-bootstrap-icons';
import { useHistory } from 'react-router';
import ButtonIcon from '../../../components/ButtonIcon';
import { NotificationManager } from 'react-notifications';
import Table, { SortType } from '../../../components/Table';
import Info from './Info';
import ConfirmModal from '../../../components/ConfirmModal';
import IAssignment from '../../../interfaces/Assignment/IAssignment';
import IColumnOption from '../../../interfaces/IColumnOption';
import IPagedModel from '../../../interfaces/IPagedModel';
import { useAppDispatch } from 'src/hooks/redux';

const columns: IColumnOption[] = [
    { columnName: 'No.', columnValue: 'assignmentNumber' },
    { columnName: 'Asset Code', columnValue: 'assetCode' },
    { columnName: 'Asset Name', columnValue: 'assetName' },
    { columnName: 'Assigned to', columnValue: 'assignedTo' },
    { columnName: 'Assigned by', columnValue: 'assignedBy' },
    { columnName: 'Assigned Date', columnValue: 'assignedDate' },
    { columnName: 'State', columnValue: 'state' },
];

type Props = {
    assignments: IPagedModel<IAssignment> | null;
    handlePage: (page: number) => void;
    handleSort: (colValue: string) => void;
    sortState: SortType;
    fetchData: Function;
};

const AssignmentTable = ({
    assignments,
    handlePage,
    handleSort,
    sortState,
    fetchData,
}) => {
    const dispatch = useAppDispatch();

    const [showDetail, setShowDetail] = useState(false);
    const [assignmentDetail, setAssignmentDetail] = useState(null as IAssignment | null);

    const handleShowInfo = (assetCode: string) => {
        const assignment = assignments?.items.find(
            (item) => item.assetCode === assetCode
        );

        if (assignment) {
            setAssignmentDetail(assignment);
            setShowDetail(true);
        }
    };

    const handleCloseDetail = () => {
        setShowDetail(false);
    };

    return (
        <>
            <Table
                columns={columns}
                handleSort={handleSort}
                sortState={sortState}
                page={{
                    currentPage: assignments?.currentPage,
                    totalPages: assignments?.totalPages,
                    handleChange: handlePage,
                }}
            >
                {assignments &&
                    assignments?.items?.map((data, index) => (
                        <tr
                            key={data.assetCode}
                            style={{ fontWeight: 'normal', fontSize: '16px' }}
                            onClick={() => handleShowInfo(data.assetCode)}
                        >
                            <td>
                                {data.assignmentNumber}
                            </td>
                            <td>
                                {/* {'SD' + String(data.assetCode).padStart(4, '0')} */}
                                {data.assetCode}
                            </td>
                            <td>{data.assetName}</td>
                            <td>{data.assignedTo}</td>
                            <td>{data.assignedBy}</td>
                            <td>
                                {new Date(data.assignedDate).toLocaleString(
                                    'en-GB',
                                    {
                                        year: 'numeric',
                                        month: 'numeric',
                                        day: 'numeric',
                                    }
                                )}
                            </td>
                            <td>{data.state}</td>

                            <td className="d-flex">
                                <ButtonIcon
                                    onClick={() => null}
                                >
                                    <PencilFill className="text-black" />
                                </ButtonIcon>
                                <ButtonIcon
                                    onClick={() =>
                                        null
                                    }
                                >
                                    <XCircle className="text-danger mx-2" />
                                </ButtonIcon>
                            </td>
                        </tr>
                    ))}
            </Table>
            {assignmentDetail && showDetail && (
                <Info assignment={assignmentDetail} handleClose={handleCloseDetail} />
            )}
        </>
    );
};

export default AssignmentTable;
