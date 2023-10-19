<script lang="ts">
import { defineComponent, ref } from "vue";
import { IdentityService } from "../../services/IdentityService";
import { useIdentityStore } from "../../stores/identity";
import { useGenderStore } from "../../stores/gender";
import { IRegisterData } from "../../domain/IRegisterData";
import { RouterLink, routeLocationKey } from "vue-router";
import { GenderService } from "../../services/GenderService";
import router from "../../router";

export default defineComponent({
  name: "Register",
  setup() {
    const identityStore = useIdentityStore();
    const genderStore = useGenderStore();
    const genderService = new GenderService();
    const email = ref("");
    const password = ref("");
    const confirmPassword = ref("");
    const firstName = ref("");
    const lastName = ref("");
    const gender = ref("");
    const birthDate = ref("");
    const genders = genderService.getAll().then((response) => {
      if (response) {
        genderStore.set(response);
      } else {
        genderStore.set([]);
      }
    });

    const registerClicked = () => {
      const data: IRegisterData = {
        email: email.value,
        password: password.value,
        confirmPassword: confirmPassword.value,
        firstName: firstName.value,
        lastName: lastName.value,
        gender: gender.value,
        birthDate: birthDate.value,
      };

      identityStore.register(data).then(() => {
        router.push("/");
      });
    };

    return {
      email,
      password,
      confirmPassword,
      firstName,
      lastName,
      gender,
      birthDate,
      genderStore,
      registerClicked,
    };
  },
});
</script>

<template>
  <h1>Register</h1>

  <form @submit.prevent="registerClicked">
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
          <div class="form-group mt-2">
            <label class="control-label" for="confirmPassword"
              >Confirm Password</label
            >
            <input
              v-model="confirmPassword"
              class="form-control"
              type="password"
            />
          </div>
          <div class="form-group mt-2">
            <label class="control-label" for="firstName">First Name</label>
            <input v-model="firstName" class="form-control" type="text" />
          </div>
          <div class="form-group mt-2">
            <label class="control-label" for="lastName">Last Name</label>
            <input v-model="lastName" class="form-control" type="text" />
          </div>
          <div class="form-group mt-2">
                        <label class="control-label" for="gender">Gender</label>

            <select v-model="gender" class="form-select" aria-label="Gender">
              <option
                v-for="gender of genderStore.genders"
                :value="gender.id"
                :key="gender.id"
              >
                {{ gender.name }}
              </option>
            </select>
          </div>
          <div class="form-group mt-2">
            <label class="control-label" for="birthDate">Birth Date</label>
            <input v-model="birthDate" class="form-control" type="date" />
          </div>
          <div class="form-group mt-3">
            <input type="submit" value="Register" class="btn btn-primary" />
          </div>
        </div>
      </div>
    </div>
  </form>
</template>
