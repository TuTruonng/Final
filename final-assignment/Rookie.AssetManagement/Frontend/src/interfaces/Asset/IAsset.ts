export default interface IAsset {
    id: number;
    assetName: string;
    assetCode: string;
    category: string;
    state: string;
    installedDate: Date;
    location: string;
    specification: string;
    history: Date;
}
