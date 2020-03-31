<template>
  <el-col>
    <el-alert
      v-if="alert.message"
      :title="alert.message"
      :type="alert.type"
      :show-icon="true"
      @close="onCloseAlert"
    >
    </el-alert>
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
import Vue from "vue";
import Component from "vue-class-component";
import { FormRefModel } from "../models/refs/form.ref.model";
import { LoginFormModel } from "../models/forms/login.form.model";
import { mapActions, ActionMethod } from "vuex";

@Component({
  methods: {
    ...mapActions("user", {
      login: "login"
    })
  }
})
class Login extends Vue {
  private login!: ActionMethod;
  private form: LoginFormModel = {
    username: "",
    password: ""
  };
  private loading = false;
  private rule = {
    username: [
      { required: true, message: "Please input username", trigger: "blur" }
    ],
    password: [
      { required: true, message: "Please input password", trigger: "blur" }
    ]
  };
  private alert = {
    message: "",
    type: ""
  };

  public $refs!: {
    form: FormRefModel;
  };

  private onCloseAlert() {
    this.alert.message = "";
    this.alert.type = "";
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
        this.login({
          username: this.form.username,
          password: this.form.password
        })
      )
      .then(user => {
        this.alert.message = `You have successfully logged in as ${user.name}!`;
        this.alert.type = "success";
        this.loading = false;
      })
      .catch(error => {
        this.alert.message = error.response.data.error;
        this.alert.type = "error";
        this.loading = false;
      });
  }
}

export default Login;
</script>

<style lang="scss" scoped></style>
