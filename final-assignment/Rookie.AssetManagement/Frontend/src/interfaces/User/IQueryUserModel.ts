export default interface IQueryUserModel {
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
    types: string[];
    location: string;
    onTopStaffCode: number;
}
