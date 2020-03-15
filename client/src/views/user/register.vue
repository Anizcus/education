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
      <a-input
        :placeholder="input.confirm.placeholder"
        v-decorator="[input.confirm.key, input.confirm.options]"
      >
        <a-icon slot="prefix" type="lock" />
        <a-tooltip slot="suffix" title="Input same password">
          <a-icon type="info-circle" class="icon" />
        </a-tooltip>
      </a-input>
    </a-form-item>
    <a-form-item>
      <a-button type="primary" html-type="submit">Register</a-button>
    </a-form-item>
  </a-form>
</template>

<style scoped lang="scss">
.icon {
  color: rgba(0, 0, 0, 0.45);
}
</style>

<script lang="ts">
import Vue from "vue";

const Register = Vue.extend({
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
            rules: [
              { required: true, message: "Please input your Password!" },
              {
                validator: function(
                  rule: String,
                  value: String,
                  error: Function
                ) {
                  if (value && form.isFieldTouched("confirm")) {
                    form.validateField("confirm", { force: true });
                  }
                  error();
                }
              }
            ]
          }
        },
        confirm: {
          key: "confirm",
          placeholder: "Confirm password",
          options: {
            rules: [
              { required: true, message: "Please confirm your Password!" },
              {
                validator: function(
                  rule: String,
                  value: String,
                  error: Function
                ) {
                  if (value && value !== form.getFieldValue("password")) {
                    error("Passwords do not match!");
                  }
                  error();
                }
              }
            ]
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

export default Register;
</script>
