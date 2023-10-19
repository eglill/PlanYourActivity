import {ILocationAdd} from "./ILocationAdd";

export interface IGroupEvent {
    timeFrom: string,
    TimeTo: string,
    countryId: string,
    activityId: string,
}

export interface IAddUserEvent {
    name: string,
    startsAt: string,
    endsAt: string,
    colourId: string,
    location?: ILocationAdd,
    activityId?: string,
    availableForGroupEvent: boolean
}

export interface IAddGroupEvent {
    name: string,
    startsAt: string,
    endsAt: string,
    colourId: string,
    location: ILocationAdd,
    activityId: string,
    groupId: string,
}
