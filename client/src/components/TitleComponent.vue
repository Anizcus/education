<template>
  <div class="container">{{ title }}</div>
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

<style lang="scss" scoped>
.container {
  border-bottom: 1px dashed #e6e6e6;
  padding: 10px 0;
  font-size: 18px;
  margin-bottom: 14px;
}
</style>
