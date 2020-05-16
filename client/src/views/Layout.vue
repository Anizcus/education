<template>
  <el-container class="container">
    <el-header class="header">
      <el-row>
        <el-col :span="14" :offset="5">
          <i-search-lesson></i-search-lesson>
        </el-col>
        <el-col :span="4" :offset="1">
          <el-button-group v-if="!session">
            <el-button
              icon="el-icon-user"
              type="primary"
              @click="() => onLogin()"
              >Login</el-button
            >
            <el-button
              icon="el-icon-key"
              type="primary"
              @click="() => onRegister()"
              >Register</el-button
            >
          </el-button-group>
          <el-button-group v-else>
            <el-tooltip :content="session.name" placement="bottom">
              <el-button
                icon="el-icon-user"
                type="primary"
                class="user-button"
                @click="() => onProfile()"
                >{{ session.name }}</el-button
              >
            </el-tooltip>
            <el-tooltip content="Logout" placement="bottom">
              <el-button
                icon="el-icon-close"
                type="primary"
                @click="() => onLogout()"
              ></el-button>
            </el-tooltip>
          </el-button-group>
        </el-col>
      </el-row>
    </el-header>
    <el-container class="wrapper">
      <el-aside class="side-left">
        <el-button
          icon="el-icon-s-home"
          type="default"
          class="menu-button"
          @click="() => onHome()"
          >Home</el-button
        >
        <el-button
          icon="el-icon-info"
          type="default"
          class="menu-button"
          @click="() => onAbout()"
          >About</el-button
        >
      </el-aside>
      <el-main class="content">
        <router-view></router-view>
      </el-main>
      <el-aside class="side-right">
        <el-button
          v-if="session"
          icon="el-icon-user-solid"
          type="default"
          class="menu-button"
          @click="() => onProfile()"
          >Profile</el-button
        >
        <el-button
          icon="el-icon-s-custom"
          type="default"
          class="menu-button"
          @click="() => onUsers()"
          >Users</el-button
        >
        <el-button
          v-if="session && session.role === 'Administrator'"
          icon="el-icon-set-up"
          type="default"
          class="menu-button"
          @click="() => onAdmin()"
          >Admin Area</el-button
        >
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

  private onAdmin() {
    if (this.$route.name !== "Admin") {
      this.$router.push({ name: "Admin" });
    }
  }

  private onLogout() {
    this.logout();

    if (this.$route.name !== "Lesson Category") {
      this.$router.push({ name: "Lesson Category" });
    }
  }

  private onLogin() {
    if (this.$route.name !== "Login") {
      this.$router.push({ name: "Login" });
    }
  }

  private onRegister() {
    if (this.$route.name !== "Register") {
      this.$router.push({ name: "Register" });
    }
  }

  private onProfile() {
    const id = this.session.id.toString();

    if (
      this.$route.name !== "Profile" ||
      (this.$route.name === "Profile" && this.$route.params.id !== id)
    ) {
      this.$router.push({ name: "Profile", params: { id } });
    }
  }

  private onHome() {
    if (this.$route.name !== "Lesson Category") {
      this.$router.push({ name: "Lesson Category" });
    }
  }

  private onAbout() {
    if (this.$route.name !== "About") {
      this.$router.push({ name: "About" });
    }
  }

  private onUsers() {
    if (this.$route.name !== "Users") {
      this.$router.push({ name: "Users" });
    }
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
  padding: 10px;
}

.menu-button {
  width: 100%;
  margin-bottom: 10px;
  margin-top: 10px;
  display: block;
  margin-left: 0px !important;
}

.side-left {
  border-right: solid 1px #e6e6e6;
  padding: 10px;
}

.header {
  border-bottom: solid 1px #e6e6e6;
}

.footer {
  border-top: solid 1px #e6e6e6;
  text-align: center;
  padding: 10px;
}

.user-button {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  width: 150px;
}
</style>
