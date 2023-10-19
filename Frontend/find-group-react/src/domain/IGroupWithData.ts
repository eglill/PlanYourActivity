import { IBaseEntity } from "./IBaseEntity";

export interface IGroupWithData extends IBaseEntity {
    name: string,
    description: string,
    maxParticipants: number,
    participants: number,
    maxAge?: number,
    minAge?: number,
    private: boolean,
    joiningLocked: boolean,
    groupGender?: string,
    admin: string
}
