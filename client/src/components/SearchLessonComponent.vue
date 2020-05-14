<template>
  <el-input placeholder="Search by lesson name" v-model="input" class="i-input">
    <el-select
      class="i-select"
      v-model="select"
      slot="prepend"
      :loading="loading"
      placeholder="Type (Optional)"
      no-data-text="No data!"
      :clearable="true"
    >
      <el-option-group
      v-for="group in groups"
      :key="group.id"
      :label="group.label">
      <el-option
        v-for="option in group.options"
        :key="option.id"
        :label="option.name"
        :value="option.id">
      </el-option>
    </el-option-group>
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
import { TypeService, NameGroupModel } from "../services/type.service";

@Component
class SearchLessonComponent extends Vue {
  private input = "";
  private select = "";
   private groups: NameGroupModel[] = [];
   private loading = true;

   public mounted() {
      TypeService.getTypes()
      .then((groups: NameGroupModel[]) => {
         this.groups = groups;
         this.loading = false;
      })
      .catch((error) => {
         console.log(error);
         this.loading = false;
      })
   }
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
