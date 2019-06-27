<template>
  <v-card>
    <v-card-title>
      <h4 class="headline">Assign Role</h4>
    </v-card-title>
    <v-card-text class="py-0">
      <v-radio-group v-model="editedItem.projectRole" :mandatory="false">
        <!-- <v-radio :value="roles.projectOwner.value" color="primary">
          <template v-slot:label>
            <div>
              <h5 class="font-weight-medium">{{roles.projectOwner.label}}</h5>
              <p class="caption ma-0 pa-0">{{roles.projectOwner.description}}</p>
            </div>
          </template>
        </v-radio> -->
        <v-radio :value="roles.admin.value" color="primary">
          <template v-slot:label>
            <div>
              <h5 class="font-weight-medium">{{roles.admin.label}}</h5>
              <p class="caption ma-0 pa-0">{{roles.admin.description}}</p>
            </div>
          </template>
        </v-radio>
        <v-radio :value="roles.member.value" color="primary" class="mt-3">
          <template v-slot:label>
            <div>
              <h5 class="font-weight-medium">{{roles.member.label}}</h5>
              <p class="caption ma-0 pa-0">{{roles.member.description}}</p>
            </div>
          </template>
        </v-radio>
      </v-radio-group>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="red darken-1" flat @click.native="close()">Cancel</v-btn>
      <v-btn color="blue darken-1" flat @click.native="update">Save</v-btn>
    </v-card-actions>
    <!-- <code>{{editedItem}}</code> -->
  </v-card>
</template>
<script>
import { Roles } from "../../../../enum/enums.js";
export default {
  components: {},
  props: ["value"],
  data: () => ({
    editedItem: {},
    roles: Roles
  }),

  computed: {},

  watch: {},
  mounted() {
    this.editedItem = this.value;
  },
  methods: {
    close(item) {
      this.$emit("close", item);
    },
    create() {
      this.$root.createItem(
        this.editedItem,
        "ProjectMemberService/Create",
        this
      );
    },
    update() {
      this.$root.updateItem(
        this.editedItem,
        "ProjectMemberService/Update",
        this
      );
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

