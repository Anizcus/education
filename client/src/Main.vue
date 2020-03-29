<template>
  <el-container class="container">
    <el-header class="header">
      <el-row>
        <el-col :span="4" :offset="20">
          <router-link to="/user/login/" v-slot="{ href, route }">
            <el-link :href="href" type="primary">{{ route.name }}</el-link>
          </router-link>
          <span> or </span>
          <router-link to="/user/register/" v-slot="{ href, route }">
            <el-link :href="href" type="primary">{{ route.name }}</el-link>
          </router-link>
        </el-col>
      </el-row>
    </el-header>
    <el-container class="wrapper">
      <el-aside>
        <el-menu :router="true" default-active="/" mode="vertical" class="menu">
          <el-menu-item index="/">Home</el-menu-item>
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

<style lang="scss">
html, body {
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

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
class Main extends Vue {
  public constructor() {
    super();
  }
}
export default Main;
</script>
