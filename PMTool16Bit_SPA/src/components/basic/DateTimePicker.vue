
<template>
  <div>
    <v-menu
      ref="menu"
      lazy
      :close-on-content-click="false"
      v-model="menu"
      transition="scale-transition"
      offset-y
      full-width
      :nudge-right="40"
      min-width="290px"
      :return-value.sync="date"
    >
      <v-text-field slot="activator" :label="label?label:'Pick date'" v-model="date" prepend-icon="event" readonly></v-text-field>
      <v-date-picker v-model="date" no-title scrollable>
        <v-spacer></v-spacer>
        <v-btn flat color="primary" @click="menu = false">Cancel</v-btn>
        <v-btn flat color="primary" @click="$refs.menu.save(date)">OK</v-btn>
      </v-date-picker>
    </v-menu>  
  </div>
</template>
<script>
export default {
  components: {},
  props: ["label","input","output"],
  data: () => ({date:null, menu:false}),
  computed: {},
  watch: {
    date(value){
      this.$emit("update:output", value);
    }
  },
  mounted() {
    this.date= this.input?this.input.slice(0,10):null;
  },
  methods: {}
};
</script>