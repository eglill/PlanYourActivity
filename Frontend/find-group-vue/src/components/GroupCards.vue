<script lang="ts">
import { defineComponent } from "vue";
import { useUserGroupStore } from "../stores/userGroups";

export default defineComponent({
  name: "GroupCards",
  setup() {
    const groupStore = useUserGroupStore();

    return {
      groupStore,
    };
  },
});
</script>

<template>
  <div
    class="card text-white bg-secondary mb-3 mx-auto"
    v-for="group of groupStore.userGroups"
    :key="group.id"
    :style="{width: '17rem'}"
  >
    <div class="card-body">
      <h5 class="card-title">{{ group.name }}</h5>
      <p class="card-text">{{ group.description }}</p>
    </div>
    <ul class="list-group list-group-flush">
      <li class="list-group-item text-white bg-secondary">
        Participants: {{ group.participants }} out of
        {{ group.maxParticipants }}
      </li>
      <li class="list-group-item text-white bg-secondary">
        Age range: {{ group.minAge }} - {{ group.maxAge }}
      </li>
      <li class="list-group-item text-white bg-secondary">
        Gender: {{ group.gender }}
      </li>
    </ul>
  </div>
</template>
