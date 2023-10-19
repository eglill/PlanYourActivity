<script lang="ts">
import { useIdentityStore } from "../stores/identity";
import { RouterLink } from "vue-router";
import { defineComponent } from "vue";
import { IdentityService } from "../services/IdentityService";
import router from "../router";

export default defineComponent({
  class: "IdentityHeader",
  setup() {
    const identityStore = useIdentityStore();
    const identityService = new IdentityService();

    const onLogOut = () => {
      identityService.logout(identityStore.jwtResponse).then( () => {
        identityStore.setJwtResponse(null);
      router.push("/")
      });
    };

    return {
      identityStore,
      onLogOut,
    };
  },
});
</script>
<template>
  <template v-if="identityStore.$state.jwtResponse === null">
    <li class="nav-item">
      <RouterLink to="/register" class="nav-link text-dark">Register</RouterLink>
    </li>
    <li class="nav-item">
      <RouterLink to="/login" class="nav-link text-dark">Login</RouterLink>
    </li>
  </template>
  <template v-else>
    <li class="nav-item">
      <a class="btnnav-link text-dark" @click="onLogOut">Logout</a>
    </li>
  </template>
</template>
