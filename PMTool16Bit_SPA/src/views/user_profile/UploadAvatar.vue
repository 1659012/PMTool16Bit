
<template>
  <div>
    <file-upload
      :url="url"
      :thumb-url="thumbUrl"
      :headers="headers"
      @change="mySaveMethod"
      @success="getResult"
    ></file-upload>
  </div>
</template>
<script>
// import _ from "lodash";
import { FileUploadService } from "v-file-upload";
export default {
  // title: "",
  components: {},
  props: ["returnAvatarId", "returnAvatarUrl", "defaultAvatarId"],
  data: () => ({
    url: "http://localhost:21021/api/services/app/FileService/UploadImage",
    headers: {},
    filesUploaded: [],
    file: {}
    // onProgress: null
  }),

  computed: {},

  watch: {
    file(val) {
      if (!this.$root.isEmptyObject(val)) {
          this.$emit("update:returnAvatarId", val.id);
          this.$emit("update:returnAvatarUrl", val.path);
      }
    }
  },
  mounted() {
    this.initialize();
  },
  computed: {},
  methods: {
    initialize() {},
    thumbUrl(file) {
      return file.myThumbUrlProperty;
    },
    onFileChange(file) {
      // Handle files like:
      this.fileUploaded = file;
    },
    getResult(e) {
      // console.log(e);
      if (e.currentTarget.response.success) {
        // console.log(e.currentTarget.response.result);
        this.file = e.currentTarget.response.result;
      }    
    },
    mySaveMethod(file) {
      let fileUpload = new FileUploadService(
        this.url,
        this.headers,
        this.onProgress
      );
      fileUpload
        .upload(file, { doc_id: 1 })
        .then(e => {
          // Handle success
        })
        .catch(e => {
          // Handle error
        });
    },
    onProgress(event) {
      // Handdle the progress
    }    
  }
};
</script>
<style lang="css">
</style>