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
import { ValidationRule } from "ant-design-vue/types/form/form";

const Register = Vue.extend({
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
          rules: Array<{}>()
        }
      },
      confirm: {
        key: "confirm",
        placeholder: "Confirm password",
        options: {
          rules: Array<{}>()
        }
      }
    };

    input.password.options.rules.push({
      required: true,
      message: "Please input your Password!"
    });

    input.password.options.rules.push({
      validator: function(
        rule: ValidationRule,
        value: String,
        error: Function
      ) {
        if (value && form.isFieldTouched(input.confirm.key)) {
          form.validateFields([input.confirm.key]);
        }
        error();
      }
    });

    input.confirm.options.rules.push({
      required: true,
      message: "Please confirm your Password!"
    });

    input.confirm.options.rules.push({
      message: "Passwords do not match!",
      validator: function(
        rule: ValidationRule,
        value: String,
        error: Function
      ) {
        if (value && value !== form.getFieldValue(input.password.key)) {
          error(rule.message);
        }
        error();
      }
    });

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

export default Register;
</script>
