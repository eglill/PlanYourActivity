import type { IJWTResponse } from "@/domain/IJWTResponse";
import { defineStore } from "pinia";
import { IdentityService } from "@/services/IdentityService";
import { type ILoginData } from "@/domain/ILoginData";
import type { IRegisterData } from "@/domain/IRegisterData";

export const useIdentityStore = defineStore({
  id: "identity",
  state: () => ({
    jwtResponse: null as IJWTResponse | null,
  }),
  getters: {
    isLoggedIn(): boolean {
      if (this.jwtResponse){
        return true;
      }
      return false; // Example getter to check if a user is logged in based on the presence of the jwt token
    },
    getJwtResponse() : IJWTResponse | null{
      return this.jwtResponse;
    },
  },
  actions: {
    async login(data: ILoginData): Promise<any> {
      const identityService = new IdentityService();
      // Example action to handle login logic and update the jwt token in the store
      try {
        const response = await identityService.login(data);
        if (response){
          this.jwtResponse = response; // Update the jwt token in the store state
        }
      } catch (error) {
        // Handle login error
      }
    },

    async register(data: IRegisterData): Promise<any> {
      const identityService = new IdentityService();
      // Example action to handle login logic and update the jwt token in the store
      try {
        const response = await identityService.register(data);
        if (response){
          this.jwtResponse = response; // Update the jwt token in the store state
        }
      } catch (error) {
        // Handle login error
      }
    },

    setJwtResponse(data : IJWTResponse | null){
      this.jwtResponse = data;
    }
  },
});
