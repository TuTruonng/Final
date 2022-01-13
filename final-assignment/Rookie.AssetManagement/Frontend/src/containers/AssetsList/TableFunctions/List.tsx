import React, { useEffect, useState } from 'react';
import { AxiosResponse } from 'axios';
import { FunnelFill } from 'react-bootstrap-icons';
import { Search, Sidebar } from 'react-feather';
import ReactMultiSelectCheckboxes from 'react-multiselect-checkboxes';
import { Link } from 'react-router-dom';
import AssetTable from './Table';

import {
    AssetStateOptions,
    AssetCategoriesOptions,
} from '../../../constants/selectOptions';
import { getAssetsRequest } from 'src/containers/AssetsList/sagas/requests';
import {
    ASCENDING,
    DESCENDING,
    DEFAULT_USER_SORT_COLUMN_NAME,
    DEFAULT_USER_PAGE_LIMIT,
    DEFAULT_ASSET_SORT_COLUMN_NAME,
} from '../../../constants/paging';
import ISelectOption from 'src/interfaces/ISelectOption';
import IQueryAssetModel from 'src/interfaces/Asset/IQueryAssetModel';
import { useAppDispatch, useAppSelector } from 'src/hooks/redux';
import { getAssets, setAssets } from '../reducer';
import { CREATEASSET, ASSETMANAGER } from 'src/constants/pages';

const _location = localStorage.getItem('location');

const _onTopAssetCodeLocalStoage = localStorage.getItem('onTopAssetCode');
const _onTopAssetCode = _onTopAssetCodeLocalStoage
    ? String(_onTopAssetCodeLocalStoage)
    : 0;
if (_onTopAssetCodeLocalStoage) {
    localStorage.removeItem('onTopAssetCode');
}

const ListAssets = () => {
    const dispatch = useAppDispatch();
    const { assets, loading } = useAppSelector((state) => state.assetReducer);

    const [query, setQuery] = useState({
        page: assets?.currentPage ?? 1,
        limit: DEFAULT_USER_PAGE_LIMIT,
        sortOrder: ASCENDING,
        sortColumn: DEFAULT_ASSET_SORT_COLUMN_NAME,
        location: _location,
        onTopAssetCode: _onTopAssetCode,
    } as IQueryAssetModel);

    const [search, setSearch] = useState('');

    const [selectedState, setSelectedState] = useState([
        // { id: 1, label: 'All', value: 'All' },
    ] as ISelectOption[]);

    const [selectedCategory, setSelectedCategory] = useState([
        // { id: 1, label: 'All', value: 'All' },
    ] as ISelectOption[]);

    const handleStateOptions = (selected: ISelectOption[]) => {
        if (selected.length === 0) {
            setQuery({
                ...query,
                page: 1,
                states: [],
            });

            setSelectedState([AssetStateOptions[0]]);
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

            setQuery({
                ...query,
                page: 1,
                // state,
                states,
            });

            return newSelected;
        });
    };

    const handleCategoriesOptions = (selected: ISelectOption[]) => {
        if (selected.length === 0) {
            setQuery({
                ...query,
                page: 1,
                categories: [],
            });

            setSelectedCategory([AssetCategoriesOptions[0]]);
            return;
        }

        const selectedAll = selected.find((item) => item.id === 1);

        setSelectedCategory((prevSelected) => {
            if (!prevSelected.some((item) => item.id === 1) && selectedAll) {
                setQuery({
                    ...query,
                    page: 1,
                    categories: [],
                });

                return [selectedAll];
            }

            const newSelected = selected.filter((item) => item.id !== 1);
            const categories = newSelected.map((item) => item.value as string);

            setQuery({
                ...query,
                page: 1,
                // types,
                categories,
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
        dispatch(getAssets(query));
    };

    useEffect(() => {
        fetchData();
    }, [query]);

    const getDropdownButtonLabel = ({ placeholderButtonLabel, value }) => {
        if (placeholderButtonLabel == 'State') return 'States';
        else return 'Category';
    };
    return (
        <>
            <div className="primaryColor text-title intro-x">Asset List</div>
            <div>
                <div className="d-flex mb-5 intro-x">
                    <div className="d-flex align-items-center w-md mr-5">
                        <div className="border d-flex">
                            <ReactMultiSelectCheckboxes
                                style={{ paddingBottom: 9 }}
                                options={AssetStateOptions}
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
                        <div className="border d-flex">
                            <ReactMultiSelectCheckboxes
                                style={{ paddingBottom: 9 }}
                                options={AssetCategoriesOptions}
                                hideSearch={true}
                                placeholderButtonLabel="Category"
                                getDropdownButtonLabel={getDropdownButtonLabel}
                                value={selectedCategory}
                                onChange={handleCategoriesOptions}
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
                            to={CREATEASSET}
                            type="button"
                            className="btn btn-danger"
                        >
                            Create new asset
                        </Link>
                    </div>
                </div>

                <AssetTable
                    assets={assets}
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

export default ListAssets;
