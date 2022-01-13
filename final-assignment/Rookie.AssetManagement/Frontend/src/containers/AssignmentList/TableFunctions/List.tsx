import React, { useEffect, useState } from 'react';
import DatePicker from "react-datepicker";
import { FunnelFill, CalendarDateFill } from 'react-bootstrap-icons';
import { Search, Sidebar } from 'react-feather';
import ReactMultiSelectCheckboxes from 'react-multiselect-checkboxes';
import { Link } from 'react-router-dom';
import AssignmentTable from './Table';
import DateField from 'src/components/FormInputs/DateField';
import moment, { invalid } from 'moment';
import {
    AssetStateAssignmentOptions,
} from '../../../constants/selectOptions';
import { getAssignmentsRequest } from '../sagas/requests';
import {
    ASCENDING,
    DESCENDING,
    DEFAULT_USER_SORT_COLUMN_NAME,
    DEFAULT_USER_PAGE_LIMIT,
    DEFAULT_ASSET_SORT_COLUMN_NAME,
} from '../../../constants/paging';
import ISelectOption from '../../../interfaces/ISelectOption';
import IQueryAssignmentModel from '../../../interfaces/Assignment/IQueryAssignmentModel';
import { useAppDispatch, useAppSelector } from 'src/hooks/redux';
import { getAssignments, setAssignments } from '../reducer';
import { ASSIGNMENTMANAGER, HOME } from '../../../constants/pages';
import { date } from 'yup';

const _location = localStorage.getItem('location');

const _onTopAssetCodeLocalStoage = localStorage.getItem('onTopAssetCode');
const _onTopAssetCode = _onTopAssetCodeLocalStoage
    ? String(_onTopAssetCodeLocalStoage)
    : 0;
if (_onTopAssetCodeLocalStoage) {
    localStorage.removeItem('onTopAssetCode');
}

const ListAssignments = () => {
    const dispatch = useAppDispatch();
    const { assignments, loading } = useAppSelector((state) => state.assignmentReducer);
    const [query, setQuery] = useState({
        page: assignments?.currentPage ?? 1,
        limit: DEFAULT_USER_PAGE_LIMIT,
        sortOrder: ASCENDING,
        sortColumn: DEFAULT_ASSET_SORT_COLUMN_NAME,
        onTopAssignmentNumber: _onTopAssetCode,
    } as IQueryAssignmentModel);

    const [search, setSearch] = useState('');

    const [selectedState, setSelectedState] = useState([
        // { id: 1, label: 'All', value: 'All' },
    ] as ISelectOption[]);
    let date;

    const [selectedAssignedDate, setSelectedAssignedDate] = useState(date as Date);

    const handleStateOptions = (selected: ISelectOption[]) => {
        if (selected.length === 0) {
            setQuery({
                ...query,
                page: 1,
                states: [],
            });

            setSelectedState([AssetStateAssignmentOptions[0]]);
            return;
        }

        const selectedAll = selected.find((item) => item.id === 1);

        setSelectedState((prevSelected) => {
            if (!prevSelected.some((item) => item.id === 1) && selectedAll) {
                setQuery({
                    ...query,
                    page: 1,
                    states: [],
                });

                return [selectedAll];
            }

            const newSelected = selected.filter((item) => item.id !== 1);
            const states = newSelected.map((item) => item.value as string);
            console.log('newSelected');

            setQuery({
                ...query,
                page: 1,
                // state,
                states,
            });

            return newSelected;

        }
        );

    };

    const handleDateChange = (date: Date) => {
        if (date == null) {
            window.location.reload();
        }
        const offset = date?.getTimezoneOffset()
        const dateCovertUTC = new Date(moment.utc(date)?.subtract(offset, 'minutes')?.format('YYYY-MM-DDT00:00:00.000Z'))
        setSelectedAssignedDate(dateCovertUTC);
        setSelectedAssignedDate((dateCovertUTC) => {
            setQuery({
                ...query,
                page: 1,
                // state,
                assignedDate: dateCovertUTC,

            });
            return dateCovertUTC;
        });
    };

    // const handleCategoriesOptions = (selected: ISelectOption[]) => {
    //     if (selected.length === 0) {
    //         setQuery({
    //             ...query,
    //             page: 1,
    //             categories: [],
    //         });

    //         setSelectedCategory([AssetCategoriesOptions[0]]);
    //         return;
    //     }

    //     const selectedAll = selected.find((item) => item.id === 1);

    //     setSelectedCategory((prevSelected) => {
    //         if (!prevSelected.some((item) => item.id === 1) && selectedAll) {
    //             setQuery({
    //                 ...query,
    //                 page: 1,
    //                 categories: [],
    //             });

    //             return [selectedAll];
    //         }

    //         const newSelected = selected.filter((item) => item.id !== 1);
    //         const categories = newSelected.map((item) => item.value as string);

    //         setQuery({
    //             ...query,
    //             page: 1,
    //             // types,
    //             categories,
    //         });

    //         return newSelected;
    //     });
    // };

    const handleChangeSearch = (e) => {
        const search = e.target.value;
        setSearch(search);
    };

    const handlePage = (page: number) => {
        setQuery({
            ...query,
            page,
        });
    };

    const handleSearch = () => {
        setQuery({
            ...query,
            page: 1,
            search,
        });
    };

    const handleSort = (sortColumn: string) => {
        const sortOrder =
            query.sortOrder === ASCENDING ? DESCENDING : ASCENDING;

        setQuery({
            ...query,
            page: 1,
            sortColumn,
            sortOrder,
        });
    };

    const fetchData = () => {
        dispatch(getAssignments(query));
    };

    useEffect(() => {
        fetchData();
    }, [query]);

    console.log('list');
    console.log(assignments);

    const getDropdownButtonLabel = ({ placeholderButtonLabel, value }) => {
        if (placeholderButtonLabel == 'State') return 'State';
        else return 'Category';
    };
    return (
        <>
            <div className="primaryColor text-title intro-x">Assignment List</div>

            <div>
                <div className="d-flex mb-10 intro-x">
                    <div className="d-flex align-items-center w-md mr-5">
                        <div className="border d-flex">
                            <ReactMultiSelectCheckboxes
                                style={{ paddingBottom: 9 }}
                                options={AssetStateAssignmentOptions}
                                hideSearch={true}
                                placeholderButtonLabel="State"
                                getDropdownButtonLabel={getDropdownButtonLabel}
                                value={selectedState}
                                onChange={handleStateOptions}
                            />
                        </div>
                        <div className="border py-1 px-2 text-dark">
                            <FunnelFill />
                        </div>
                        <div className="px-2"></div>
                        <DatePicker
                            placeholderText='Assigned Date'
                            selected={selectedAssignedDate}
                            dateFormat='dd/MM/yyyy'
                            showYearDropdown
                            isClearable
                            scrollableMonthYearDropdown
                            onChange={handleDateChange}
                        //minDate={moment().toDate()}
                        />
                        <div className="border py-1 px-2 text-dark">
                            <CalendarDateFill />
                        </div>

                    </div>
                    <div className="d-flex align-items-center w-ld ml-auto">
                        <div className="input-group border">
                            <input
                                onChange={handleChangeSearch}
                                value={search}
                                type="text"
                                className="form-control h-100"
                            />
                            <span
                                onClick={handleSearch}
                                className="px-2 pt-1 pointer"
                            >
                                <Search />
                            </span>
                        </div>
                    </div>

                    <div className="d-flex align-items-center ml-3">
                        <Link
                            to={HOME}
                            type="button"
                            className="btn btn-danger"
                        >
                            Create new assignment
                        </Link>
                    </div>
                </div>

                <AssignmentTable
                    assignments={assignments}
                    handlePage={handlePage}
                    handleSort={handleSort}
                    sortState={{
                        columnValue: query.sortColumn,
                        orderBy: query.sortOrder,
                    }}
                    fetchData={fetchData}
                />
            </div>
        </>
    );
};

export default ListAssignments;
