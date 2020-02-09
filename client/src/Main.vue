<template>
  <a-layout class="container">
    <a-layout-header>
      <div class="logo"></div>
    </a-layout-header>
    <a-layout>
      <a-layout-sider :collapsible="true" :defaultCollapsed="true">
        <a-menu
          :defaultSelectedKeys="['home']"
          :defaultOpenKeys="['sub-home']"
          theme="dark"
          mode="inline"
        >
          <a-sub-menu key="sub-home">
            <span slot="title">
              <a-icon type="home" />
              <span>Home</span>
            </span>
            <a-menu-item key="home">
              <a-icon type="home" />
              <span>
                <router-link to="/">Welcome</router-link>
              </span>
            </a-menu-item>
            <a-menu-item key="about">
              <a-icon type="info" />
              <span>
                <router-link to="/about">About</router-link>
              </span>
            </a-menu-item>
          </a-sub-menu>
          <a-sub-menu key="sub-other">
            <span slot="title">
              <a-icon type="up" />
              <span>Up</span>
            </span>
          </a-sub-menu>
        </a-menu>
      </a-layout-sider>
      <a-layout>
        <a-breadcrumb>
          <a-breadcrumb-item>Home</a-breadcrumb-item>
          <a-breadcrumb-item>
            <a href="">Application Center</a>
          </a-breadcrumb-item>
          <a-breadcrumb-item>
            <a href="">Application List</a>
          </a-breadcrumb-item>
          <a-breadcrumb-item>An Application</a-breadcrumb-item>
        </a-breadcrumb>
        <a-layout-content class="container">
          <router-view />
        </a-layout-content>
        <a-layout-footer class="footer">
          Educational system with gamification ©2020 Created by Daniel Vrubel
        </a-layout-footer>
      </a-layout>
      <a-layout-sider
        :collapsible="true"
        :defaultCollapsed="true"
        :reverseArrow="true"
        @collapse="this.onToggleRightLayout"
      >
        <b-collapse icon="user" title="Anizcus" :shrink="isRightSideCollapsed">
          <span>HEllo worlD!</span>
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

.footer {
  text-align: center;
}
</style>

<script lang="ts">
import Vue from "vue";
import Collapse from "./components/collapse.vue";

const Main = Vue.extend({
  data: function() {
    return {
      isRightSideCollapsed: true,
      isUserCollapseOpen: false
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
  }
});

export default Main;
</script>
