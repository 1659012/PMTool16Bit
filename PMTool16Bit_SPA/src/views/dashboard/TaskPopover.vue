<template>
  <v-menu v-model="event.open" full-width offset-x :close-on-content-click="false" open-on-click class="">
    <template v-slot:activator="{ on }">
      <div v-ripple class="myEvent" v-on="on" v-html="event.taskName"></div>
      <!-- <v-btn color="primary" flat dark v-on="on" class="text-capitalize" v-html="event.taskName"></v-btn> -->
    </template>
    <v-card>
      <v-card-text>
        <code>{{event}}</code>
      </v-card-text>
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
  padding:5px 3px;
  cursor: pointer;
  margin: 1px 0px;
}
</style>