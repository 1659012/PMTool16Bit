
<template>
  <v-container fluid grid-list-lg>
    <v-toolbar dense flat color="transparent">
      <v-btn color="deep-purple darken-1" flat class="pl-0" @click="createProject">
        <v-icon left dark class>add_circle_outline</v-icon>New Project
      </v-btn>

      <v-spacer></v-spacer>
      <!-- <v-select
        :items="projectOptions"
        v-model="filter.projectType"
        label="Chose project type"
        item-text="text"
        item-value="value"
        clearable
        style="width:250px!important;
        max-width:250px !important;"
      ></v-select>-->

      <v-divider vertical></v-divider>

      <v-tooltip bottom>
        <template v-slot:activator="{ on }">
          <v-btn :color="listView && !gridView ?'primary':'grey'" dark v-on="on" icon flat class="mx-1" @click="handleListView">
            <v-icon>format_list_bulleted</v-icon>
          </v-btn>
        </template>
        <span>List View</span>
      </v-tooltip>

      <v-tooltip bottom>
        <template v-slot:activator="{ on }">
          <v-btn :color="!listView && gridView ?'primary':'grey'" dark v-on="on" icon flat class="mr-1" @click="handleGridView">
            <v-icon>apps</v-icon>
          </v-btn>
        </template>
        <span>Grid View</span>
      </v-tooltip>

      <v-tooltip bottom>
        <template v-slot:activator="{ on }">
          <v-btn :color="gridView && listView ?'primary':'grey'" dark v-on="on" icon flat class="ml-1" @click="handleAllView">
            <v-icon>vertical_split</v-icon>
          </v-btn>
        </template>
        <span>All View</span>
      </v-tooltip>
      <v-dialog lazy v-model="dialog" max-width="600px" persistent>
        <ProjectCreate v-if="dialog" lazy @close="closeDialog" @cancel="cancelDialog" />
      </v-dialog>
      <v-dialog lazy v-model="deleteProjectDialog" max-width="600px" persistent>
        <ConfirmDeleteProject
          v-if="deleteProjectDialog"
          lazy
          @close="closeDeleteConfirm"
          @cancel="cancelDeleteConfirm"
          v-model="project"
          :loadData="loadData"
        />
      </v-dialog>
    </v-toolbar>
    <v-layout row wrap mx-3 v-show="listView">
      <!-- <v-flex lg12>filter here</v-flex> -->
      <v-flex lg12>
        <v-data-table
          :headers="headers"
          :items="items"
          class="elevation-1"
          :rows-per-page-items="[10,20,50,100]"
          :loading="loading"
          :pagination.sync="pagination"
          :total-items="totalItems"
        >
          <template slot="items" slot-scope="props" class="list-item" transition="slide-y-transition">
            <tr class="tableRow">
              <td class="colMax150">{{ props.item.projectName }}</td>
              <td class="colMax150">{{ props.item.shortDescription }}</td>
              <td class="colMax150">{{ props.item.projectOwner?props.item.projectOwner.fullName:"" }}</td>
              <td class="text-lg-center colMax120">
                <v-btn flat icon :to="`/ProjectDetail/${props.item.id}`">
                  <v-icon small>edit</v-icon>
                </v-btn>
                <v-tooltip bottom>
                  <template v-slot:activator="{ on }">
                    <v-btn flat icon v-on="on" @click="openDeleteConfirm(props.item)" :disabled="props.item.projectOwnerId!=userId">
                      <v-icon small>delete</v-icon>
                    </v-btn>
                  </template>
                  <span>Delete project</span>
                </v-tooltip>
              </td>
            </tr>
          </template>
          <template slot="no-data">
            <v-btn color="primary" @click="resetFilter()">Reset</v-btn>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>

    <v-layout row wrap mx-3 v-show="gridView">
      <v-flex lg3 v-for="(item, index) in items" :key="index">
        <v-hover>
          <v-card slot-scope="{ hover }" :class="`elevation-${hover ? 12 : 2}`">
            <v-card-title primary-title>
              <div>
                <h4 class="mb-0">{{item.projectName}}</h4>
                <label>Project owner</label>
                <h5>{{item.projectOwner?item.projectOwner.fullName:""}}</h5>
              </div>
            </v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn flat color="blue darken-1" :to="`/ProjectDetail/${item.id}`">Detail</v-btn>
              <v-btn flat color="red darken-1" @click="openDeleteConfirm(item)" :disabled="item.projectOwnerId!==userId">Delete</v-btn>
            </v-card-actions>
          </v-card>
        </v-hover>
      </v-flex>
    </v-layout>
    <!-- <code>{{$store.state}}</code> -->
    <!-- <code>{{filter}}</code> -->
  </v-container>
</template>
<script>
import _ from "lodash";
import moment from "moment";
import ProjectCreate from "./ProjectCreate";
import ConfirmDeleteProject from "./ConfirmDeleteProject";
export default {
  title: "Project list",
  components: {
    ProjectCreate,
    ConfirmDeleteProject
  },
  data: () => ({
    dialog: false,
    deleteProjectDialog: false,
    project: {},
    loading: false,
    totalItems: 0,
    filter: {},
    pagination: {},
    projectOptions: [
      { text: "All projects", value: "all" },
      { text: "My own projects", value: "own" },
      { text: "Participated projects", value: "participated" }
    ],
    headers: [
      {
        text: "Project name",
        value: "projectName"
      },
      {
        text: "Short description",
        value: "shortDescription"
      },
      {
        text: "Project owner",
        value: "projectOwner.fullName"
      },
      {
        text: "Actions",
        align: "center",
        value: "tool",
        sortable: false
      }
    ],
    items: [],
    // editedItem: {},
    // defaultItem: { projectName: "", projectOwnerId: null },
    userId: null,
    gridView: false,
    listView: true
  }),

  computed: {
    sortDirection() {
      let me = this;
      return me.pagination.sortBy
        ? me.pagination.sortBy + (me.pagination.descending ? " DESC" : " ASC")
        : "";
    }
  },
  watch: {
    filter: {
      handler: _.debounce(function() {
        this.loadData();
      }, 400),
      deep: true
    },
    pagination: {
      handler() {
        this.loadData();
      },
      deep: true
    }
  },
  mounted() {
    this.initialize();
  },
  methods: {
    initialize() {
      this.userId = this.$store.state.userId;
    },
    handleListView() {
      this.listView = true;
      this.gridView = false;
    },
    handleGridView() {
      this.listView = false;
      this.gridView = true;
    },
    handleAllView() {
      this.listView = true;
      this.gridView = true;
    },
    resetFilter() {
      this.filter = {};
    },
    createProject() {
      this.dialog = true;
    },
    openDeleteConfirm(item) {
      this.project = _.cloneDeep(item);
      this.deleteProjectDialog = true;
    },
    cancelDeleteConfirm() {
      this.project = {};
      this.deleteProjectDialog = false;
    },
    closeDeleteConfirm() {
      this.project = {};
      this.deleteProjectDialog = false;
      this.loadData();
    },
    loadData() {
      let me = this;
      me.loading = true;
      this.axios
        .get("ProjectService/GetAll", {
          params: {
            skipCount: (me.pagination.page - 1) * me.pagination.rowsPerPage,
            maxResultCount: me.pagination.rowsPerPage,
            sorting: me.sortDirection,
            projectName: me.filter.projectName,
            memberId: me.$store.state.userId
          }
        })
        .then(response => {
          if (response.data.success) {
            me.items = response.data.result.items;
            me.totalItems = response.data.result.totalCount;
            me.loading = false;
            // console.log(me.items);
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    deleteItem(item) {
      this.$root.deleteItem(item, "ProjectService/Delete", this);
    },
    closeDialog() {
      this.loadData();
      this.dialog = false;
    },
    cancelDialog() {
      this.dialog = false;
    }
  }
};
</script>
