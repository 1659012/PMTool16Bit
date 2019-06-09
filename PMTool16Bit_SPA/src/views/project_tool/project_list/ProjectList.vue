
<template>
  <v-container fluid grid-list-lg>
    <h1>Project list</h1>
    <v-toolbar flat color="transparent">
      <v-btn color="success" @click="exportExcel">Export data</v-btn>
      <v-spacer></v-spacer>
      <v-btn color="primary" dark @click="createProject">New Project</v-btn>

      <v-dialog lazy v-model="dialog" fullscreen hide-overlay persistent>
        <!-- <createorder v-if="dialog" lazy v-model="editedItem" @close="close"/> -->
      </v-dialog>
    </v-toolbar>

    <v-layout row wrap mx-3>
      <v-flex lg12>filter here</v-flex>
    </v-layout>

    <div>
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

            <td class="text-lg-center colMax120">
              <v-btn flat icon @click="editItem(props.item)">
                <v-icon small>edit</v-icon>
              </v-btn>
              <v-btn flat icon @click="deleteItem(props.item)">
                <v-icon small>delete</v-icon>
              </v-btn>
            </td>
          </tr>
        </template>
        <template slot="no-data">
          <v-btn color="primary" @click="resetFilterData()">Reset</v-btn>
        </template>
      </v-data-table>
    </div>

    <v-layout row wrap>
      <v-flex lg4 v-for="(item, index) in items" :key="index">
        <v-card>
          <v-card-title primary-title>
            <div>
              <h3 class="headline mb-0">{{item.projectName}}</h3>
            </div>
          </v-card-title>
          <v-card-actions>
            <v-btn flat color="green">Edit</v-btn>
            <v-btn flat color="orange">Delete</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
<script>
import _ from "lodash";
import moment from "moment";
export default {
  title: "Project list",
  components: {},
  data: () => ({
    dialog: false,
    loading: false,
    resetFilter: false,
    totalItems: 0,
    filter: {},
    pagination: {},
    headers: [
      {
        text: "Project name",
        value: "projectName"
      },
      {
        text: "Actions",
        align: "center",
        value: "tool",
        sortable: false
      }
    ],
    items: [],
    editedItem: {},
    defaultItem: {}
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
    initialize() {},
    resetFilterData() {
      this.filter = {};
      // this.resetFilter = true;
      // let me = this;
      // _.delay(() => {
      //   me.resetFilter = false;
      // }, 10);
    },
    createProject() {
      this.editedItem = Object.assign({}, this.defaultItem);
      this.dialog = true;
    },
    loadData() {
      let me = this;
      me.loading = true;
      this.axios
        .get("http://localhost:21021/api/services/app/ProjectService/GetAll", {
          params: {
            skipCount: (me.pagination.page - 1) * me.pagination.rowsPerPage,
            maxResultCount: me.pagination.rowsPerPage,
            sorting: me.sortDirection,
            projectName: me.filter.projectName
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

    exportExcel() {
      // this.$root.exportExcel("FileService/ExportAbSheet");
    },
    editItem(item) {
      // this.editedItem = item;
      // this.dialog = true;
    },
    deleteItem(item) {
      // this.$root.deleteItem(item, "OrderService/Delete", this);
    },
    close(item) {
      this.loadData();
      this.dialog = false;
      this.editedItem = Object.assign({}, this.defaultItem);
    }
  }
};
</script>
