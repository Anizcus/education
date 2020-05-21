<template>
  <el-row>
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
        <el-form-item label="Vartotojo vardas" prop="username">
          <el-input v-model="form.username"></el-input>
        </el-form-item>
        <el-form-item label="Slaptažodis" prop="password">
          <el-input v-model="form.password" :show-password="true"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit" :loading="loading">
            <span>Prisijungti</span>
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
import { LoginFormModel } from "../models/forms/login.form.model";
import { mapActions, ActionMethod } from "vuex";

@Component({
  methods: {
    ...mapActions("user", {
      login: "login",
      getProfile: "getProfile"
    })
  }
})
class Login extends Vue {
  private login!: ActionMethod;
  private getProfile!: ActionMethod;
  private form: LoginFormModel = {
    username: "",
    password: ""
  };
  private loading = false;
  private rule = {
    username: [
      {
        required: true,
        message: "Vartotojo vardas yra privalomas",
        trigger: "blur"
      }
    ],
    password: [
      { required: true, message: "Slaptažodis yra privalomas", trigger: "blur" }
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
        this.alert.message = `Jūs sėkmingai prisijungėte kaip ${user.name}!`;
        this.alert.type = "success";
        this.loading = false;
      })
      .catch(error => {
        this.alert.message = error;
        this.alert.type = "error";
        this.loading = false;
      });
  }
}

export default Login;
</script>

<style lang="scss" scoped></style>
