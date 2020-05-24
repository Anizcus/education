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
        <el-form-item :label="language.Username" prop="username">
          <el-input v-model="form.username"></el-input>
        </el-form-item>
        <el-form-item :label="language.Password" prop="password">
          <el-input v-model="form.password" :show-password="true"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit" :loading="loading">
            <span>{{ language.Login }}</span>
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
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { LanguageModel } from "../assets/i18n/language";

@Component({
  methods: {
    ...mapActions("user", {
      login: "login",
      getProfile: "getProfile",
    }),
  },
  computed: {
    ...mapGetters("language", {
      language: "getTranslations",
    }),
  },
})
class Login extends Vue {
  private login!: ActionMethod;
  private getProfile!: ActionMethod;
  private language!: LanguageModel;
  private form: LoginFormModel = {
    username: "",
    password: "",
  };
  private loading = false;
  private rule = {
    username: [
      {
        required: true,
        message: this.language.UsernameIsRequired,
        trigger: "blur",
      },
    ],
    password: [
      {
        required: true,
        message: this.language.PasswordIsRequired,
        trigger: "blur",
      },
    ],
  };
  private alert = {
    message: "",
    type: "",
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
          password: this.form.password,
        })
      )
      .then((user) => {
        this.alert.message = `${this.language.SuccessfullyLoggedInAs} ${user.name}!`;
        this.alert.type = "success";
        this.loading = false;
      })
      .catch((error) => {
        this.alert.message = error;
        this.alert.type = "error";
        this.loading = false;
      });
  }
}

export default Login;
</script>

<style lang="scss" scoped></style>
