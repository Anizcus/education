<template>
  <a-layout class="container">
    <a-layout-header class="header">
      <div class="logo" style="width: 80%;"></div>
      <div>Logged in as Anizcus</div>
    </a-layout-header>
    <a-layout>
      <a-layout-sider :collapsible="true" :defaultCollapsed="true">
        <a-menu :defaultSelectedKeys="['welcome']" theme="dark" mode="inline">
          <a-menu-item key="welcome">
            <a-icon type="robot" />
            <span>Welcome</span>
          </a-menu-item>
          <a-sub-menu key="sub-home">
            <span slot="title">
              <a-icon type="bars" />
              <span>Main</span>
            </span>
            <a-menu-item key="home">
              <a-icon type="home" />
              <span>Home</span>
            </a-menu-item>
            <a-menu-item key="about">
              <a-icon type="info" />
              <span>About</span>
            </a-menu-item>
          </a-sub-menu>
          <a-sub-menu key="sub-other">
            <span slot="title">
              <a-icon type="bars" />
              <span>Other</span>
            </span>
          </a-sub-menu>
        </a-menu>
      </a-layout-sider>
      <a-layout>
        <a-breadcrumb class="breadcrumb">
          <a-breadcrumb-item>Home</a-breadcrumb-item>
          <a-breadcrumb-item>
            <a href="">Application Center</a>
          </a-breadcrumb-item>
          <a-breadcrumb-item>
            <a href="">Application List</a>
          </a-breadcrumb-item>
          <a-breadcrumb-item>An Application</a-breadcrumb-item>
        </a-breadcrumb>
        <a-layout-content class="content">
          <a-layout class="router">
            <router-view />
          </a-layout>
          <a-layout-footer class="footer">
            Educational system with gamification ©2020 Created by Daniel Vrubel
          </a-layout-footer>
        </a-layout-content>
      </a-layout>
      <a-layout-sider
        :collapsible="true"
        :defaultCollapsed="true"
        :reverseArrow="true"
        @collapse="this.onToggleRightLayout"
      >
        <b-collapse icon="user" title="Level 0" :shrink="isRightSideCollapsed">
          <a-progress type="dashboard" :percent="100">
            <template v-slot:format="percent">
              <a-avatar size="large" shape="square" icon="user" />
              <div style="color: blue;">{{ percent }}%</div>
            </template>
          </a-progress>
          <a-tag style="margin: 0px;" color="pink">Administrator</a-tag>
        </b-collapse>
        <b-collapse icon="bell" title="Activity" :shrink="isRightSideCollapsed">
          <span>Gained Achievement!</span>
        </b-collapse>
      </a-layout-sider>
    </a-layout>
  </a-layout>
</template>

<style scoped lang="scss">
.container {
  height: 100%;
}

.logo {
  margin: 16px;
  height: 32px;
  background: rgba(255, 255, 255, 0.2);
}

.header {
  display: flex;
  color: white;
}

.footer {
  text-align: center;
}

.breadcrumb {
  padding: 14px 12px 10px 12px;
}

.content {
  padding: 0px 12px 0px 12px;
  overflow-x: hidden;
}

.router {
  min-height: calc(100% - 69px);
}
</style>

<script lang="ts">
import Vue from "vue";
import Collapse from "./components/collapse.vue";

const Main = Vue.extend({
  data: function() {
    return {
      isRightSideCollapsed: true,
      isUserCollapseOpen: false,
      route: {}
    };
  },
  components: {
    "b-collapse": Collapse
  },
  methods: {
    onToggleUserCollapse: function(openKeys: string[]) {
      this.isUserCollapseOpen = openKeys.includes("user");
    },
    onToggleRightLayout: function(isCollapsed: boolean) {
      this.isRightSideCollapsed = isCollapsed;
    }
  },
  watch: {
    $route: function(to, from) {
      this.route = to;
    }
  }
});

export default Main;
</script>
