import { CategoryDto } from "./category.model";
export enum PostStatus {
    Draft = 0,
    Published = 1,
    Archived = 2
}

export interface PostDto {
    id: number;
    isPublic: boolean;
    title: string;
    slug: string;
    content: string;
    summary: string;
    createdBy: string;
    status: PostStatus;
    createdAt: Date;
    updatedAt: Date;
    publishedAt: Date;
    tags: any[];
    categories: CategoryDto[];
}

export interface PostPagedQuery {
    pageIndex: number;
    pageSize: number;
    postStatus?: PostStatus;
    isDeleted?: boolean;
    search?: string;
    categoryId?: number;
}

