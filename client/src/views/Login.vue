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
        <el-button type="primary" @click="onSubmit" :loading="loading">
          <span>Login</span>
        </el-button>
      </el-form-item>
    </el-form>
  </el-col>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { FormRefModel } from "../models/refs/form.ref.model";
import { UserService } from "../services/user.service";
import { LoginFormModel } from "../models/forms/login.form.model";

@Component
class Login extends Vue {
  private form: LoginFormModel;
  private loading: boolean;
  private rule = {
    username: [
      { required: true, message: "Please input username", trigger: "blur" }
    ],
    password: [
      { required: true, message: "Please input password", trigger: "blur" }
    ]
  };

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
      .then(() =>
        UserService.login({
          username: this.form.username,
          password: this.form.password
        })
      )
      .then((response: any) => {
        this.loading = false;
        localStorage.setItem("session", response.data.session);
      })
      .catch(() => {
        this.loading = false;
      });
  }
}

export default Login;
</script>

<style lang="scss" scoped></style>
