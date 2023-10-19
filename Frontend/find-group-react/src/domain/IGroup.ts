import { IBaseEntity } from "./IBaseEntity";

export interface IGroup extends IBaseEntity {
    name: string,
    description: string,
    maxParticipants: number,
    participants: number,
    maxAge?: number,
    minAge?: number,
    groupGender?: string
}
