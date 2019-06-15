
<template>
  <div>
    <v-list>
      <v-list-tile avatar v-for="(eventTask, eventTaskIndex) in eventTasks" :key="eventTaskIndex" @click="getdetail(eventTask.id)" class="bottomBorder">
         <v-list-tile-action>
            <v-icon color="pink">star</v-icon>
          </v-list-tile-action>

          <v-list-tile-content>
            <v-list-tile-title v-text="eventTask.taskName"></v-list-tile-title>
            <!-- <v-list-tile-sub-title>subtitle</v-list-tile-sub-title> -->
          </v-list-tile-content>

          <v-list-tile-avatar>
            <img class="customAvatar" src="../../../assets/logo.png">
            <img class="customAvatar" src="../../../assets/logo.png">
          </v-list-tile-avatar>
      </v-list-tile>
    </v-list>
  </div>
</template>
<script>
export default {
  components: {},
  props: ["eventTasks", "loadData"],
  data: () => ({}),

  computed: {},

  watch: {},
  mounted() {},
  methods: {
    getdetail(index) {
      console.log(index);
    },
    create() {
      this.$root.createItem(this.editedItem, "EventTaskService/Create", this);
    },
    update() {
      this.$root.updateItem(this.editedItem, "EventTaskService/Update", this);
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

<style lang="css" scoped>
.bottomBorder{
 border-bottom: 1px solid rgba(0,0,0,0.12);
}
.customAvatar {
 width: 20px;
 height: 20px;
}
</style>
