<script lang="ts">
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import { defineComponent } from "vue";

export default defineComponent({
  name: 'App',
  components: {
    Header,
    Footer,
  },
});

</script>
<template>
  <Header></Header>
  <div class="container">
    <main role="main" class="pb-3">
      <RouterView></RouterView>
    </main>
  </div>
</template>

<style scoped></style>