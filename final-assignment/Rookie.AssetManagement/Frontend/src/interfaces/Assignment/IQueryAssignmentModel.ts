export default interface IQueryAssignmentModel {
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
    assignedDate: Date;
    onTopAssignmentNumber: number;
}
