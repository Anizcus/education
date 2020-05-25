<template>
  <el-input
    :placeholder="language.SearchLessonByTitle"
    v-model="input"
    class="i-input"
  >
    <el-select
      class="i-select"
      v-model="select"
      slot="prepend"
      :loading="loading"
      :placeholder="language.LessonType"
      :no-data-text="language.NoData"
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
      @click="onSearch"
      >{{ language.Search }}</el-button
    >
  </el-input>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { AdminService, NameGroupModel } from "../services/admin.service";
import { LanguageModel } from "../assets/i18n/language";
import { mapGetters, mapActions, ActionMethod } from "vuex";

@Component({
  computed: {
    ...mapGetters("language", {
      language: "getTranslations"
    })
  },
  methods: {
    ...mapActions("search", {
      searchLesson: "getLessons"
    })
  }
})
class SearchLessonComponent extends Vue {
  private language!: LanguageModel;
  private input = "";
  private select = "";
  private groups: NameGroupModel[] = [];
  private searchLesson!: ActionMethod;
  private loading = true;

  public created() {
    AdminService.getTypes()
      .then((groups: NameGroupModel[]) => {
        this.groups = groups;
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private onSearch() {
    this.searchLesson({
      typeId: this.select || 0,
      name: this.input
    }).then(() =>
      this.$router.push({
        name: "Search"
      })
    );
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
