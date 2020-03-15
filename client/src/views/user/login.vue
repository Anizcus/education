<template>
  <a-form :form="form" @submit="onSubmit">
    <a-form-item>
      <a-input placeholder="Username" v-decorator="input.username">
        <a-icon slot="prefix" type="user" />
        <a-tooltip slot="suffix" title="Extra information">
          <a-icon type="info-circle" style="color: rgba(0,0,0,0.45)" />
        </a-tooltip>
      </a-input>
    </a-form-item>
    <a-form-item>
      <a-input placeholder="Password" v-decorator="input.password">
        <a-icon slot="prefix" type="lock" />
        <a-tooltip slot="suffix" title="Extra information">
          <a-icon type="info-circle" style="color: rgba(0,0,0,0.45)" />
        </a-tooltip>
      </a-input>
    </a-form-item>
    <a-form-item>
      <a-button type="primary" html-type="submit">Log in</a-button>
    </a-form-item>
  </a-form>
</template>

<style lang="scss" scoped></style>

<script lang="ts">
import Vue from "vue";

const Login = Vue.extend({
  data: function() {
    let form = this.$form.createForm(this);

    return {
      form,
      input: {
        username: {
          key: "username",
          placeholder: "Username",
          options: {
            rules: [{ required: true, message: "Please input your username!" }]
          }
        },
        password: {
          key: "password",
          placeholder: "Password",
          options: {
            rules: [{ required: true, message: "Please input your password!" }]
          }
        }
      }
    };
  },
  methods: {
    onSubmit: function() {
      return null;
    }
  }
});

export default Login;
</script>
