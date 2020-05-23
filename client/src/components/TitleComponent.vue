<template>
  <div>{{ title }}</div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component({
  props: {
    title: {
      type: String,
      required: true
    }
  }
})
class TitleComponent extends Vue {}

export default TitleComponent;
</script>
