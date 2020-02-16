<template>
  <a-layout>
    <a-form :form="form" @submit="onSubmit">
      <a-form-item>
        <a-input placeholder="Username" v-decorator="input.username">
          <a-icon slot="prefix" type="user" />
          <a-tooltip slot="suffix" title="Extra information">
            <a-icon type="info-circle" style="color: rgba(0,0,0,.45)" />
          </a-tooltip>
        </a-input>
      </a-form-item>
      <a-form-item>
        <a-input placeholder="Password" v-decorator="input.password">
          <a-icon slot="prefix" type="lock" />
          <a-tooltip slot="suffix" title="Extra information">
            <a-icon type="info-circle" style="color: rgba(0,0,0,.45)" />
          </a-tooltip>
        </a-input>
      </a-form-item>
      <a-form-item>
        <a-input placeholder="Confirm password" v-decorator="input.confirm">
          <a-icon slot="prefix" type="lock" />
          <a-tooltip slot="suffix" title="Input same password">
            <a-icon type="info-circle" style="color: rgba(0,0,0,.45)" />
          </a-tooltip>
        </a-input>
      </a-form-item>
      <a-form-item>
        <a-button type="primary" html-type="submit">Register</a-button>
      </a-form-item>
    </a-form>
  </a-layout>
</template>

<style lang="sass" scoped></style>

<script lang="ts">
import Vue from "vue";
import {
  ValidateCallback,
  ValidationRule
} from "ant-design-vue/types/form/form";

const Register = Vue.extend({
  data: function() {
    const form = this.$form.createForm(this);
    return {
      form,
      input: {
        username: [
          "username",
          {
            rules: [{ required: true, message: "Please input your username!" }]
          }
        ],
        password: [
          "password",
          {
            rules: [
              { required: true, message: "Please input your Password!" },
              {
                validator: function(
                  rule: String,
                  value: String,
                  error: Function
                ) {
                  if (value && form.isFieldTouched("confirm")) {
                    form.validateFields(["confirm"], { force: true });
                  }
                  error();
                }
              }
            ]
          }
        ],
        confirm: [
          "confirm",
          {
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
        ]
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
