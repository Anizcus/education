<template>
  <el-container class="container">
    <el-header class="header">
      <el-row>
        <el-col :span="11" :offset="5">
          <i-search-lesson></i-search-lesson>
        </el-col>
        <el-col :span="5" :offset="1">
          <el-button-group v-if="!session">
            <el-button
              icon="el-icon-user"
              type="primary"
              @click="() => onLogin()"
              >{{ language.Login }}</el-button
            >
            <el-button
              icon="el-icon-key"
              type="primary"
              @click="() => onRegister()"
              >{{ language.Register }}</el-button
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
            <el-tooltip :content="language.Logout" placement="bottom">
              <el-button
                icon="el-icon-close"
                type="primary"
                @click="() => onLogout()"
              ></el-button>
            </el-tooltip>
          </el-button-group>
        </el-col>
        <el-col :span="2">
          <el-select
            @change="onChangeLanguage"
            :placeholder="language.Language"
            :value="language[languageKey]"
          >
            <el-option value="English">{{ language.English }}</el-option>
            <el-option value="Lithuanian">{{ language.Lithuanian }}</el-option>
          </el-select>
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
          >{{ language.MainPage }}</el-button
        >
        <el-button
          icon="el-icon-info"
          type="default"
          class="menu-button"
          @click="() => onAbout()"
          >{{ language.AboutPage }}</el-button
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
          >{{ language.Profile }}</el-button
        >
        <el-button
          icon="el-icon-s-custom"
          type="default"
          class="menu-button"
          @click="() => onUsers()"
          >{{ language.Users }}</el-button
        >
        <el-button
          v-if="
            session &&
              (session.role === 'Administrator' || session.role === 'Teacher')
          "
          icon="el-icon-s-management"
          type="default"
          class="menu-button"
          @click="() => onManagement()"
          >{{ language.ManageLessons }}</el-button
        >
        <el-button
          v-if="session && session.role === 'Administrator'"
          icon="el-icon-s-tools"
          type="default"
          class="menu-button"
          @click="() => onConfiguration()"
          >{{ language.Configuration }}</el-button
        >
      </el-aside>
    </el-container>
    <el-footer class="footer">{{ language.FooterNotice }}</el-footer>
  </el-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapGetters, mapActions, ActionMethod } from "vuex";
import { SessionModel } from "../models/stores/user.store.model";
import { LanguageModel } from "../assets/i18n/language";
import SearchLessonComponent from "../components/SearchLessonComponent.vue";

@Component({
  computed: {
    ...mapGetters("user", {
      session: "session"
    }),
    ...mapGetters("language", {
      language: "getTranslations",
      languageKey: "getKey"
    })
  },
  methods: {
    ...mapActions("user", {
      logout: "logout"
    }),
    ...mapActions("language", {
      setLanguage: "setTranslations"
    })
  },
  components: {
    "i-search-lesson": SearchLessonComponent
  }
})
class Main extends Vue {
  private language!: LanguageModel;
  private languageKey!: string;
  private setLanguage!: ActionMethod;
  private session!: SessionModel;
  private logout!: ActionMethod;

  private onConfiguration() {
    if (this.$route.name !== "Configuration") {
      this.$router.push({ name: "Configuration" });
    }
  }

  private onManagement() {
    if (this.$route.name !== "Management") {
      this.$router.push({ name: "Management" });
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

  private onChangeLanguage(language: string) {
    this.setLanguage(language);
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
