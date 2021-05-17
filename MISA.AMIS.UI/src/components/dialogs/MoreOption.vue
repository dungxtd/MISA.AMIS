<template>
  <div>
    <ul
      class="more-option"
      :style="{
        top: screenY - 8 + 'px',
        left: screenX - 120 + 'px',
      }"
    >
      <li @click="clickClone()">Nhân bản</li>
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
    employeeProp: { type: Object, default: Object.create(null) },
  },
  data() {
    return {
      isShowReportLog: false, //Bien hien thi report
      codeDeleteTemp: "", //Bien
      employee: {},
    };
  },

  methods: {
    deleteClick() {
      this.codeDeleteTemp = this.employeeProp.employeeCode;
      this.isShowReportLog = true;
    },
    /**
     * Hàm gọi api lấy mã nhân viên lớn nhất và clone
     * Createby: TDDUNG
     * Date: 11/5/2021
     */
    async clickClone() {
      this.employee = { ...this.employeeProp };
      var aipUrl = "https://localhost:44368/api/v1/Employees/getMaxCode";
      await axios
        .get(aipUrl)
        .then((res) => {
          var temp = res.data[0].split("-");
          this.employee.employeeCode =
            temp[0] + "-" + (parseInt(temp[1]) + 1).toString();
          // console.log(this.employee.employeeCode);
        })
        .catch((err) => {
          console.log(err);
        });
      this.$emit("cloneEmployee", this.employee);
    },
    /**
     * Hàm gọi api xoá nhân viên
     * Createby: TDDUNG
     * Date: 11/5/2021
     */
    async deleteEmployee() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees?id=" +
        this.employeeProp.employeeId;
      axios
        .delete(aipUrl)
        .then((res) => {
          console.log(res);
          if (res.status == 200) {
            this.$emit("hideMoreOption");
          }
        })
        .catch((err) => {
          console.log(err);
        });
      await this.$emit("getData");
    },
    hideReportLog() {
      this.isShowReportLog = false;
      this.$emit("hideMoreOption");
      this.$emit("getData");
    },
  },
};
</script>

<style scope>
.more-option {
  z-index: 15;
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
.unselectable {
  -webkit-user-select: none;
  -webkit-touch-callout: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}
</style>