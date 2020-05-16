<template>
  <el-dialog
    @open="() => onOpen()"
    :title="`${modalState} ${data && data.name} status`"
    :visible="manageUserModal"
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
      <el-form-item label="Role" prop="role" v-if="data && data.roles">
        <el-select
          style="width: 100%;"
          :clearable="true"
          v-model="form.role"
          placeholder="Select a role"
        >
          <el-option
            v-for="role in data.roles"
            :key="role.id"
            :label="role.name"
            :value="role.id"
          >
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="User status" prop="isBlocked">
        <el-switch
          v-model="form.isBlocked"
          active-text="User will be blocked"
          inactive-text="User is not blocked"
        >
        </el-switch>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onCancel">Cancel</el-button>
      <el-button :loading="buttonLoading" type="primary" @click="onAction">{{
        modalState
      }}</el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { LessonService } from "../services/lesson.service";
import { mapGetters, mapActions, ActionMethod } from "vuex";
import { NameServiceModel } from "../models/services/name.service.model";

interface ModalData {
  id: number;
  name: string;
  roles: NameServiceModel[];
  role: number;
}

interface ManageUserForm {
  isBlocked: boolean;
  role: number | null;
}

@Component({
  methods: {
    ...mapActions("modal", {
      setManageUserModalVisible: "setManageUserModalVisible"
    }),
    ...mapActions("user", {
      modifyStatus: "modifyStatus"
    })
  },
  computed: {
    ...mapGetters("modal", {
      modalState: "modalState",
      manageUserModal: "manageUserModalVisible",
      data: "modalData"
    })
  }
})
class DialogManageUserForm extends Vue {
  private setManageUserModalVisible!: ActionMethod;
  private form: ManageUserForm = {
    isBlocked: false,
    role: null
  };
  private modalState!: string;
  private manageUserModal!: boolean;
  private error = "";
  private data!: ModalData;
  private modifyStatus!: ActionMethod;
  private buttonLoading = false;

  private onAction() {
    this.buttonLoading = true;

    this.modifyStatus({
      userId: this.data.id,
      roleId: this.form.role,
      isBlocked: this.form.isBlocked
    })
      .then(() => {
        this.buttonLoading = false;
        this.setManageUserModalVisible({
          visible: false,
          data: undefined,
          stateName: this.modalState
        });
      })
      .catch(error => {
        this.error = error;
        this.buttonLoading = false;
      });
  }

  private onCloseAlert() {
    this.error = "";
  }

  private onOpen() {
    this.form.role = this.data.role || null;
  }

  private onCancel() {
    this.setManageUserModalVisible({
      visible: false,
      stateName: this.modalState
    });
  }
}
export default DialogManageUserForm;
</script>
