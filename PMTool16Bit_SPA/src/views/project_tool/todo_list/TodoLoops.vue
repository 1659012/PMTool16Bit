
<template>
  <div v-if="value">
    <v-text-field v-model="todo.todoName" label="Add new todo" solo @keydown.enter="create" hint="Press enter to add">
      <v-fade-transition v-slot:append>
        <v-icon v-if="task" @click="create">add_circle</v-icon>
      </v-fade-transition>
    </v-text-field>

    <h2 class="display-1 success--text pl-3">
      Total todo:&nbsp;
      <v-fade-transition leave-absolute>
        <span :key="`todos-${todoItems.length}`">{{ todoItems.length }}</span>
      </v-fade-transition>
    </h2>

    <v-divider class="mt-3"></v-divider>

    <v-layout my-1 align-center>
      <strong class="mx-3 info--text text--darken-3">Remaining: {{ remainingTodos }}</strong>

      <v-divider vertical></v-divider>

      <strong class="mx-3 black--text">Completed: {{ completedTodos }}</strong>

      <v-spacer></v-spacer>

      <v-progress-circular :value="progress" class="mr-2"></v-progress-circular>
    </v-layout>

    <v-divider class="mb-3"></v-divider>

    <v-card v-if="todoItems.length > 0">
      <v-list class="py-0">
        <template v-for="(todo, i) in todoItems">
          <v-divider v-if="i !== 0" :key="`${i}-divider`"></v-divider>

          <v-list-tile :key="`${i}-${todo.todoName}`">
            <v-list-tile-action class="pt-2">
              <v-checkbox v-model="todo.isCompleted" color="info darken-3">
                <template v-slot:label>
                  <div :class="todo.isCompleted && 'grey--text' || 'text--primary'" class="ml-3" v-text="todo.todoName"></div>
                </template>
              </v-checkbox>
            </v-list-tile-action>

            <v-spacer></v-spacer>

            <v-scroll-x-transition>
              <!-- <v-icon v-if="todo.isCompleted" color="success">check</v-icon> -->
              <v-btn icon @click="remove(i)">
                <v-icon color="danger">delete_forever</v-icon>
              </v-btn>
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
    todo: { todoName: "", isCompleted: false },
    defaultTodo: { todoName: "", isCompleted: false }
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
    this.todoItems = this.value;
  },
  methods: {
    create() {
      this.todoItems.push(this.todo);
      this.todo = Object.assign({}, this.defaultTodo);
    },
    remove(index){
      this.todoItems.splice(index, 1);
    }
  }
};
</script>

