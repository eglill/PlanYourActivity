import type { IGender } from "@/domain/IGender";
import { defineStore } from "pinia";

export const useGenderStore = defineStore({
    id: "genders",
    state: () => ({
        genders: [] as IGender[],
    }),
    getters:{

    },
    actions: {
        set(genders: IGender[]){
            this.genders = genders;
        }
    }
});