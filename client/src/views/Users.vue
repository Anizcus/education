<template>
  <el-container
    v-if="loading"
    style="
      align-items: center;
      justify-content: center;
      height: 100%;
    "
  >
    <el-button :loading="loading" type="info" :circle="true"></el-button>
  </el-container>
  <div v-else>
    <el-table :data="users || []" style="width: 100%" row-key="id">
      <el-table-column prop="name" :label="language.Username" width="180">
        <template slot-scope="scope">
          <el-button type="text" @click="() => onProfile(scope.$index)">
            {{ scope.row.name }}
          </el-button>
        </template>
      </el-table-column>
      <el-table-column
        prop="level"
        :label="language.Level"
        width="180"
      ></el-table-column>
      <el-table-column prop="role" :label="language.Role">
        <template slot-scope="scope">
          {{ mapRole(scope.row.role) }}
        </template>
      </el-table-column>
      <el-table-column
        v-if="session && session.role === 'Administrator'"
        :label="language.Operations"
        width="180"
      >
        <template slot-scope="scope">
          <el-button
            type="primary"
            @click="() => onModify(scope.$index)"
            size="small"
            >{{ language.Modify }}</el-button
          >
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import {
  ProfileListModel,
  ProfileModel,
  SessionModel,
} from "../models/stores/user.store.model";
import { NameServiceModel } from "../models/services/name.service.model";
import { LanguageModel } from "../assets/i18n/language";

@Component({
  methods: {
    ...mapActions("user", {
      getUsers: "getUsers",
      getRoles: "getRoles",
    }),
    ...mapActions("modal", {
      setManageUserModalVisible: "setManageUserModalVisible",
    }),
  },
  computed: {
    ...mapGetters("user", {
      users: "users",
      roles: "roles",
      session: "session",
    }),
    ...mapGetters("language", {
      language: "getTranslations",
    }),
  },
})
class Users extends Vue {
  private getUsers!: ActionMethod;
  private getRoles!: ActionMethod;
  private setManageUserModalVisible!: ActionMethod;
  private users!: Array<ProfileListModel>;
  private language!: LanguageModel;
  private roles!: NameServiceModel[];
  private session!: SessionModel;
  private loading = true;

  public mounted() {
    Promise.all([this.getUsers(), this.getRoles({ forRegistration: false })])
      .then(() => {
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private onProfile(index: number) {
    this.$router.push({
      name: "Profile",
      params: { id: this.users[index].id.toString() },
    });
  }

  private onModify(index: number) {
    const roleList = this.roles.map((item) => {
      return { id: item.id, name: this.language[item.name] };
    });

    const role = this.roles.find((item) => {
      return item.name === this.users[index].role
    });

    this.setManageUserModalVisible({
      visible: true,
      stateName: "Update",
      data: {
        id: this.users[index].id,
        name: this.mapRole(this.users[index].name),
        roles: roleList,
        role: role.id,
        labelAction: this.language.Update,
        labelTitle: this.language.UpdateUserStatus,
        labelSelect: this.language.SelectRole,
        labelBack: this.language.Back,
        labelAccess: this.language.UserAccess,
        labelBlock: this.language.BlockUser,
        labelUnblock: this.language.UnblockUser,
        labelRole: this.language.Role,
      },
    });
  }

  private mapRole(role: string): string {
    return this.language[role];
  }
}

export default Users;
</script>
