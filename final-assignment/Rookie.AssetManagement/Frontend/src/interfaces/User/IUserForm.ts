export default interface IUserForm {
    userId?: number;
    firstName: string;
    lastName: string;
    fullName?: string,
    dateOfBirth?: Date;
    gender: string;
    joinedDate?: Date;
    type: string;
    location: string;
    staffCode?: number;
    username?: string;
}
