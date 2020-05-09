<template>
  <el-row>
    <el-col>
      <el-form label-position="top" :model="form" :rules="rule" ref="form">
        <el-form-item label="Username" prop="username">
          <el-input v-model="form.username"></el-input>
        </el-form-item>
        <el-form-item label="Password" prop="password">
          <el-input v-model="form.password" :show-password="true"></el-input>
        </el-form-item>
        <el-form-item label="Confirm password" prop="confirm">
          <el-input v-model="form.confirm" :show-password="true"></el-input>
        </el-form-item>
        <el-form-item label="Role" prop="role">
          <el-select style="width: 100%;" :loading="roleLoading" :clearable="true" v-model="form.role" placeholder="Select a role">
            <el-option
              v-for="role in roles"
              :key="role.id"
              :label="role.name"
              :value="role.id">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit" :loading="loading">
            <span>Register</span>
          </el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </el-row>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { FormRefModel } from "../models/refs/form.ref.model";
import { RegisterFormModel } from "../models/forms/register.form.model";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { NameServiceModel } from "../models/services/name.service.model";

@Component({
  methods: {
    ...mapActions("user", {
      register: "register",
      getRoles: "getRoles"
    })
  },
  computed: {
    ...mapGetters("user", {
      roles: "roles"
    })
  }
})
class Register extends Vue {
  private register!: ActionMethod;
  private getRoles!: ActionMethod;
  private form: RegisterFormModel = {
    username: "",
    password: "",
    confirm: "",
    role: ""
  };
  private loading = false;
  private roleLoading = true;
  private roles!: NameServiceModel[];
  private rule = {
    username: [
      { required: true, message: "Please input username", trigger: "blur" }
    ],
    password: [
      {
        required: true,
        validator: (rule: object, value: string, callback: Function) => {
          if (value == "") {
            return callback(new Error("Please input password"));
          }

          if (this.form.confirm !== "") {
            this.$refs.form.validateField("confirm");
          }

          return callback();
        },
        trigger: "blur"
      }
    ],
    confirm: [
      {
        required: true,
        validator: (rule: object, value: string, callback: Function) => {
          if (value == "") {
            return callback(new Error("Please input password"));
          }

          if (this.form.password !== value) {
            return callback(new Error("Password inputs don't match!"));
          }

          return callback();
        },
        trigger: "blur"
      }
    ],
    role: [{ required: true, message: "Please select a role!", trigger: "blur" }]
  };

  public $refs!: {
    form: FormRefModel;
  };

  public mounted() {
    this.getRoles({forRegistration: true})
      .then(() => this.roleLoading = false)
      .catch(() => this.roleLoading = false);
  }

  private onSubmit() {
    if (this.loading) {
      return;
    } else {
      this.loading = true;
    }

    this.$refs.form
      .validate()
      .then(() =>
        this.register({
          username: this.form.username,
          password: this.form.password,
          role: Number(this.form.role)
        })
      )
      .then(response => {
        this.loading = false;
        alert(response.name);
      })
      .catch(() => {
        this.loading = false;
      });
  }
}

export default Register;
</script>

<style lang="scss" scoped></style>
