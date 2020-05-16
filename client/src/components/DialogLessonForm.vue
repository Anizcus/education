<template>
  <el-dialog :title="`${modalState} a lesson`" :visible="lessonModal">
    <el-alert
      v-if="error"
      :title="error"
      type="error"
      :show-icon="true"
      @close="onCloseAlert"
    >
    </el-alert>
    <el-form :model="form" label-position="top">
      <el-form-item label="Lesson name">
        <el-input v-model="form.name"></el-input>
      </el-form-item>
      <el-row>
        <el-col :span="18">
          <el-form-item label="Lesson description">
            <el-input
              type="textarea"
              v-model="form.description"
              :rows="5"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item label="Badge" class="uploader">
            <el-upload
              action="undefined"
              :drag="false"
              :multiple="false"
              :show-file-list="false"
              :before-upload="handleUpload"
            >
              <img
                v-if="image"
                :src="getImageUrl()"
                width="100"
                height="100"
                style="margin-top: 14px;"
              />
              <i v-else class="el-icon-plus uploader-icon"></i>
            </el-upload>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">Cancel</el-button>
      <el-button type="primary" @click="onAction">{{ modalState }}</el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { LessonService } from "../services/lesson.service";
import { mapGetters, mapActions, ActionMethod } from "vuex";

@Component({
  methods: {
    ...mapActions("modal", {
      setLessonModalVisible: "setLessonModalVisible",
    }),
  },
  computed: {
    ...mapGetters("modal", {
      modalState: "modalState",
      lessonModal: "lessonModalVisible",
    }),
  },
})
class DialogLessonForm extends Vue {
  private setLessonModalVisible!: ActionMethod;
  private form = {
    name: "",
    description: "",
  };
  private modalState!: string;
  private lessonModal!: boolean;
  private image: string | Blob = "";
  private error = "";
  private formData = new FormData();
  private loading = false;

  private handleUpload(file: File) {
    if (file.type === "image/png") {
      this.image = file as Blob;
    }
  }

  private getImageUrl() {
    return URL.createObjectURL(this.image);
  }

  private onAction() {
    if (this.form.name.trim() === "") {
      this.error = "Please fill a lesson name!";
      return;
    }

    if (this.form.description.trim() === "") {
      this.error = "Please fill a lesson description!";
      return;
    }

    this.loading = true;

    this.formData.set("name", this.form.name);
    this.formData.set("description", this.form.description);
    this.formData.set("badge", this.image);
    this.formData.set("type", this.$route.params.id);

    LessonService.postLesson(this.formData)
      .then(() => {
        this.loading = false;
        this.setLessonModalVisible({
          visible: false,
          data: undefined,
          stateName: "Close",
        });
      })
      .catch((error) => {
        this.loading = false;
        this.error = error;
      });
  }

  private onCloseAlert() {
    this.error = "";
  }

  private onCancel() {
    this.form.name = "";
    this.form.description = "";
    this.image = "";

    this.setLessonModalVisible({
      visible: false,
      data: undefined,
      stateName: "Cancel",
    });
  }
}
export default DialogLessonForm;
</script>

<style lang="scss">
.uploader-icon {
  font-size: 28px;
  color: #8c939d;
  line-height: 100px;
  text-align: center;
  margin: 16px;
}

.uploader {
  margin-left: 22px;
}

.el-upload {
  display: block;
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
}
</style>
