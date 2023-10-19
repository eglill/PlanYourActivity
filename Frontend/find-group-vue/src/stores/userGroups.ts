import type { IGroup } from "@/domain/IGroup";
import { defineStore } from "pinia";

export const useUserGroupStore = defineStore({
    id: "userGroups",
    state:() => ({
        userGroups: [] as IGroup[]
    }),
    getters: {
        get() {
            this.userGroups;
        }
    },
    actions: {
        set(userGroups: IGroup[]) {
            this.userGroups = userGroups;
        }
    }
})