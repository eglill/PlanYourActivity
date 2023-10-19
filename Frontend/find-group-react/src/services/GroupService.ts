import { IGroup } from "../domain/IGroup";
import { IGroupWithData } from "../domain/IGroupWithData";
import {IAddGroup} from "../dto/IAddGroup";
import {AxiosInstance} from "axios";
import {IAppUser} from "../domain/IAppUser";
export class GroupService {
    private groupUrl = 'v1/Group/';
    private axios: AxiosInstance;
    constructor(private axiosInstance: AxiosInstance){
        this.axios = axiosInstance;
    }

    async getAll(): Promise<IGroup[]> {
        try {
            const response = await this.axios.get<IGroup[]>(this.groupUrl);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }

    async addGroup(group: IAddGroup): Promise<string> {
        if (group.groupGenderId === "") {
            group.groupGenderId = undefined;
        }
        try {
            const response = await this.axios.post<IGroup>(this.groupUrl, group);
            if (response.status === 201) {
                return response.data.id!;
            }
        } catch (e) {
            console.log(e);
        }
        return "";
    }

    async getJoinedGroups(): Promise<IGroup[]> {
        try {
            const response = await this.axios.get<IGroup[]>(this.groupUrl + "user");
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }

    async getGroup(groupId: string): Promise<IGroupWithData | undefined> {
        try {
            const response = await this.axios.get<IGroupWithData>(this.groupUrl + groupId);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return undefined;
    }

    async getGroupUsers(groupId: string): Promise<IAppUser[]> {
        try {
            const response = await this.axios.get<IAppUser[]>(this.groupUrl + "users/" + groupId);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }

    async getIfUserIsAdmin(groupId: string): Promise<boolean> {
        try {
            const response = await this.axios.get<boolean>(this.groupUrl + "admin/" + groupId);
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }

    async getGroupInvites(): Promise<IGroup[]> {
        try {
            const response = await this.axios.get<IGroup[]>(this.groupUrl + "invite");
            if (response.status === 200) {
                return response.data;
            }
        } catch (e) {
            console.log(e);
        }
        return [];
    }

    async joinGroup(groupId: string): Promise<boolean> {
        try {
            const response = await this.axios.post(this.groupUrl + "join/" + groupId);
            if (response.status === 200) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }

    async leaveGroup(groupId: string): Promise<boolean> {
        try {
            const response = await this.axios.post(this.groupUrl + "leave/" + groupId);
            if (response.status === 200) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }
    async inviteToGroup(groupId: string, userId: string): Promise<boolean> {
        try {
            const response = await this.axios.post(this.groupUrl + "admin/invite/",
                {userId: userId, groupId: groupId});

            if (response.status === 200) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }


    async updateGroup(group: IAddGroup, groupId: string): Promise<boolean> {
        if (group.groupGenderId === "") {
            group.groupGenderId = undefined;
        }
        try {
            const response = await this.axios.put(this.groupUrl + "admin/" + groupId, group);
            if (response.status === 204) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }

    async kickUserFromGroup(userId: string, groupId: string): Promise<boolean> {
        try {
            const response = await this.axios.post(this.groupUrl + "admin/kick/",
                {userId: userId, groupId: groupId});

            if (response.status === 200) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }

        return false;
    }

    async deleteGroup(groupId: string): Promise<boolean> {
        try {
            const response = await this.axios.delete(this.groupUrl + "admin/" + groupId);
            if (response.status === 204) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    }
}
