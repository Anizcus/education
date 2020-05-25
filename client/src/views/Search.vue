<template>
  <el-row>
    <i-title :title="language.SearchResults"></i-title>
    <el-col
      v-if="!lessons.length"
      style="border-bottom: 1px dashed #e6e6e6; margin-bottom: 14px; padding: 0 24px;"
    >
      <el-card shadow="hover" style="text-align: center; margin-bottom: 14px;">
        {{ language.NoData }}
      </el-card>
    </el-col>
    <el-col v-if="lessons.length" style="border-bottom: 1px dashed #e6e6e6; padding: 0 24px;">
      <template v-for="lesson in lessons">
        <el-card :key="lesson.id" shadow="hover" style="margin-bottom: 14px;">
          <el-row>
            <el-col :span="4">
              <el-image
                :src="lesson.badgeBase64"
                :alt="language.Badge"
                style="width: 100px; height: 100px"
              >
                <div
                  slot="error"
                  style="
                  display: flex;
                  justify-content: center;
                  align-items: center;
                  width: 100%;
                  height: 100%;
                  background: #f5f7fa;
                  color: #909399;
                  font-size: 30px;
                "
                >
                  <i class="el-icon-picture-outline"></i>
                </div>
              </el-image>
            </el-col>
            <el-col :span="1" style="height: 100px;">
              <div
                style="
                width: 1px;
                height: 100%;
                margin: 0 8px;
                vertical-align: middle;
                position: relative;
                background-color: #DCDFE6;
              "
              ></div>
            </el-col>
            <el-col :span="19">
              <router-link :to="`/lesson/${lesson.id}`">
                <el-link type="primary" :underline="false">
                  {{ language.Lesson }} “<i>{{ lesson.name }}</i
                  >”
                </el-link>
              </router-link>
              <el-divider>
                <i class="el-icon-star-on"></i>
              </el-divider>
              <span>
                {{ language.Author }} <b>{{ lesson.ownerName }}</b>
              </span>
              <span style="float: right;">
                {{ lesson.modified }}
              </span>
            </el-col>
          </el-row>
        </el-card>
      </template>
    </el-col>
  </el-row>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import LessonList from "./LessonList";
import { mapGetters } from "vuex";
import TitleComponent from "../components/TitleComponent.vue";
import { LanguageModel } from "../assets/i18n/language";

@Component({
  computed: {
    ...mapGetters("language", {
      language: "getTranslations",
    }),
    ...mapGetters("search", {
      lessons: "lessons",
    }),
  },
  components: {
    "i-title": TitleComponent,
  },
})
class Search extends Vue {
  private lessons: LessonList[];
  private language!: LanguageModel;
}

export default Search;
</script>
