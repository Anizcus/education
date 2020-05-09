<template>
  <div v-if="loading">
    <el-button :loading="loading" type="info" :circle="true"></el-button>
  </div>
  <div v-else>
    <el-table
      :data="users"
      style="width: 100%"
      row-key="id"
      @row-click="onRowClick"
    >
      <el-table-column prop="name" label="Name" width="180"> </el-table-column>
      <el-table-column prop="level" label="Level" width="180"></el-table-column>
      <el-table-column prop="role" label="Role"> </el-table-column>
    </el-table>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { mapActions, ActionMethod } from "vuex";
import { ProfileListModel } from "../models/stores/user.store.model";
import { NameServiceModel } from "../models/services/name.service.model";

@Component({
  methods: {
    ...mapActions("user", {
      getUsers: "getUsers",
      getRoles: "getRoles",
    })
  }
})
class Users extends Vue {
  private getUsers!: ActionMethod;
  private getRoles!: ActionMethod;
  private users: ProfileListModel[] = [];
  private roles: NameServiceModel[] = [];
  private loading = true;

  public mounted() {
    Promise.all([this.getUsers(), this.getRoles({forRegistration: false})])
      .then(([users, roles]) => {
        this.users = users;
        this.roles = roles;
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  private onRowClick(row: ProfileListModel) {
    this.$router.push({ name: "Profile", params: { id: row.id.toString() } });
  }
}

export default Users;
</script>
