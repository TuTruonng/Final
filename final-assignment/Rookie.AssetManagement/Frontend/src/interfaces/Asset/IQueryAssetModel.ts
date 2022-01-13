export default interface IQueryAssetModel {
    // page: number;
    // type: string[];
    // search: string;
    // orderBy: string;
    // orderByColumn: string;
    search: string;
    sortOrder: string;
    sortColumn: string;
    limit: number;
    page: number;
    states: string[];
    categories: string[];
    location: string;
    onTopAssetCode: string;
}
