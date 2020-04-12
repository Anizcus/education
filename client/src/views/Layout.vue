<template>
  <el-container class="container">
    <el-header class="header">
      <el-row>
        <el-col :span="6" :offset="18">
          <p v-if="!session">
            <router-link to="/user/login/" v-slot="{ href, route }">
              <el-link :href="href" type="primary">{{ route.name }}</el-link>
            </router-link>
            <span> or </span>
            <router-link to="/user/register/" v-slot="{ href, route }">
              <el-link :href="href" type="primary">{{ route.name }}</el-link>
            </router-link>
          </p>
          <p v-else>Hello {{ session.name }}!</p>
        </el-col>
      </el-row>
    </el-header>
    <el-container class="wrapper">
      <el-aside>
        <el-menu :router="true" default-active="/" mode="vertical" class="menu">
          <el-menu-item index="/">Home</el-menu-item>
          <el-menu-item index="/about">About</el-menu-item>
        </el-menu>
      </el-aside>
      <el-main class="content">
        <el-row>
          <router-view></router-view>
        </el-row>
      </el-main>
      <el-aside class="side-right">ASide</el-aside>
    </el-container>
    <el-footer class="footer">Footer</el-footer>
  </el-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters, mapActions, ActionMethod } from "vuex";
import { SessionModel } from "../models/stores/user.store.model";

@Component({
  computed: {
    ...mapGetters("user", {
      session: "session"
    })
  }
})
class Main extends Vue {
  private session!: SessionModel;
}
export default Main;
</script>

<style lang="scss">
html,
body {
  height: 100%;
  margin: 0;
}

.container {
  height: 100%;
  padding: 8px;
}

.wrapper {
  height: calc(100% - 120px);
  overflow: auto;
}

.content {
  height: 100%;
}

.side-right {
  border-left: solid 1px #e6e6e6;
}

.menu {
  height: 100%;
}

.header {
  border-bottom: solid 1px #e6e6e6;
}

.footer {
  border-top: solid 1px #e6e6e6;
}
</style>