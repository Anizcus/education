<template>
  <el-col>
    <el-form :model="form" :rules="rule" ref="form">
      <el-form-item label="Username" prop="username">
        <el-input v-model="form.username"></el-input>
      </el-form-item>
      <el-form-item label="Password" prop="password">
        <el-input v-model="form.password"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit" :loading="loading"
          >Submit</el-button
        >
      </el-form-item>
    </el-form>
  </el-col>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { FormRefModel } from "../models/refs/form.ref.model";
import { UserService } from "../services/user.service";
import {
  LoginFormModel,
  LoginFormRules
} from "../models/forms/login.form.model";

@Component
class Login extends Vue {
  private form: LoginFormModel;
  private rule = LoginFormRules;
  private loading: boolean;

  public $refs!: {
    form: FormRefModel;
  };

  public constructor() {
    super();

    this.form = {
      username: "",
      password: ""
    };
    this.loading = false;
  }

  private onSubmit() {
    if (this.loading) {
      return;
    }

    this.loading = true;
    this.$refs.form
      .validate()
      .then(() => {
        return UserService.login({
          username: this.form.username,
          password: this.form.password
        });
      })
      .then(response => {
        localStorage.setItem("session", response.data.session);
      })
      .catch((error) => {
        console.log(error);
      })
      .finally(() => {
        this.loading = false;
      });
  }
}

export default Login;
</script>

<style lang="scss" scoped></style>
