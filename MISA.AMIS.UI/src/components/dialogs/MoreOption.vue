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
    screenX: { type: NaN, default: 0 }, //Biến chứa toạ độ x của chuột khi ấn
    screenY: { type: NaN, default: 0 }, //Biến chứa toạ độ y của chuột khi ấn
    employee: { type: Object, default: Object.create(null) }, //Biến chứa thông tin nhân viên khi truyền lên
  },
  data() {
    return {
      isShowReportLog: false, //Bien hien thi report
      codeDeleteTemp: "", //Bien tạm thời lưu code nhân viên cần xoá
      screenXDialog: 0, //Biến chứa toạ độ x của dialog
      screenYDialog: 0, //Biến chứa toạ độ y của dialog
    };
  },
  watch: {
    screenY() {
      if (this.screenY > 770) this.screenYDialog = 770;
    },
  },
  methods: {
    /**
     * Bắt sự kiện khi nhấn xoá
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    deleteClick() {
      this.codeDeleteTemp = this.employee.EmployeeCode;
      this.isShowReportLog = true;
    },
    /**
     * Hàm gọi api lấy mã nhân viên lớn nhất và clone
     * Createby: TDDUNG
     * Date: 11/5/2021
     */
    async clickClone() {
      this.$emit("cloneEmployee", this.employee.EmployeeId);
    },
    /**
     * Hàm gọi api xoá nhân viên
     * Createby: TDDUNG
     * Date: 11/5/2021
     */
    deleteEmployee() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees?id=" +
        this.employee.EmployeeId;
      axios
        .delete(aipUrl)
        .then((res) => {
          if (res.status == 200) {
            this.$emit("shownoti", "Sửa nhân viên thành công");
            this.$emit("hideMoreOption");
          }
        })
        .catch((err) => {
          console.log(err);
        });
      this.$emit("getData");
    },
    /**
     * Hàm ẩn cửa sổ thông báo
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
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
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
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