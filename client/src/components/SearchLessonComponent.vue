<template>
  <el-input placeholder="Ieškoti pagal pamokos pavadinimą" v-model="input" class="i-input">
    <el-select
      class="i-select"
      v-model="select"
      slot="prepend"
      :loading="loading"
      placeholder="Pamokos tipas"
      no-data-text="Nėra duomenų!"
      :clearable="true"
    >
      <el-option-group
        v-for="group in groups"
        :key="group.id"
        :label="group.label"
      >
        <el-option
          v-for="option in group.options"
          :key="option.id"
          :label="option.name"
          :value="option.id"
        >
        </el-option>
      </el-option-group>
    </el-select>
    <el-button
      class="i-button"
      :plain="true"
      slot="append"
      type="warning"
      icon="el-icon-search"
      >Ieškoti</el-button
    >
  </el-input>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { AdminService, NameGroupModel } from "../services/admin.service";

@Component
class SearchLessonComponent extends Vue {
  private input = "";
  private select = "";
  private groups: NameGroupModel[] = [];
  private loading = true;

  public created() {
    AdminService.getTypes()
      .then((groups: NameGroupModel[]) => {
        this.groups = groups;
        this.loading = false;
      })
      .catch(error => {
        this.loading = false;
      });
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
    background: #e6a23c;
    border-color: #e6a23c;
    color: #fff;
  }
}

.el-input-group__append > .i-button {
  width: 100px;
}
</style>
