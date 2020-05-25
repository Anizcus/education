<template>
  <el-dialog
    :title="data && data.labelTitle"
    :visible="lessonModal"
    @open="onOpen"
    @close="onCancel"
  >
    <el-alert
      v-if="error"
      :title="error"
      type="error"
      :show-icon="true"
      @close="onCloseAlert"
    >
    </el-alert>
    <el-form :model="form" label-position="top">
      <el-form-item :label="language.LessonTitle">
        <el-input v-model="form.name"></el-input>
      </el-form-item>
      <el-form-item :label="language.LessonType">
        <el-select
          style="width: 100%;"
          v-model="form.type"
          :placeholder="language.SelectLessonType"
          :loading="typeLoading"
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
      </el-form-item>
      <el-row>
        <el-col :span="18">
          <el-form-item :label="language.LessonDescription">
            <el-input
              type="textarea"
              v-model="form.description"
              :rows="5"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="6">
          <el-form-item :label="language.Badge" class="uploader">
            <el-upload
              action="undefined"
              :drag="false"
              :multiple="false"
              :show-file-list="false"
              :before-upload="handleUpload"
            >
              <img
                v-if="(data && data.image) || image"
                :src="(data && data.image) || getImageUrl()"
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
      <el-button @click="onCancel">{{ data && data.labelBack }}</el-button>
      <el-button type="primary" @click="onAction">{{
        data && data.labelAction
      }}</el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { LessonService } from "../services/lesson.service";
import { mapGetters, mapActions, ActionMethod } from "vuex";
import { NameGroupModel, AdminService } from "../services/admin.service";
import { LanguageModel } from "../assets/i18n/language";

interface ModalData {
  onSuccess: () => void;
  onFailure: () => void;
  labelTitle: string;
  labelBack: string;
  labelAction: string;
  name: string;
  description: string;
  type: string;
  image: string;
  id: string;
}

@Component({
  methods: {
    ...mapActions("modal", {
      setLessonModalVisible: "setLessonModalVisible",
    }),
  },
  computed: {
    ...mapGetters("modal", {
      modalState: "modalState",
      data: "modalData",
      lessonModal: "lessonModalVisible",
    }),
    ...mapGetters("language", {
      language: "getTranslations",
    }),
  },
})
class DialogLessonForm extends Vue {
  private setLessonModalVisible!: ActionMethod;
  private groups: NameGroupModel[] = [];
  private language!: LanguageModel;
  private form = {
    name: "",
    description: "",
    type: "",
  };
  private modalState!: string;
  private lessonModal!: boolean;
  private image: string | Blob = "";
  private error = "";
  private formData = new FormData();
  private loading = false;
  private typeLoading = false;
  private data!: ModalData;

  private onOpen() {
    this.typeLoading = true;
    AdminService.getTypes()
      .then((groups: NameGroupModel[]) => {
        this.groups = groups;

        if (this.data.type) {
          const group = this.groups.find((group) =>
            group.options.find((option) => option.name == this.data.type)
          );

          if (group) {
            this.form.type = group.options.find(
              (option) => option.name == this.data.type
            ).id;
          }
        }
        this.typeLoading = false;
      })
      .catch(() => {
        this.typeLoading = false;
      });
    this.form.description = this.data.description || "";
    this.form.name = this.data.name || "";
  }

  private handleUpload(file: File) {
    if (file.type === "image/png") {
      this.data.image = "";
      this.image = file as Blob;
    }
  }

  private getImageUrl() {
    return URL.createObjectURL(this.image);
  }

  private onAction() {
    if (this.form.name.trim() === "") {
      this.error = this.language.LessonTitleIsRequired;
      return;
    }

    if (this.form.description.trim() === "") {
      this.error = this.language.LessonDescriptionIsRequired;
      return;
    }

    this.loading = true;

    this.formData.set("name", this.form.name);
    this.formData.set("description", this.form.description);
    this.formData.set("badge", this.image);
    this.formData.set("type", this.form.type);

    if (this.modalState == "Update") {
      this.formData.set("id", this.data.id);
      LessonService.putLesson(this.formData)
        .then(() => {
          this.loading = false;
          this.data.onSuccess();
          this.setLessonModalVisible({
            visible: false,
            data: undefined,
            stateName: "Close",
          });
        })
        .catch((error) => {
          this.loading = false;
          this.error = error.toString();
        });
    } else {
      LessonService.postLesson(this.formData)
        .then(() => {
          this.loading = false;
          this.data.onSuccess();
          this.setLessonModalVisible({
            visible: false,
            data: undefined,
            stateName: "Close",
          });
        })
        .catch((error) => {
          this.loading = false;
          this.error = error.toString();
        });
    }

    this.error = "";
  }

  private onCloseAlert() {
    this.error = "";
  }

  private onCancel() {
    this.form.name = "";
    this.form.description = "";
    this.image = "";
    this.error = "";

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
