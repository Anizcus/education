<template>
  <el-alert
    v-if="
      !session || (session.role != 'Teacher' && session.role != 'Administrator')
    "
    :title="language.LogInAsTeacherOrAdmin"
    type="error"
    :show-icon="true"
    :closable="false"
  >
  </el-alert>
  <div v-else style="height: 100%;">
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
    <el-table v-else :data="lessons" :empty-text="language.NoData">
      <el-table-column type="expand">
        <template slot-scope="scope">
          <el-row>
            <el-col :span="5">
              <img
                :src="scope.row.badgeBase64"
                alt="Badge"
                wdith="100"
                height="100"
              />
            </el-col>
            <el-col :span="19">
              <el-row style="text-align: center;">
                <el-col :span="8" v-if="session.role === 'Administrator'">
                  <span class="cell cell-custom">{{ language.Author }}</span>
                </el-col>
                <el-col :span="8" v-if="session.role === 'Teacher'">
                  <span class="cell cell-custom">{{
                    language.Description
                  }}</span>
                </el-col>
                <el-col :span="8"
                  ><span class="cell cell-custom">{{
                    language.Reason
                  }}</span></el-col
                >
                <el-col :span="8"
                  ><span class="cell cell-custom">{{
                    language.ModifyDate
                  }}</span></el-col
                >
              </el-row>
              <el-row style="text-align: center;">
                <el-col v-if="session.role === 'Administrator'" :span="8">
                  <el-button
                    type="text"
                    @click="() => onProfile(scope.row.ownerId)"
                  >
                    {{ scope.row.ownerName }}
                  </el-button>
                </el-col>
                <el-col v-if="session.role === 'Teacher'" :span="8">
                  {{ scope.row.description || "-" }}
                </el-col>
                <el-col :span="8">{{ scope.row.status || "-" }}</el-col>
                <el-col :span="8">
                  {{ localTime(scope.row.modified) }}
                </el-col>
              </el-row>
            </el-col>
          </el-row>
        </template>
      </el-table-column>
      <el-table-column prop="name" :label="language.Title">
        <template slot-scope="scope">
          <el-button type="text" @click="() => onLesson(scope.row.id)">
            {{ scope.row.name }}
          </el-button>
        </template>
      </el-table-column>
      <el-table-column
        prop="category"
        :label="language.Category"
      ></el-table-column>
      <el-table-column
        prop="type"
        :label="language.LessonType"
      ></el-table-column>
      <el-table-column prop="state" :label="language.State">
        <template slot-scope="scope">
          {{ mapState(scope.row.state) }}
        </template>
      </el-table-column>
      <el-table-column align="right" width="200">
        <template
          v-if="session.role === 'Teacher'"
          slot="header"
          slot-scope="scope"
        >
          <el-button
            v-if="session.role === 'Teacher'"
            size="mini"
            type="primary"
            @click="() => onCreateLesson(scope)"
            >{{ language.AddLesson }}</el-button
          >
        </template>
        <template slot-scope="scope">
          <el-button
            v-if="
              session.role === 'Administrator' && scope.row.state === 'Waiting'
            "
            size="mini"
            type="success"
            @click="() => onApprove(scope.row.id)"
            >{{ language.Approve }}</el-button
          >
          <el-button
            v-if="
              session.role === 'Administrator' && scope.row.state === 'Waiting'
            "
            size="mini"
            type="danger"
            @click="() => onReject(scope.row.id)"
            >{{ language.Reject }}</el-button
          >
          <el-button
            v-if="session.role === 'Teacher' && scope.row.state === 'Created'"
            size="mini"
            type="danger"
            @click="() => onUpdateLesson(scope.row)"
            >{{ language.Edit }}</el-button
          >
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { LessonListModel } from "../models/stores/lesson.store.model";
import { SessionModel } from "../models/stores/user.store.model";
import { mapGetters, mapActions, ActionMethod } from "vuex";
import { LessonService } from "../services/lesson.service";
import { LanguageModel } from "../assets/i18n/language";

@Component({
  computed: {
    ...mapGetters("user", {
      session: "session",
    }),
    ...mapGetters("language", {
      language: "getTranslations",
    }),
  },
  methods: {
    ...mapActions("lesson", {
      postStatus: "postLessonStatus",
    }),
    ...mapActions("modal", {
      setLessonModalVisible: "setLessonModalVisible",
      setAuthorizeModalVisible: "setAuthorizeModalVisible",
    }),
    ...mapActions("language", {
      localTime: "getLocalTime",
    }),
  },
})
class Management extends Vue {
  private session!: SessionModel;
  private language!: LanguageModel;
  private loading = true;
  private lessons: LessonListModel[] = [];
  private setLessonModalVisible!: ActionMethod;
  private setAuthorizeModalVisible!: ActionMethod;
  private postStatus!: ActionMethod;
  private localTime: ActionMethod;

  private onCreateLesson() {
    this.setLessonModalVisible({
      visible: true,
      data: {
        labelTitle: this.language.CreateLesson,
        labelBack: this.language.Back,
        labelAction: this.language.Create,
        onSuccess: () => {
          this.initialize();
        },
      },
      stateName: "Create",
    });
  }

  private onUpdateLesson(row: LessonListModel) {
    this.setLessonModalVisible({
      visible: true,
      data: {
        labelTitle: this.language.UpdateLesson,
        labelBack: this.language.Back,
        labelAction: this.language.Update,
        name: row.name,
        description: row.description,
        type: row.type,
        id: row.id,
        image: row.badgeBase64,
        onSuccess: () => {
          this.initialize();
        },
      },
      stateName: "Update",
    });
  }

  public created() {
    this.initialize();
  }

  private initialize() {
    if (this.session) {
      if (this.session.role === "Administrator") {
        LessonService.getLessonListForAdmin().then((lessons) => {
          this.lessons = lessons;
          this.loading = false;
        });
      }

      if (this.session.role === "Teacher") {
        LessonService.getLessonListForTeacher().then((lessons) => {
          this.lessons = lessons;
          this.loading = false;
        });
      }
    } else {
      this.loading = false;
    }
  }

  private onProfile(id: string) {
    if (
      this.$route.name !== "Profile" ||
      (this.$route.name === "Profile" && this.$route.params.id !== id)
    ) {
      this.$router.push({ name: "Profile", params: { id } });
    }
  }

  private onLesson(id: string) {
    if (
      this.$route.name !== "Lesson" ||
      (this.$route.name === "Lesson" && this.$route.params.id !== id)
    ) {
      this.$router.push({ name: "Lesson", params: { id } });
    }
  }

  private onReject(id: string) {
    this.setAuthorizeModalVisible({
      visible: true,
      stateName: "Reject",
      data: {
        lessonId: id,
        labelAction: this.language.Reject,
        labelBack: this.language.Back,
        labelTitle: this.language.RejectLesson,
        labelReasonOptional: this.language.ReasonOptional,
        onAction: (id: number, status: string, valid: boolean) =>
          this.postStatus({
            lessonId: id,
            status: status,
            isValid: valid,
          }).then(() => this.initialize()),
      },
    });
  }

  private onApprove(id: string) {
    this.setAuthorizeModalVisible({
      visible: true,
      stateName: "Approve",
      data: {
        lessonId: id,
        labelAction: this.language.Approve,
        labelBack: this.language.Back,
        labelTitle: this.language.ApproveLesson,
        labelReasonOptional: this.language.ReasonOptional,
        onAction: (id: number, status: string, valid: boolean) =>
          this.postStatus({
            lessonId: id,
            status: status,
            isValid: valid,
          }).then(() => this.initialize()),
      },
    });
  }

  private mapState(state: string) {
    return this.language[state];
  }

  private async onLocalTime(date: string) {
    let localDate = "";

    await this.localTime(date).then(
      (response: string) => localDate = response);

    return localDate;
  }
}

export default Management;
</script>

<style lang="scss">
.cell-custom {
  color: #909399;
  font-weight: bold;
  font-size: 14px;
}
</style>
