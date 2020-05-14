<template>
  <el-container class="container">
    <el-header class="header">
      <el-row>
        <el-col :span="14" :offset="5">
          <i-search-lesson></i-search-lesson>
        </el-col>
        <el-col :span="4" :offset="1">
          <el-button-group v-if="!session">
            <el-button icon="el-icon-user" type="primary">Login</el-button>
            <el-button icon="el-icon-key" type="primary">Register</el-button>
          </el-button-group>
          <p v-else>Hello {{ session.name }} (<el-link @click="() => onLogout()">Logout</el-link>)!</p>
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
        <router-view></router-view>
      </el-main>
      <el-aside class="side-right">
        <el-menu :router="true" mode="vertical" class="menu">
          <el-menu-item v-if="session" :index="`/user/profile/${session.id}`"
            >Profile</el-menu-item
          >
          <el-menu-item index="/users">Users</el-menu-item>
        </el-menu>
      </el-aside>
    </el-container>
    <el-footer class="footer">Simple © Daniel Vrubel - 2020</el-footer>
  </el-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters, mapActions, ActionMethod } from "vuex";
import { SessionModel } from "../models/stores/user.store.model";
import SearchLessonComponent from "../components/SearchLessonComponent.vue";

@Component({
  computed: {
    ...mapGetters("user", {
      session: "session"
    })
  },
  methods: {
    ...mapActions("user", {
      logout: "logout"
    })
  },
  components: {
    "i-search-lesson": SearchLessonComponent
  }
})
class Main extends Vue {
  private session!: SessionModel;
  private logout!: ActionMethod;

  private onLogout() {
    this.logout();
    this.$router.push({ name: 'Lesson Category' });
  }

  private onLogin() {
    this.$router.push({ name: 'Login' });
  }

  private onRegister() {
    this.$router.push({ name: 'Register' });
  }
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

.side-right {
  .menu {
    text-align: right;
    border-right: none;
  }
}

.header {
  border-bottom: solid 1px #e6e6e6;
}

.footer {
  border-top: solid 1px #e6e6e6;
  text-align: center;
  padding: 10px;
}
</style>
