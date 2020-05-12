<template>
  <div v-if="loading">
    <el-button :loading="loading" type="info" :circle="true"></el-button>
  </div>
  <div v-else>
    <el-table
      :data="users || []"
      style="width: 100%"
      row-key="id"
    >
      <el-table-column prop="name" label="Name" width="180">
        <template slot-scope="scope">
          <el-link @click="() => onProfileClick(scope.$index)">{{scope.row.name}}</el-link>
        </template>
      </el-table-column>
      <el-table-column prop="level" label="Level" width="180"></el-table-column>
      <el-table-column prop="role" label="Role"> </el-table-column>
      <el-table-column
        v-if="profile && profile.role === 'Administrator'"
        label="Operations"
        width="180">
        <template slot-scope="scope">
          <el-button type="primary" @click="() => onModify(scope.$index)" size="small">Modify</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapActions, ActionMethod, mapGetters } from "vuex";
import { ProfileListModel, ProfileModel, SessionModel } from "../models/stores/user.store.model";
import { NameServiceModel } from "../models/services/name.service.model";

@Component({
  methods: {
    ...mapActions("user", {
      getUsers: "getUsers",
      getRoles: "getRoles",
      getProfile: "getProfile"
    }),
    ...mapActions("modal", {
      setManageUserModalVisible: "setManageUserModalVisible"
    })
  },
  computed: {
    ...mapGetters("user", {
      profile: "profile",
      users: "users",
      roles: "roles",
      session: "session"
    })
  }
})
class Users extends Vue {
  private getUsers!: ActionMethod;
  private getRoles!: ActionMethod;
  private getProfile!: ActionMethod;
  private setManageUserModalVisible!: ActionMethod;
  private users!: Array<ProfileListModel>;
  private roles!: NameServiceModel[];
  private profile!: ProfileModel;
  private session!: SessionModel;
  private loading = true;

  public mounted() {
    Promise.all([
      this.getUsers(), 
      this.getRoles({forRegistration: false}), 
    ]).then(() => {
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
    });

    if (this.session) {
      this.getProfile({ id: this.session.id});
    }
  }

  private onProfileClick(index: number) {
    this.$router.push({ name: "Profile", params: { id: this.users[index].id.toString() } });
  }

  private onModify(index: number) {
    const roleList = [... this.roles];
    const roleId = roleList
      .find(item => item.name === this.users[index].role)!
      .id;

    this.setManageUserModalVisible({
      visible: true,
      stateName: "Update",
      data: {
        id: this.users[index].id,
        name: this.users[index].name,
        roles: roleList,
        role: roleId
      }
    });
  }
}

export default Users;
</script>
