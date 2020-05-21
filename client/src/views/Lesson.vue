<template>
  <el-card shadow="hover" style="margin-bottom: 14px;">
    <el-row>
      <el-col :span="4">
        <div class="container">
          <el-image
            :src="lesson.badgeBase64"
            alt="Badge"
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
                font-size: 30px;"
            >
              <i class="el-icon-picture-outline"></i>
            </div>
          </el-image>
          <el-divider> </el-divider>
          <div class="text-center">{{ lesson.category }}</div>
          <el-divider> </el-divider>
          <div class="text-center">{{ lesson.type }}</div>
        </div>
      </el-col>
      <el-col :span="1" style="height: 300px;">
        <div
          style="width: 1px; height: 100%; margin: 0 8px; vertical-align: middle; position: relative; background-color: #DCDFE6;"
        ></div>
      </el-col>
      <el-col :span="19">
        <el-row>
          <el-col>
            <el-row>
              <el-col :span="12">
                <p>Pamoka: {{ lesson.name }}</p>
                <div>
                  Būsena: {{ lessonState }}
                  {{ lessonProgress }}
                </div>
              </el-col>
              <el-col :span="12">
                <p style="float: right;">Autorius: {{ lesson.ownerName }}</p>
              </el-col>
            </el-row>
            <el-divider>
              <i class="el-icon-star-on"></i>
            </el-divider>
            <p>{{ lesson.description }}</p>
          </el-col>
        </el-row>
        <el-row>
          <el-col>
            <p>Užduotys {{ lesson.assignments.length }}</p>
            <el-collapse>
              <el-collapse-item
                v-for="(assignment, index) in lesson.assignments"
                :key="`question-${index}`"
                :title="`Klausimas ${index + 1} ${lessonProgress}`"
              >
                <el-row>
                  <el-col :span="20">
                    <h3>{{ assignment.description }}</h3>
                    <p>Taškai ({{ assignment.experience }})</p>
                  </el-col>
                  <el-col :span="4">
                    <el-button-group>
                      <el-button
                        v-if="lesson.state == 'Created'"
                        :plain="true"
                        type="warning"
                        icon="el-icon-edit"
                        :circle="true"
                        size="small"
                        @click="() => onAssignmentEdit(index, assignment)"
                      ></el-button>
                      <el-button
                        v-if="lesson.state == 'Created'"
                        :plain="true"
                        type="danger"
                        icon="el-icon-delete"
                        :circle="true"
                        size="small"
                        @click="() => onAssignmentDelete(index)"
                      ></el-button>
                    </el-button-group>
                    <el-button
                      v-if="
                        lesson.state == 'Published' &&
                          assignment.progress == 'Active'
                      "
                      :plain="true"
                      type="info"
                      icon="el-icon-edit-outline"
                      :circle="false"
                      size="small"
                      @click="() => onAnswerQuestion(assignment)"
                    ></el-button>
                  </el-col>
                </el-row>
              </el-collapse-item>
            </el-collapse>
            <p>
              <el-button
                v-if="
                  lesson.state == 'Created' &&
                    session &&
                    session.role === 'Teacher'
                "
                @click="() => onAssignmentCreate()"
                type="primary"
                :plain="true"
                style="float: left;"
                >Pridėti klausimą</el-button
              >
              <el-button
                v-if="
                  lesson.state == 'Waiting' &&
                    session &&
                    session.role === 'Administrator'
                "
                @click="() => Reject()"
                type="danger"
                :plain="true"
                style="float: left;"
                >Atšaukti</el-button
              >
            </p>
            <p>
              <el-button
                v-if="
                  lesson.state == 'Created' &&
                    session &&
                    session.role === 'Teacher'
                "
                @click="() => Publish()"
                type="success"
                :plain="true"
                style="float: right;"
                >Publikuoti</el-button
              >
              <el-button
                v-if="
                  lesson.state == 'Waiting' &&
                    session &&
                    session.role === 'Administrator'
                "
                @click="() => Approve()"
                type="success"
                :plain="true"
                style="float: right;"
                >Patvirtinti</el-button
              >
              <el-button
                v-if="
                  lesson.state == 'Published' &&
                    lesson.progress == null &&
                    session && session.role === 'Student'
                "
                @click="() => onStartLesson()"
                type="warning"
                :plain="true"
                style="float: right;"
                >Pasirinkti</el-button
              >
            </p>
          </el-col>
        </el-row>
      </el-col>
    </el-row>
  </el-card>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import {
  LessonModel,
  AssignmentModel
} from "../models/stores/lesson.store.model";
import { ActionMethod, mapGetters, mapActions } from "vuex";
import { SessionModel } from "../models/stores/user.store.model";

@Component({
  methods: {
    ...mapActions("lesson", {
      getLesson: "getLessonById",
      startLesson: "startLesson",
      postStatus: "postLessonStatus",
      postAssignments: "postLessonAssignments"
    }),
    ...mapActions("modal", {
      setAssignmentModalVisible: "setAssignmentModalVisible",
      setAuthorizeModalVisible: "setAuthorizeModalVisible",
      setAnswerModalVisible: "setAnswerModalVisible",
      setConfirmModalVisible: "setConfirmModalVisible"
    })
  },
  computed: {
    ...mapGetters("lesson", {
      lesson: "lesson"
    }),
    ...mapGetters("user", {
      session: "session"
    })
  }
})
class Lesson extends Vue {
  private getLesson!: ActionMethod;
  private setAssignmentModalVisible!: ActionMethod;
  private setAuthorizeModalVisible!: ActionMethod;
  private setAnswerModalVisible!: ActionMethod;
  private setConfirmModalVisible!: ActionMethod;
  private postStatus!: ActionMethod;
  private startLesson!: ActionMethod;
  private postAssignments!: ActionMethod;
  private lesson!: LessonModel;
  private session!: SessionModel;
  private loading = true;
  private dateOptions = {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hour12: false
  };

  private initialize() {
    this.getLesson({ id: Number(this.$route.params.id) })
      .then(() => {
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  public mounted() {
    this.initialize();
  }

  public onStartLesson() {
    this.setConfirmModalVisible({
      visible: true,
      stateName: "Pasirinkti",
      data: {
        title: "Patvirtinti pasirinkimą.",
        message: "Ar tikrai norite pasirinkti šią pamoką?",
        onAction: () => {
          return this.startLesson()
            .then(() => {
              this.initialize();
              return Promise.resolve();
            })
            .catch(() => Promise.reject());
        }
      }
    });
  }

  private onAssignmentCreate() {
    this.setAssignmentModalVisible({
      visible: true,
      stateName: "Sukurti"
    });
  }

  private onAssignmentDelete(index: number) {
    this.setAssignmentModalVisible({
      visible: true,
      stateName: "Ištrinti",
      data: {
        index
      }
    });
  }

  private onAssignmentEdit(index: number, assignment: AssignmentModel) {
    this.setAssignmentModalVisible({
      visible: true,
      stateName: "Atnaujinti",
      data: {
        index,
        ...assignment
      }
    });
  }

  private Publish() {
    this.setConfirmModalVisible({
      visible: true,
      stateName: "Publikuoti",
      data: {
        title: "Patvirtinti publikaciją",
        message: "Ar tikrai norite publikuoti pamoką?",
        onAction: () => {
          return this.postAssignments()
            .then(() => {
              this.initialize();
              return Promise.resolve();
            })
            .catch(() => Promise.reject());
        }
      }
    });
  }

  private Approve() {
    this.setAuthorizeModalVisible({
      visible: true,
      stateName: "Patvirtinti",
      data: {
        lessonId: this.lesson.id,
        onAction: (id: number, status: string, valid: boolean) =>
          this.postStatus({
            lessonId: id,
            status: status,
            isValid: valid
          }).then(() => this.initialize())
      }
    });
  }

  private Reject() {
    this.setAuthorizeModalVisible({
      visible: true,
      stateName: "Atmesti",
      data: {
        lessonId: this.lesson.id,
        onAction: (id: number, status: string, valid: boolean) =>
          this.postStatus({
            lessonId: id,
            status: status,
            isValid: valid
          }).then(() => this.initialize())
      }
    });
  }

  private onAnswerQuestion(model: AssignmentModel) {
    this.setAnswerModalVisible({
      visible: true,
      stateName: "Atsakyti",
      data: {
        assignmentId: model.id,
        question: model.description,
        lessonId: this.lesson.id
      }
    });
  }

  get lessonState() {
    if (this.lesson.state == "Published") {
      return "Publikuota";
    }
    if (this.lesson.state == "Waiting") {
      return "Laukiama";
    }
    if (this.lesson.state == "Created") {
      return "Sukurta";
    }
    if (this.lesson.state == "Rejected") {
      return "Atmesta";
    }

    return "";
  }

  get lessonProgress() {
    if (this.lesson.progress) {
      if (this.lesson.progress == "Active") {
        return "(Aktyvus)";
      }
      if (this.lesson.progress == "Completed") {
        return "(Užbaigta)";
      }
    }

    return "";
  }
}

export default Lesson;
</script>

<style lang="scss" scoped>
.text-center {
  text-align: center;
  width: 100px;
}

.container {
  align-items: center;
  display: flex;
  flex-direction: column;
}
</style>
