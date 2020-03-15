<template>
  <a-form :form="form" @submit="onSubmit">
    <a-form-item>
      <a-input
        :placeholder="input.username.placeholder"
        v-decorator="[input.username.key, input.username.options]"
      >
        <a-icon slot="prefix" type="user" />
        <a-tooltip slot="suffix" title="Extra information">
          <a-icon type="info-circle" class="icon" />
        </a-tooltip>
      </a-input>
    </a-form-item>
    <a-form-item>
      <a-input
        :placeholder="input.password.placeholder"
        v-decorator="[input.password.key, input.password.options]"
      >
        <a-icon slot="prefix" type="lock" />
        <a-tooltip slot="suffix" title="Extra information">
          <a-icon type="info-circle" class="icon" />
        </a-tooltip>
      </a-input>
    </a-form-item>
    <a-form-item>
      <a-button type="primary" html-type="submit">Log in</a-button>
    </a-form-item>
  </a-form>
</template>

<style lang="scss" scoped>
.icon {
  color: rgba(0, 0, 0, 0.45);
}
</style>

<script lang="ts">
import Vue from "vue";

const Login = Vue.extend({
  data: function() {
    const form = this.$form.createForm(this);
    const input = {
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
    };

    return {
      form,
      input
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
