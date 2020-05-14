<template>
  <el-input placeholder="Search by lesson name" v-model="input" class="i-input">
    <el-select
      class="i-select"
      v-model="select"
      slot="prepend"
      placeholder="(Optional) Type"
    >
      <el-option label="Restaurant" :value="1"></el-option>
      <el-option label="Order No." :value="2"></el-option>
      <el-option label="Tel" :value="3"></el-option>
    </el-select>
    <el-button
      class="i-button"
      :plain="true"
      slot="append"
      type="warning"
      icon="el-icon-search"
    >Search</el-button>
  </el-input>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component
class SearchLessonComponent extends Vue {
  private input = "";
  private select = "";
}

export default SearchLessonComponent;
</script>

<style lang="scss">
.i-input {
   .el-select {
      width: 150px;
   }

  .el-input-group__prepend {
    background-color: #fff;
  }

  .el-input-group__append {
      background: #E6A23C;
      border-color: #E6A23C;
      color: #FFF;
      width: 75px;
   }
}

.el-input-group__append > .i-button {
  width: 75px;
}
</style>
