<template>
  <el-col>
    <el-form :model="form" :rules="rule" ref="form">
      <el-form-item label="Username" prop="username">
        <el-input v-model="form.username"></el-input>
      </el-form-item>
      <el-form-item label="Password" prop="password">
        <el-input v-model="form.password"></el-input>
      </el-form-item>
      <el-form-item label="Confirm password" prop="confirm">
        <el-input v-model="form.confirm"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit" :loading="loading">
          <span>Register</span>
        </el-button>
      </el-form-item>
    </el-form>
  </el-col>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { FormRefModel } from "../models/refs/form.ref.model";
import { RegisterFormModel } from "../models/forms/register.form.model";
import { mapActions, ActionMethod } from "vuex";

@Component({
  methods: {
    ...mapActions("user", {
      register: "register"
    })
  }
})
class Register extends Vue {
  private register!: ActionMethod;
  private form: RegisterFormModel = {
    username: "",
    password: "",
    confirm: ""
  };
  private loading = false;
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
    ]
  };

  public $refs!: {
    form: FormRefModel;
  };

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
          password: this.form.password
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
