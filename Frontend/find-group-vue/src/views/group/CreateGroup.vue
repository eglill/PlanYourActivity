<script lang="ts">
import { defineComponent, ref } from "vue";
import GroupNav from "../../components/GroupNav.vue";
import { IAddGroup } from "../../domain/IAddGroup";
import { GenderService } from "../../services/GenderService";
import { GroupService } from "../../services/GroupService";
import { useGenderStore } from "../../stores/gender";
import router from "../../router";

export default defineComponent({
  components: { GroupNav },
  name: "CreateGroup",
  setup() {
    const genderStore = useGenderStore();
    const genderService = new GenderService();
    const groupService = new GroupService();

    const genders = genderService.getAll().then((response) => {
      if (response) {
        genderStore.set(response);
      } else {
        genderStore.set([]);
      }
    });

    const name = ref("");
    const description = ref("");
    const maxParticipants = ref(1);
    const maxAge = ref(undefined);
    const minAge = ref(undefined);
    const groupGenderId = ref("");
    const privateGroup = ref(false);
    const joiningLocked = ref(false);

    const createClicked = () => {
      const data: IAddGroup = {
        name: name.value,
        description: description.value,
        maxParticipants: maxParticipants.value,
        maxAge: maxAge.value,
        minAge: minAge.value,
        groupGenderId: groupGenderId.value,
        private: privateGroup.value,
        joiningLocked: joiningLocked.value,
      };

      groupService.addGroup(data).then(() => {
        router.push("../user/groups/");
      });
    };

    return {
      name,
      description,
      maxParticipants,
      maxAge,
      minAge,
      groupGenderId,
      privateGroup,
      joiningLocked,
      genderStore,
      createClicked
    };
  },
});
</script>

<template>
  <div class="container">
    <group-nav></group-nav>

    <form @submit.prevent="createClicked">
      <div class="row">
        <div class="col-md-12">
          <div class="form-group">
            <label class="control-label" for="name">Name</label>
            <input v-model="name" class="form-control" type="text" />
          </div>
          <div class="form-group mt-2">
            <label class="control-label" for="description">Description</label>
            <input v-model="description" class="form-control" type="text" />
          </div>
          <div class="form-group mt-2">
            <label class="control-label" for="maxParticipants"
              >Maximum participants</label
            >
            <input
              v-model="maxParticipants"
              class="form-control"
              type="number"
            />
          </div>
          <div class="form-group mt-2">
            <label class="control-label" for="minAge">Minimum age</label>
            <input v-model="minAge" class="form-control" type="number" />
          </div>
          <div class="form-group mt-2">
            <label class="control-label" for="maxAge">Maximum age</label>
            <input v-model="maxAge" class="form-control" type="number" />
          </div>
          <div class="form-group mt-2">
            <label class="control-label" for="groupGenderId">Gender</label>
            <select v-model="groupGenderId" class="form-select" aria-label="Gender">
              <option
                v-for="gender of genderStore.genders"
                :value="gender.id"
                :key="gender.id"
              >
                {{ gender.name }}
              </option>
            </select>
          </div>
          <div class="form-check mt-2">
            <label class="form-check-label" for="privateGroup"
              >Private group</label
            >
            <input
              v-model="privateGroup"
              type="checkbox"
              class="form-check-input"
            />
          </div>
          <div class="form-check mt-2">
            <label class="form-check-label" for="joiningLocked"
              >Joining to group is locked</label
            >
            <input
              v-model="joiningLocked"
              type="checkbox"
              class="form-check-input"
            />
          </div>
          <div class="form-group mt-3">
            <input type="submit" value="Create" class="btn btn-primary" />
          </div>
        </div>
      </div>
    </form>
  </div>
</template>
