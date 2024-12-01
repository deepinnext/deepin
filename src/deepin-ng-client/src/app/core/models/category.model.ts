export interface CategoryDto {
    id: number;
    parentId: number;
    name: string;
    icon: string;
    description: string;
    displayOrder: number;
    isBuiltIn: boolean;
    createdBy: string;
    createdAt: Date;
    updatedAt: Date;
}