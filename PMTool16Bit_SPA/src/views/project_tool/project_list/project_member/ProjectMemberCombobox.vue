
<template>
  <div>
    <v-combobox
      v-model="model"
      :items="items"
      :loading="loading"
      label="Add member(s)"
      item-text="fullName"
      item-value="id"
      multiple
      chips
      cache-items
      :search-input.sync="keyword"
      clearable
    >
      <template slot="selection" slot-scope="data">
        <v-chip
          :key="JSON.stringify(data.item)"
          :selected="data.selected"
          :disabled="data.disabled"
          class="v-chip--select-multi"
        >
          <v-avatar
            v-if="data.item.fullName"
            class="accent white--text"
          >{{ data.item.fullName.slice(0, 1).toUpperCase() }}</v-avatar>
          {{ data.item.fullName }}
          <v-icon
            class="ml-3"
            color="blue-grey"
            title="Remove item"
            small
            @click.prevent="removeItem(data.index)"
            v-if="roles.projectOwner.value!=data.item.projectRole"
          >close</v-icon>
          
           <v-icon
            class="ml-3"
            color="green accent-4"
            title="Project owner"
            small           
            v-else
          >stars</v-icon>
        </v-chip>
      </template>
    </v-combobox>
  </div>
</template>
<script>
import _ from "lodash";
import { Roles } from "../../../../enum/enums";
export default {
  components: {},
  props: ["defaultItems", "returnItems"],
  data() {
    return {
      items: [],
      model: [],
      keyword: "",
      loading: false,
      roles: Roles
    };
  },
  computed: {},
  watch: {
    keyword: _.debounce(function(val) {
      if (this.loading) return;
      this.loadData();
    }, 400),

    model(value) {
      if (Array.isArray(value)) {
        let result = [];
        value.forEach(user => {
          if (this.isObject(user)) {
            let item = { memberId: null, projectRole: this.roles.member.value };
            item.memberId = user.id;
            if (user.projectRole) item.projectRole = user.projectRole;
            result.push(item);
          } else {
            let index = value.indexOf(user);
            value.splice(index, 1);
          }
        });

        this.$emit("update:returnItems", result);
      }
    }
  },

  mounted() {
    // this.loadData();
    this.loadDefaultItems();
  },
  methods: {
    isObject(object) {
      return object instanceof Object && object.constructor === Object;
    },

    loadData() {
      let me = this;
      me.loading = true;
      this.axios
        .get("User/GetDropdownByKeyword", {
          params: {
            maxResultCount: 50,
            keyword: me.keyword
          }
        })
        .then(response => {
          if (response.data.success) {
            if (response.data.result) {
              me.items = response.data.result;
            } else {
              me.items = [];
            }
            me.loading = false;
          }
        })
        // .then(() => {
        //   me.loadDefaultItems();
        // })
        .catch(e => {
          me.errors.push(e);
        });
    },
    loadDefaultItems() {
      if (Array.isArray(this.defaultItems) && this.defaultItems.length > 0) {
        let memberList = [];
        this.defaultItems.forEach(projectMember => {
          // console.log(projectMember);
          let member = Object.assign({}, projectMember.member);
          member.projectRole = projectMember.projectRole;
          // console.log(member);

          memberList.push(member);
        });
        this.model = _.cloneDeep(memberList);
        this.items = _.cloneDeep(memberList);
      }
    },
    removeItem(index) {
      this.model.splice(index, 1);
    }
  }
};
</script>