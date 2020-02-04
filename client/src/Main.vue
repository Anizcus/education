<template>
  <div>
    <div>
      <router-link to="/">Home</router-link>
      <router-link to="/about">About</router-link>
    </div>
    <router-view />
    <a-button type="dashed">aaa</a-button>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

const Main = Vue.extend({
  data: function() {
    return {};
  }
});

export default Main;
</script>
