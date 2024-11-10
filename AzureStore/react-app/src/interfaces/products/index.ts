export interface IProductItem {
    id: number;
    name: string,
    price: number,
    images: string[],
    categoryName: string,
    categoryId: number,
}

export interface IProductCreate {
    name: string,
    price: number,
    categoryId: number,
    images: File[]|null,
}

export interface IProductEdit {
    id: number;
    name: string;
    price: number;
    categoryId: number;
    newImages?: File[];
    removeImages?: string[];
}

export interface IUploadedFile {
    id: number;
    image: string;
    priority: number;
    preview: any;
    url: any;
    originFileObj: File;
    size: number;
    type: string;
    uid: string;
}