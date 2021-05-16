<template>
  <div>
    <ul
      class="more-option"
      :style="{
        top: screenY - 10 + 'px',
        left: screenX - 112 + 'px',
      }"
    >
      <li>Nhân bản</li>
      <li @click="deleteClick()">Xoá</li>
      <li>Ngưng sử dụng</li>
    </ul>
    <ReportDialog
      v-if="isShowReportLog == true"
      @deleteEmployee="deleteEmployee"
      @hideReportLog="hideReportLog"
      :codeDelete="codeDeleteTemp"
    />
  </div>
</template>
<script>
import axios from "axios";
import ReportDialog from "../dialogs/ReportDialog";

export default {
  components: {
    ReportDialog,
  },
  props: {
    screenX: { type: NaN, default: 0 },
    screenY: { type: NaN, default: 0 },
    idDelete: { type: String, default: "" },
    codeDelete: { type: String, default: "" },
  },
  data() {
    return {
      isShowReportLog: false, //Bien hien thi report
      codeDeleteTemp: "", //Bien
    };
  },

  methods: {
    deleteClick() {
      this.codeDeleteTemp = this.codeDelete;
      this.isShowReportLog = true;
    },
    async deleteEmployee() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees?id=" + this.idDelete;
      await axios
        .delete(aipUrl)
        .then((res) => {
          console.log(res);
          if (res.status == 200) {
            this.$emit("hideMoreOption");
            this.$emit("getData");
          }
        })
        .catch((err) => {
          console.log(err);
        });
    },
    hideReportLog() {
      this.isShowReportLog = false;
    },
  },
};
</script>

<style scope>
.more-option {
  z-index: 99;
  position: absolute;
  background-color: rgb(255, 255, 255);
  border: 1px solid #dedede;
  padding-left: 0;
}
ul li {
  list-style: none;
  text-align: inherit;
  padding: 8px;
  box-sizing: border-box;
  outline: none;
  text-transform: none;
  text-decoration: none;
  font-weight: 100;
  cursor: pointer;
}
ul li:hover {
  background-color: #eee;
  color: #2cbc18;
}
</style>