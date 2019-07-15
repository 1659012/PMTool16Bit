<template>
  <v-menu
    v-model="event.open"
    max-width="500px"
    offset-x
    :close-on-content-click="false"
    open-on-click
    class
  >
    <template v-slot:activator="{ on }">
      <div v-ripple class="myEvent" v-on="on" v-html="event.taskName"></div>
      <!-- <v-btn color="primary" flat dark v-on="on" class="text-capitalize" v-html="event.taskName"></v-btn> -->
    </template>
    <v-card>
      <v-card-text>
        <v-layout row wrap align-center>
          <v-flex xs3>
            <label class="font-weight-light">Project:</label>
          </v-flex>
          <v-flex xs8>
            <span class="body-1 text-truncate">{{event.projectName}}</span>
          </v-flex>
          <v-flex xs1>
            <v-tooltip bottom>
              <template v-slot:activator="{ on }">
                <v-btn
                  flat
                  icon
                  v-on="on"
                  :to="`/ProjectDetail/${event.projectId}`"
                  target="_blank"
                  class="mt-0"
                >
                  <v-icon>arrow_right</v-icon>
                </v-btn>
              </template>
              <span>To project detail</span>
            </v-tooltip>
          </v-flex>

          <v-flex xs3>
            <label class="font-weight-light">Task Group:</label>
          </v-flex>
          <v-flex xs8>
            <span class="body-1 text-truncate" v-html="event.taskGroupName"></span>
          </v-flex>
          <v-flex xs1></v-flex>

          <v-flex xs3>
            <label class="font-weight-light">Task:</label>
          </v-flex>
          <v-flex xs8>
            <span class="body-1 text-truncate">{{event.taskName}}</span>
          </v-flex>
          <v-flex xs1>
            <v-tooltip bottom>
              <template v-slot:activator="{ on }">
                <v-btn
                  flat
                  icon
                  v-on="on"
                  :to="`/ProjectDetail/${event.projectId}`"
                  target="_blank"
                  class="mt-0"
                >
                  <v-icon>arrow_right</v-icon>
                </v-btn>
              </template>
              <span>Open detail task</span>
            </v-tooltip>
          </v-flex>

          <v-flex xs3>
            <label class="font-weight-light">Start date:</label>
          </v-flex>
          <v-flex xs9>
            <span class="body-1">{{event.startDate|date}}</span>
          </v-flex>

          <v-flex xs3 mt-2>
            <label class="font-weight-light">Due date:</label>
          </v-flex>
          <v-flex xs9 mt-2>
            <span class="body-1">{{event.dueDate|date}}</span>
          </v-flex>
        </v-layout>

        <!-- <code>{{event}}</code> -->
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" flat @click.native="event.open = false">Close</v-btn>
      </v-card-actions>
    </v-card>
  </v-menu>
</template>

<script>
export default {
  components: {},
  props: ["event"],
  data: () => ({}),
  computed: {},
  watch: {},
  mounted() {},
  methods: {}
};
</script>
<style>
.myEvent {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  /* border-radius: 2px; */
  background-color: #1867c0;
  color: #ffffff;
  border: 1px solid #1867c0;
  width: 100%;
  font-size: 12px;
  padding: 3px;
  cursor: pointer;
  margin: 1px 0px;
}
</style>