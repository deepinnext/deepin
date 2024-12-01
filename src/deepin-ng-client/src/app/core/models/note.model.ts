import { CategoryDto } from "./category.model";
import { PagedQuery } from "./pagination.model";
import { TagDto } from "./tag.model";

export type NoteStatus = 'draft' | 'published' | 'archived';

export interface NoteQuery extends PagedQuery {
    categoryId?: number;
    status?: NoteStatus;
    isDeleted?: boolean;
}

export interface NoteDto {
    id: number;
    title: string;
    content: string;
    categoryId: number;
    description: string;
    status: NoteStatus;
    isDeleted: boolean;
    isPublic: boolean;
    createdAt: Date;
    updatedAt: Date;
    deletedAt: Date;
    publishedAt: Date;
    tags: TagDto[];
    category: CategoryDto;
}
export interface CreateNoteCommand {
    categoryId: number;
    title: string;
    content: string;
    description: string;
    isPublished: boolean;
    isPublic: boolean;
    tags: string[];
    coverImageId: string;
}