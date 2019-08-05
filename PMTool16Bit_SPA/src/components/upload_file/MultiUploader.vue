<template>
  <div>
    <uploader :target="$store.state.baseUrl+ 'api/services/app/FileService/UploadDocument'" action="POST" v-on:finish="finishUpload"></uploader>
    <div class="my-1 d-flex" v-for="(document, index) in documents" :key="index">
      <p class="pointer mt-1 mb-0" :title="document.name" @click="downloadFile(document.id)">{{document.name}}</p>
      <v-icon @click="documents.splice(index,1)">delete_forever</v-icon>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Uploader from "vue-simple-upload/dist/FileUpload";

export default {
  components: {
    Uploader
  },
  props: ["value"],
  data() {
    return {
      documents: []
    };
  },
  watch: {
    documents(val) {
      let data = val.map(p => p.id).join(",");
      this.$emit("input", data);
    }
  },
  mounted() {
    this.getDocuments();
  },

  methods: {
    finishUpload(e) {
      let result = JSON.parse(e.target.response);
      this.documents.push(result.result);
    },
    getDocuments() {
      if (!this.value) return;
      this.axios
        .get("FileService/GetAllFileFromIds", {
          params: { idString: this.value }
        })
        .then(response => {
          if (response.data.success) {
            this.documents = response.data.result;
          }
        })
        .catch(e => {
          this.errors.push(e);
        });
    },
    downloadFile(id) {
      window.location =
        this.$store.state.baseUrl +
        "api/services/app/FileService/Download?Id=" +
        id;
    }
  }
};
</script>
<style lang="stylus" scoped>
.pointer {
  &:hover {
    text-decoration: underline;
    color: blue;
    cursor: pointer;
  }
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 200px;
}
</style>