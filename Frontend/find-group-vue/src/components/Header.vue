<script lang="ts">
import { useIdentityStore } from "../stores/identity";
import { RouterLink } from "vue-router";
import { defineComponent } from "vue";
import IdentityHeader from "./IdentityHeader.vue";

export default defineComponent({
  components: { IdentityHeader },
  class: "Header",
  setup() {
    const identityStore = useIdentityStore();

    return {
      identityStore,
    };
  },
});
</script>

<template>
  <header>
    <nav
      class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3"
    >
      <div class="container-fluid">
        <RouterLink class="navbar-brand" to="/">Activity Planner</RouterLink>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target=".navbar-collapse"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div
          class="navbar-collapse collapse d-sm-inline-flex justify-content-between"
        >
          <ul class="navbar-nav flex-grow-1">
            <li class="nav-item">
              <RouterLink to="/user/groups" class="nav-link text-dark"
                >Groups</RouterLink
              >
            </li>
            <li class="nav-item">
              <RouterLink to="/user/events" class="nav-link text-dark"
                >Events</RouterLink
              >
            </li>
          </ul>
          <ul class="navbar-nav">
            <identity-header></identity-header>
          </ul>
        </div>
      </div>
    </nav>
  </header>
</template>
