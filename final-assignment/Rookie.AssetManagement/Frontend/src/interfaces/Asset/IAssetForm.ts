export default interface IAssetForm {
    assetCode?: string;
    assetName: string;
    state?: string;
    installedDate?: Date;
    category?: string;
    specification: string;
    location: string;
    history?: string;
}
