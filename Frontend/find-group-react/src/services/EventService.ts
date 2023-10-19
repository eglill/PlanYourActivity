import {AxiosInstance} from "axios";
import {IGroupEvent, IUserEvent} from "../domain/IEvent";
import {IAddGroupEvent, IAddUserEvent} from "../dto/IEvent";

export class EventService {
    private eventUrl = 'v1/Event/';
    private axios: AxiosInstance;

    constructor(private axiosInstance: AxiosInstance) {
        this.axios = axiosInstance;
    }

    async getAllPublicEvents(): Promise<IGroupEvent[]> {
        try {
            const response = await this.axios.get<IGroupEvent[]>(this.eventUrl);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }

    async getUserEvent(eventId: string): Promise<IUserEvent | undefined> {
        try {
            const response = await this.axios.get<IUserEvent>(this.eventUrl + "user/" + eventId);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return undefined;
    }

    async getGroupEvent(eventId: string): Promise<IGroupEvent | undefined> {
        try {
            const response = await this.axios.get<IGroupEvent>(this.eventUrl + "group/" + eventId);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return undefined;
    }

    async getGroupEvents(groupId: string): Promise<IGroupEvent[]> {
        try {
            const response = await this.axios.get<IGroupEvent[]>(this.eventUrl + "group/all/" + groupId);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }

    async getUserEvents(): Promise<IGroupEvent[]> {
        try {
            const response = await this.axios.get<IGroupEvent[]>(this.eventUrl + "user/");
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }
    async AddUserEvent(userEvent: IAddUserEvent): Promise<string | undefined> {
        try {
            const response = await this.axios.post<IUserEvent>(this.eventUrl + "user/", userEvent);
            if (response.status === 201) {
                return response.data.id;
            }
        } catch (e) {
            console.log(e);
        }
        return undefined;
    }

    async AddGroupEventToEvents(eventId: string): Promise<boolean> {
        try {
            const response = await this.axios.post(this.eventUrl + "user/" + eventId);
            if (response.status === 200) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }

    async AddGroupEvent(groupEvent: IAddGroupEvent): Promise<string | undefined> {
        try {
            const response = await this.axios.post<IGroupEvent>(this.eventUrl + "group/", groupEvent);
            if (response.status === 201) {
                return response.data.id;
            }
        } catch (e) {
            console.log(e);
        }
        return undefined;
    }

    async updateUserEvent(userEvent: IAddUserEvent, eventId: string): Promise<boolean> {
        try {
            const response = await this.axios.put(this.eventUrl + "user/" + eventId, userEvent);
            if (response.status === 204) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }

    async updateGroupEvent(groupEvent: IAddGroupEvent, eventId: string): Promise<boolean> {
        try {
            const response = await this.axios.put(this.eventUrl + "group/" + eventId, groupEvent);
            if (response.status === 204) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }

    async deleteUserEvent(eventId: string): Promise<boolean> {
        try {
            const response = await this.axios.delete(this.eventUrl + "user/" + eventId);
            if (response.status === 204) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }

    async deleteGroupEvent(eventId: string): Promise<boolean> {
        try {
            const response = await this.axios.delete(this.eventUrl + "group/" + eventId);
            if (response.status === 204) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }
}