<script lang="ts">
import { defineComponent } from "vue";
import GroupCards from "../../components/GroupCards.vue";
import GroupNav from "../../components/GroupNav.vue";
import { GroupService } from "../../services/GroupService";
import { useUserGroupStore } from "../../stores/userGroups";

export default defineComponent({
  components: { GroupCards, GroupNav },
  name: "UserGroups",
  setup() {
    const groupStore = useUserGroupStore();
    const groupService = new GroupService();
    const groupResponse = groupService.getJoinedGroups().then((g) => {
      groupStore.set(g);
    });
    return {
      groupStore,
    };
  },
});
</script>

<template>
  <div class="container">
    <group-nav></group-nav>
    <div class="card-deck mt-4">
      <div
        className="row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4"
      >
        <group-cards></group-cards>
      </div>
    </div>
  </div>
</template>
