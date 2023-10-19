<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "GroupNav",
  setup() {},
});
</script>

<template>
  <div
    class="btn-group p-3 w-50"
    role="group"
    aria-label="Button group with nested dropdown"
  >
            <RouterLink to="/user/groups" class="btn btn-sm btn-warning">User groups</RouterLink>
            <RouterLink to="/user/add-group" class="btn btn-sm btn-warning">Create new</RouterLink>
  </div>
</template>
