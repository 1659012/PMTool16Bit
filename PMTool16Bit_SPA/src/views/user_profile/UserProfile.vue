
<template>
 <div>
   profile
 </div>
</template>
<script>
// import _ from "lodash";
// import moment from "moment";
// import DatePicker from "../basiccomponents/DatePicker";
export default {
  // title: "",
   components: {
  
  },
   props: ["userId"],
  data: () => ({
   editedItem:{ userId:null}
  }),
 
  computed: {},
 
  watch: {},
  mounted() {
    this.initialize();
  },
  methods: {
    initialize(){
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
    create() {
      this.$root.createItem(this.editedItem, "TaskGroupService/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "TaskGroupService/Update", this);
    },
    save() {
      this.$validator.validateAll().then(result => {
        if (result) {
          if (this.editedItem.id) {
            this.update();
          } else {
            this.create();
          }
        }
      });
    }
  }
};
</script>