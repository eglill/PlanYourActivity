export interface IAddGroup {
    name: string,
    description: string,
    maxParticipants: number,
    maxAge?: number,
    minAge?: number,
    private: boolean,
    joiningLocked: boolean,
    groupGenderId?: string
}