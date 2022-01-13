export default interface IAccount {
    id: number;
    token?: string;
    userName: string;
    passw
    role: string;
    fullName: string;
    staffCode: string;
    location: string;
    isConfirmed?: boolean;
    changePassword: boolean;
}