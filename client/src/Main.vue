<template>
  <el-container>
    <el-header>Header</el-header>
    <el-container>
      <el-aside>Aside</el-aside>
      <el-container>
        <el-main>
          <el-row>
            <router-view></router-view>
          </el-row>
        </el-main>
        <el-footer>Footer</el-footer>
      </el-container>
      <el-aside>Aside</el-aside>
    </el-container>
  </el-container>
</template>

<style lang="scss"></style>

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
