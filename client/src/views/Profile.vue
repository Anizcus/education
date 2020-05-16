<template>
  <div v-if="loading">
    <el-button :loading="loading" type="info" :circle="true"></el-button>
  </div>
  <div v-else-if="!session">
    <el-alert
      title="You must be logged in to view other users!"
      type="error"
      :show-icon="true"
      :closable="false"
    >
    </el-alert>
  </div>
  <div v-else>
    <el-row>
      <el-col :span="12" style="padding-right: 10px;">
        <p>Profile page of {{ profile.name }}</p>
        <p>Role - {{ profile.role }}</p>
        <p>Level: {{ profile.level }}</p>
        <el-progress
          :text-inside="true"
          :stroke-width="22"
          :percentage="
            Math.ceil((profile.experience / profile.nextExperience) * 100)
          "
          status="warning"
        ></el-progress>
        <p>
          Experience: {{ profile.experience }} / {{ profile.nextExperience }}
        </p>
      </el-col>
      <el-col :span="12">
        <p>Badges (Completed)</p>
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
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <p>Badges (Active)</p>
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
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapActions, mapGetters, ActionMethod } from "vuex";
import { ProfileModel, SessionModel } from "../models/stores/user.store.model";

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
    })
  }
})
class Profile extends Vue {
  private getProfile!: ActionMethod;
  private profile!: ProfileModel;
  private session!: SessionModel;
  private loading = true;

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
}

export default Profile;
</script>
