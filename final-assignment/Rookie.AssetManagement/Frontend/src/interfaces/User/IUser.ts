export default interface IUser {
    id: number,
    username: string,
    staffCode: number,
    type: string;
    firstName: string,
    lastName: string,
    fullName: string,
    dateOfBirth: Date,
    location: string,
    gender: string,
    joinedDate: Date,
}