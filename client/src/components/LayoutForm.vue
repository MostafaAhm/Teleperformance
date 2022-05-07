<template>
  <button v-on:click="logout">logout</button>
</template>
<script>
export default {
  data() {
    return {};
  },
  created() {
    document.title = this.$props.title;
  },
  props: {
    title: String,
  },
  method: {
    logout() {
      window.localStorage.clear();
      this.$router.push({ name: "Login" });
    },
  },
};
</script>
