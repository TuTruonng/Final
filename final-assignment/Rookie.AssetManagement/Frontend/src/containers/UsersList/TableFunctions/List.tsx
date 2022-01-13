import React, { useEffect, useState } from 'react';
import { AxiosResponse } from 'axios';
import { FunnelFill } from 'react-bootstrap-icons';
import { Search, Sidebar } from 'react-feather';
import ReactMultiSelectCheckboxes from 'react-multiselect-checkboxes';
import { Link } from 'react-router-dom';
import UserTable from './Table';

import { UserTypeOptions } from '../../../constants/selectOptions';
import { getUsersRequest } from 'src/containers/UsersList/sagas/requests';
import {
    ASCENDING,
    DESCENDING,
    DEFAULT_USER_SORT_COLUMN_NAME,
    DEFAULT_USER_PAGE_LIMIT,
} from '../../../constants/paging';
import ISelectOption from 'src/interfaces/ISelectOption';
import IQueryUserModel from 'src/interfaces/User/IQueryUserModel';
import { useAppDispatch, useAppSelector } from 'src/hooks/redux';
import { getUsers, setUsers } from '../reducer';
import { CREATEUSER, USERMANAGER } from 'src/constants/pages';

const _location = localStorage.getItem('location');
const _onTopStaffCodeLocalStoage = localStorage.getItem('onTopStaffCode')
const _onTopStaffCode = _onTopStaffCodeLocalStoage ? Number(_onTopStaffCodeLocalStoage) : 0
if (_onTopStaffCodeLocalStoage) {
    localStorage.removeItem('onTopStaffCode')
}
const ListUsers = () => {
    const dispatch = useAppDispatch();
    const { users, loading } = useAppSelector((state) => state.userReducer);

    const [query, setQuery] = useState({
        page: users?.currentPage ?? 1,
        limit: DEFAULT_USER_PAGE_LIMIT,
        sortOrder: ASCENDING,
        sortColumn: DEFAULT_USER_SORT_COLUMN_NAME,
        location: _location,
        onTopStaffCode: _onTopStaffCode,
    } as IQueryUserModel);

    const [search, setSearch] = useState('');

    const [selectedType, setSelectedType] = useState([
        { id: 1, label: 'All', value: 'All' },
    ] as ISelectOption[]);

    const handleType = (selected: ISelectOption[]) => {
        if (selected.length === 0) {
            setQuery({
                ...query,
                page: 1,
                types: [],
            });

            setSelectedType([UserTypeOptions[0]]);
            return;
        }

        const selectedAll = selected.find((item) => item.id === 1);

        setSelectedType((prevSelected) => {
            if (!prevSelected.some((item) => item.id === 1) && selectedAll) {
                setQuery({
                    ...query,
                    page: 1,
                    types: [],
                });

                return [selectedAll];
            }

            const newSelected = selected.filter((item) => item.id !== 1);
            const types = newSelected.map((item) => item.value as string);

            setQuery({
                ...query,
                page: 1,
                // types,
                types,
            });

            return newSelected;
        });
    };

    const handleChangeSearch = (e) => {
        e.preventDefault();
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
        console.log('sort')
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
        dispatch(getUsers(query));
    };

    useEffect(() => {
        fetchData();
        // dispatch(setUsers(users));
    }, [query]);

    return (
        <>
            <div className="primaryColor text-title intro-x">User List</div>

            <div>
                <div className="d-flex mb-5 intro-x">
                    <div className="d-flex align-items-center w-md mr-5">
                        <div className="border d-flex">
                            <ReactMultiSelectCheckboxes
                                style={{ paddingBottom: 9 }}
                                options={UserTypeOptions}
                                hideSearch={true}
                                placeholderButtonLabel="Type"
                                value={selectedType}
                                onChange={handleType}
                            />
                        </div>
                        <div className="border py-1 px-2 text-dark">
                            <FunnelFill />
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
                            to={CREATEUSER}
                            type="button"
                            className="btn btn-danger"
                        >
                            Create new user
                        </Link>
                    </div>
                </div>

                <UserTable
                    users={users}
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

export default ListUsers;
