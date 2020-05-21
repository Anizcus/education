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
      <el-table-column prop="name" label="Vartotojo vardas" width="180">
        <template slot-scope="scope">
          <el-button type="text" @click="() => onProfile(scope.$index)">
            {{ scope.row.name }}
          </el-button>
        </template>
      </el-table-column>
      <el-table-column prop="level" label="Lygis" width="180"></el-table-column>
      <el-table-column prop="role" label="Rolė"> 
        <template slot-scope="scope">
            {{ mapRole(scope.row.role) }}
        </template>
      </el-table-column>
      <el-table-column
        v-if="session && session.role === 'Administrator'"
        label="Operacijos"
        width="180"
      >
        <template slot-scope="scope">
          <el-button
            type="primary"
            @click="() => onModify(scope.$index)"
            size="small"
            >Modifikuoti</el-button
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
  },
})
class Users extends Vue {
  private getUsers!: ActionMethod;
  private getRoles!: ActionMethod;
  private setManageUserModalVisible!: ActionMethod;
  private users!: Array<ProfileListModel>;
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
    const roleList = [...this.roles];
    const roleId = roleList.find(
      (item) => item.name === this.users[index].role
    )!.id;

    this.setManageUserModalVisible({
      visible: true,
      stateName: "Atnaujinti",
      data: {
        id: this.users[index].id,
        name: this.mapRole(this.users[index].name),
        roles: roleList,
        role: roleId,
      },
    });
  }

  private mapRole(role: string) {
    if (role == 'Administrator')
    {
      return 'Administratorius';
    }

    if (role == 'Teacher')
    {
      return 'Mokytojas';
    }

    if (role == 'Student')
    {
      return 'Studentas';
    }

    return '';
  }
}

export default Users;
</script>
