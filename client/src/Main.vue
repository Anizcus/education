<template>
  <a-layout class="container">
    <a-layout-header class="header">
      <span></span>
    </a-layout-header>
    <a-layout :hasSider="true">
      <a-layout-sider :collapsible="true"></a-layout-sider>
      <a-layout>
        <a-layout-content class="breadcrumb">
          <span>Home</span>
        </a-layout-content>
        <a-layout-content class="content">
          <router-view></router-view>
        </a-layout-content>
        <a-layout-content class="footer">
          <span>Footer</span>
        </a-layout-content>
      </a-layout>
      <a-layout-sider :collapsible="true" :reverseArrow="true"></a-layout-sider>
    </a-layout>
  </a-layout>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class Main extends Vue {
  private visibleMenu: boolean;

  public constructor() {
    super();
    this.visibleMenu = false;
  }

  private onShowMenu() {
    this.visibleMenu = true;
  }

  private onCloseMenu() {
    this.visibleMenu = false;
  }
}
</script>

<style lang="scss">
%shared-block {
  text-align: center;
}

.container {
  height: 100%;
}

.header {
  @extend %shared-block;
}

.breadcrumb {
  @extend %shared-block;

  background: white;
  padding: 4px;
  margin: 0 16px;
}

.content {
  @extend %shared-block;

  background: white;
  height: 100%;
  padding: 0px 8px 0px 8px;
  margin: 16px 32px;
  overflow: auto;
}

.sider {
  height: 100%;
}

.footer {
  @extend %shared-block;

  background: white;
  padding: 4px;
  margin: 0px 16px;
}
</style>
