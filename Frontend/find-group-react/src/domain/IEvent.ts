import {IBaseEntity} from "./IBaseEntity";
import {ILocation} from "./ILocation";

export interface IGroupEvent extends IBaseEntity {
    name: string,
    startsAt: string,
    endsAt: string,
    colour: string,
    location: ILocation,
    activity: string
}

export interface IUserEvent extends IBaseEntity {
    name: string,
    startsAt: string,
    endsAt: string,
    colour: string,
    location?: ILocation,
    activity?: string,
    availableForGroupEvent: boolean,
    editable: boolean
}
