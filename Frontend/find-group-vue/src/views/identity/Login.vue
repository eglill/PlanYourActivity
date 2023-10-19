<script lang="ts">
import { defineComponent, ref } from "vue";
import { IdentityService } from "../../services/IdentityService";
import { useIdentityStore } from "../../stores/identity";
import { type ILoginData } from "../../domain/ILoginData";

import { RouterLink } from "vue-router";
import router from "../../router";

export default defineComponent({
  name: "Login",
  setup() {
    const identityStore = useIdentityStore();

    const email = ref("");
    const password = ref("");

    const loginClicked = () => {

      const data: ILoginData = {
        email: email.value,
        password: password.value,
      };

      identityStore.login(data).then(() =>{
        router.push("/")
      });
      
    };

    return {
      identityStore,
      email,
      password,
      loginClicked,
    };
  },
});
</script>




<template>
    <h1>Login</h1>
  
    <form @submit.prevent="loginClicked">
      <div class="row">
        <div class="col-md-12">
          <div>
            <div class="form-group">
              <label class="control-label" for="email">Email</label>
              <input v-model="email" class="form-control" type="text" />
            </div>
            <div class="form-group mt-2">
              <label class="control-label" for="password">Password</label>
              <input v-model="password" class="form-control" type="password" />
            </div>
            <div class="form-group mt-3">
              <input type="submit" value="Login" class="btn btn-primary" />
            </div>
          </div>
        </div>
      </div>
    </form>
  </template>
  
