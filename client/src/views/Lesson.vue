<template>
  <el-card shadow="hover" style="margin-bottom: 14px;">
    <el-row>
      <el-col :span="4">
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
                <p>Lesson: {{ lesson.name }}</p>
                <div>State: {{ lesson.state }}</div>
              </el-col>
              <el-col :span="12">
                <p style="float: right;">Author: {{ lesson.ownerName }}</p>
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
            <p>Practice {{ lesson.assignments.length }}</p>
            <el-collapse>
              <el-collapse-item
                v-for="(assignment, index) in lesson.assignments"
                :key="`question-${index}`"
                :title="`Question ${index + 1}`"
              >
                <el-row>
                  <el-col :span="20">
                    <div>{{ assignment.description }}</div>
                    <div>Experience ({{ assignment.experience }})</div>
                  </el-col>
                  <el-col :span="4">
                    <el-button
                      :plain="true"
                      type="warning"
                      icon="el-icon-edit"
                      :circle="true"
                      size="mini"
                      @click="() => onAssignmentEdit(index, assignment)"
                    ></el-button>
                    <el-button
                      :plain="true"
                      type="danger"
                      icon="el-icon-delete"
                      :circle="true"
                      size="mini"
                      @click="() => onAssignmentDelete(index)"
                    ></el-button>
                  </el-col>
                </el-row>
              </el-collapse-item>
            </el-collapse>
            <p>
              <el-button
                @click="() => onAssignmentCreate()"
                type="primary"
                :plain="true"
                style="float: left;"
                >Add a question</el-button
              >
            </p>
            <p>
              <el-button @click="Publish" type="success" :plain="true" style="float: right;"
                >Publish</el-button
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
  AssignmentModel,
} from "../models/stores/lesson.store.model";
import { ActionMethod, mapGetters, mapActions } from "vuex";

@Component({
  methods: {
    ...mapActions("lesson", {
      getLesson: "getLessonById",
      postAssignments: "postLessonAssignments"
    }),
    ...mapActions("modal", {
      setAssignmentModalVisible: "setAssignmentModalVisible",
    }),
  },
  computed: {
    ...mapGetters("lesson", {
      lesson: "lesson",
    }),
  },
})
class Lesson extends Vue {
  private getLesson!: ActionMethod;
  private setAssignmentModalVisible!: ActionMethod;
  private postAssignments!: ActionMethod;
  private lesson!: LessonModel;
  private loading = true;
  private dateOptions = {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
    hour12: false,
  };

  public mounted() {
    this.getLesson({ id: Number(this.$route.params.id) })
      .then((res) => {
        console.log(res);
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private onAssignmentCreate() {
    this.setAssignmentModalVisible({
      visible: true,
      stateName: "Create",
    });
  }

  private onAssignmentDelete(index: number) {
    this.setAssignmentModalVisible({
      visible: true,
      stateName: "Delete",
      data: {
        index,
      },
    });
  }

  private onAssignmentEdit(index: number, assignment: AssignmentModel) {
    this.setAssignmentModalVisible({
      visible: true,
      stateName: "Update",
      data: {
        index,
        ...assignment,
      },
    });
  }

  private Publish() {
    this.postAssignments();
  }
}

export default Lesson;
</script>

<style lang="scss" scoped>
.text-center {
  text-align: center;
  width: 100px;
}
</style>
