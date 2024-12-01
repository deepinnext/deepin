export interface PagedQuery {
    pageIndex: number;
    pageSize: number;
    search?: string;
}

export class PagedResult<T>{
    pageSize = 0;
    pageIndex = 0;
    totalPages = 0;
    count = 0;
    totalCount = 0;
    items: Array<T> = [];
}