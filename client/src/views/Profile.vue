<template>
  <el-container
    v-if="loading"
    style="
      align-items: center;
      justify-content: center;
      height: 100%;
    "
  >
    <el-button :loading="loading" type="info" :circle="true"></el-button>
  </el-container>
  <div v-else-if="!session">
    <el-alert
      :title="language.LogInToView"
      type="error"
      :show-icon="true"
      :closable="false"
    >
    </el-alert>
  </div>
  <div v-else>
    <el-row>
      <el-col :offset="6" :span="12" style="padding-right: 10px;">
        <h2 style="text-align:center;">{{ profile.name }}</h2>
        <p style="text-align:center;">{{ profileRole }}</p>
        <p style="text-align:center;">{{ language.Level }}</p>
        <p style="text-align:center;">{{ profile.level }}</p>
        <el-progress
          :text-inside="true"
          :stroke-width="22"
          :percentage="
            Math.ceil((profile.experience / profile.nextExperience) * 100)
          "
          status="warning"
        ></el-progress>
        <p style="text-align:center;">{{ language.Points }}</p>
        <p style="text-align:center;">
          {{ profile.experience }} / {{ profile.nextExperience }}
        </p>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <p style="text-align:center;">{{ language.ActiveBadges }}</p>
        <div class="container-center">
          <el-card
            v-for="lesson in profile.lessons.filter(
              item => item.progress == 'Active'
            )"
            :key="lesson.id"
            shadow="hover"
            style="display: inline-block;"
          >
            <div @click="() => goToLesson(lesson.id)">
              <el-tooltip :content="lesson.name">
                <el-image
                  :src="lesson.badgeBase64"
                  alt="Badge"
                  style="width: 100px; height: 100px;"
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
              font-size: 30px;"
                  >
                    <i class="el-icon-picture-outline"></i>
                  </div>
                </el-image>
              </el-tooltip>
            </div>
          </el-card>
        </div>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <p style="text-align:center;">{{ language.CompletedBadges }}</p>
        <div class="container-center">
          <el-card
            v-for="lesson in profile.lessons.filter(
              item => item.progress == 'Completed'
            )"
            :key="lesson.id"
            shadow="hover"
            style="display: inline-block;"
          >
            <div @click="() => goToLesson(lesson.id)">
              <el-tooltip :content="lesson.name">
                <el-image
                  :src="lesson.badgeBase64"
                  alt="Badge"
                  style="width: 100px; height: 100px;"
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
              font-size: 30px;"
                  >
                    <i class="el-icon-picture-outline"></i>
                  </div>
                </el-image>
              </el-tooltip>
            </div>
          </el-card>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapActions, mapGetters, ActionMethod } from "vuex";
import { ProfileModel, SessionModel } from "../models/stores/user.store.model";
import { LanguageModel } from "../assets/i18n/language";

@Component({
  methods: {
    ...mapActions("user", {
      getProfile: "getProfile"
    })
  },
  computed: {
    ...mapGetters("user", {
      profile: "profile",
      session: "session"
    }),
    ...mapGetters("language", {
      language: "getTranslations"
    })
  }
})
class Profile extends Vue {
  private getProfile!: ActionMethod;
  private profile!: ProfileModel;
  private session!: SessionModel;
  private loading = true;
  private language!: LanguageModel;

  public created() {
    this.onProfile();

    this.$watch("$route", () => this.onProfile());
  }

  private onProfile() {
    this.getProfile({ id: Number(this.$route.params.id) })
      .then(() => {
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private goToLesson(lessonId: string) {
    this.$router.replace({ name: "Lesson", params: { id: lessonId } });
  }

  get profileRole() {
    return `${this.language[this.profile.role]}`;
  }
}

export default Profile;
</script>

<style lang="scss">
.container-center {
  display: flex;
  justify-content: center;
}
</style>
