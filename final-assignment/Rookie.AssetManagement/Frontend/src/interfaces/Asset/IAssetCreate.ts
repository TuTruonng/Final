export default interface IAssetCreate {
    assetCode?: string;
    assetName: string;
    stateId: string;
    installedDate?: Date;
    categoryId: number;
    specification: string;
    location: string;
    history?: string;
}
