
<template>
  <div v-if="value">
    <v-text-field
      v-model="editedTodo.todoName"
      label="Add new todo (Press enter to add)"
      solo
      :append-icon="editedTodo.todoName?'add_circle_outline':''"
      @click:append="create"
      @keydown.enter="create"
    ></v-text-field>

    <v-divider></v-divider>

    <v-layout my-1 align-center>
      <strong class="mx-3 subheading success--text">Total todo: {{ todoItems.length }}</strong>
      <v-divider vertical></v-divider>
      <strong class="mx-3 info--text text--darken-3">Remaining: {{ remainingTodos }}</strong>
      <v-divider vertical></v-divider>
      <strong class="mx-3 black--text">Completed: {{ completedTodos }}</strong>
      <v-spacer></v-spacer>
      <v-progress-circular :value="progress" class="mr-2"></v-progress-circular>
    </v-layout>

    <v-divider class="mb-3"></v-divider>

    <v-card v-if="todoItems.length">
      <v-list class="py-0">
        <template v-for="(todo, i) in todoItems">
          <v-divider v-if="i !== 0" :key="`${i}-divider`"></v-divider>

          <v-text-field
            v-if="editingIndex==i"
            v-model="todoName"
            :key="i"
            height="15"
            full-width
            solo
            class="py-0 my-0 customizeTextField"
            @keydown.enter="update"
            :append-icon="todoName?'add_circle_outline':''"
            @click:append="update"
          ></v-text-field>

          <v-list-tile v-else :key="`${i}-${todo.todoName}`">
            <v-list-tile-action class="pt-2">
              <v-checkbox v-model="todo.isCompleted" color="info darken-3">
                <template v-slot:label>
                  <div :class="todo.isCompleted && 'primary--text' || 'grey--text'" class="ml-3" v-text="todo.todoName"></div>
                </template>
              </v-checkbox>
            </v-list-tile-action>
            <v-spacer></v-spacer>
            <v-scroll-x-transition>
              <span>
                <v-btn icon @click="changeName(todo.todoName,i)" title="Edit">
                  <v-icon color="danger" size="20">edit</v-icon>
                </v-btn>
                <v-btn flat icon danger @click="remove(i)" title="Remove">
                  <v-icon color="danger">delete_forever</v-icon>
                </v-btn>
              </span>
            </v-scroll-x-transition>
          </v-list-tile>
        </template>
      </v-list>
    </v-card>
    <!-- <code>{{todoItems}}</code> -->
  </div>
</template>
<script>
// import _ from "lodash";
export default {
  components: {},
  props: ["value"],
  data: () => ({
    todoItems: [],
    editedTodo: { todoName: "", isCompleted: false, change: false },
    defaultTodo: { todoName: "", isCompleted: false, change: false },
    todoName: "",
    editingIndex: -1
  }),
  computed: {
    completedTodos() {
      return this.todoItems.filter(todo => todo.isCompleted).length;
    },
    progress() {
      return (this.completedTodos / this.todoItems.length) * 100;
    },
    remainingTodos() {
      return this.todoItems.length - this.completedTodos;
    }
  },
  watch: {},
  mounted() {
    this.value.forEach(todo => (todo.change = false));
    this.todoItems = this.value;
  },
  methods: {
    create() {
      this.todoItems.push(this.editedTodo);
      this.editedTodo = Object.assign({}, this.defaultTodo);
    },
    update() {
      if (this.editingIndex != -1) {
        this.todoItems[this.editingIndex].todoName = this.todoName;
      }
      this.editingIndex = -1;
    },
    remove(index) {
      this.todoItems.splice(index, 1);
    },
    changeName(name, index) {
      this.editingIndex = index;
      this.todoName = name;
    }
  }
};
</script>



<style lang="stylus">
.customizeTextField {
  .v-text-field__details {
    display: none !important;
  }

  .v-input__slot {
    margin: 0 !important;
  }

  input[type=text] {
    margin: 0 !important;
  }
}
</style>



