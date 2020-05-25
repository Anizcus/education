<template>
  <el-row>
    <el-col>
      <el-form label-position="top" :model="form" :rules="rule" ref="form">
        <el-form-item :label="language.Username" prop="username">
          <el-input v-model="form.username"></el-input>
        </el-form-item>
        <el-form-item :label="language.Password" prop="password">
          <el-input v-model="form.password" :show-password="true"></el-input>
        </el-form-item>
        <el-form-item :label="language.ConfirmPassword" prop="confirm">
          <el-input v-model="form.confirm" :show-password="true"></el-input>
        </el-form-item>
        <el-form-item :label="language.Role" prop="role">
          <el-select
            style="width: 100%;"
            :loading="roleLoading"
            :clearable="true"
            v-model="form.role"
            :placeholder="language.SelectRole"
          >
            <el-option
              v-for="role in roles"
              :key="role.id"
              :label="language[role.name]"
              :value="role.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onSubmit" :loading="loading">
            <span>{{ language.Register }}</span>
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
import { LanguageModel } from "../assets/i18n/language";

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
    }),
    ...mapGetters("language", {
      language: "getTranslations"
    })
  }
})
class Register extends Vue {
  private register!: ActionMethod;
  private getRoles!: ActionMethod;
  private language!: LanguageModel;
  private form: RegisterFormModel = {
    username: "",
    password: "",
    confirm: "",
    role: ""
  };
  private loading = false;
  private roleLoading = true;
  private roles!: NameServiceModel[];
  private rule = {};

  public $refs!: {
    form: FormRefModel;
  };

  public created() {
    this.getRoles({ forRegistration: true })
      .then(() => (this.roleLoading = false))
      .catch(() => (this.roleLoading = false));

    this.rule = {
      username: [
        {
          required: true,
          trigger: "blur",
          validator: (rule: object, value: string, callback: Function) => {
            if (value == "") {
              return callback(new Error(this.language.UsernameIsRequired));
            }
            return callback();
          }
        }
      ],
      password: [
        {
          required: true,
          validator: (rule: object, value: string, callback: Function) => {
            if (value == "") {
              return callback(new Error(this.language.EnterPassword));
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
              return callback(new Error(this.language.EnterPassword));
            }

            if (this.form.password !== value) {
              return callback(new Error(this.language.PasswordsDoNotMatch));
            }

            return callback();
          },
          trigger: "blur"
        }
      ],
      role: [
        {
          required: true,
          trigger: "blur",
          validator: (rule: object, value: string, callback: Function) => {
            if (value == "") {
              return callback(new Error(this.language.SelectRole));
            }
            return callback();
          }
        }
      ]
    };
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
