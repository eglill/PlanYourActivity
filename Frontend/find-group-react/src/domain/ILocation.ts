import {IBaseEntity} from "./IBaseEntity";

export interface ILocation extends IBaseEntity {
    address: string,
    description: string,
    country: string
}