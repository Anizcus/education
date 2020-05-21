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
  <el-table
    v-else
    :data="
      groups.filter(
        group =>
          !search || group.label.toLowerCase().includes(search.toLowerCase())
      )
    "
    empty-text="Nėra duomenų!"
  >
    <el-table-column type="expand">
      <template slot-scope="scope">
        <div v-if="scope.row.options.length">
          <template v-for="option in scope.row.options">
            <el-button
              size="mini"
              type="info"
              :key="option.id"
              @click="() => onUpdateType(option.id, option.name)"
              >{{ option.name }}</el-button
            >
          </template>
        </div>
        <div v-else>
          <i>Kategorija neturi tipų!</i>
        </div>
      </template>
    </el-table-column>
    <el-table-column prop="label" label="Kategorija" width="180">
    </el-table-column>
    <el-table-column prop="options" label="Tipų kiekis" width="90">
      <template slot-scope="scope">
        <span>{{ scope.row.options.length }}</span>
      </template>
    </el-table-column>
    <el-table-column align="right">
      <template slot="header" slot-scope="scope">
        <el-input
          style="width: 75%; margin-right: 10px;"
          v-model="search"
          size="mini"
          placeholder="Ieškoti pagal pavadinimą"
        ></el-input>
        <el-button
          size="mini"
          type="warning"
          @click="() => onCreateCategory(scope)"
          >Sukurti</el-button
        >
      </template>
      <template slot-scope="scope">
        <el-button
          size="mini"
          type="danger"
          icon="el-icon-edit"
          @click="() => onUpdateCategory(scope.row.id, scope.row.label)"
          >Redaguoti</el-button
        >
        <el-button
          size="mini"
          type="primary"
          icon="el-icon-plus"
          @click="() => onCreateType(scope.row.id)"
          >Pridėti</el-button
        >
      </template>
    </el-table-column>
  </el-table>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { NameGroupModel, AdminService } from "../services/admin.service";
import { mapActions, ActionMethod } from "vuex";

@Component({
  methods: {
    ...mapActions("modal", {
      setNameModalVisible: "setNameModalVisible"
    })
  }
})
class Configuration extends Vue {
  private setNameModalVisible!: ActionMethod;
  private groups: NameGroupModel[] = [];
  private search = "";
  private loading = true;

  private initialize() {
    AdminService.getCategories()
      .then((groups: NameGroupModel[]) => {
        this.groups = groups;
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  public created() {
    this.initialize();
  }

  private onUpdateCategory(id: number, name: string) {
    this.setNameModalVisible({
      visible: true,
      stateName: "Atnaujinti",
      data: {
        id,
        name,
        title: "Atnaujinti kategorijos pavadinimą.",
        entity: "Kategorijos",
        onAction: (id: number, name: string) =>
          AdminService.updateCategory({
            id,
            name
          }).then(() => this.initialize())
      }
    });
  }

  private onCreateCategory() {
    this.setNameModalVisible({
      visible: true,
      stateName: "Sukurti",
      data: {
        title: "Sukurti naują kategoriją.",
        entity: "Kategorijos",
        onAction: (id: number, name: string) =>
          AdminService.createCategory({
            id,
            name
          }).then(() => this.initialize())
      }
    });
  }

  private onUpdateType(id: number, name: string) {
    this.setNameModalVisible({
      visible: true,
      stateName: "Atnaujinti",
      data: {
        id,
        name,
        title: "Atnaujinti pamokos tipo pavadinimą.",
        entity: "Pamokos tipo",
        onAction: (id: number, name: string) =>
          AdminService.updateType({
            id,
            name
          }).then(() => this.initialize())
      }
    });
  }

  public onCreateType(categoryId: number) {
    this.setNameModalVisible({
      visible: true,
      stateName: "Sukurti",
      data: {
        id: categoryId,
        name: "",
        title: "Pridėti naują pamokos tipą.",
        entity: "Pamokos tipo",
        onAction: (id: number, name: string) =>
          AdminService.createType({
            id,
            name
          }).then(() => this.initialize())
      }
    });
  }
}
export default Configuration;
</script>
