<template>
  <div>
    <div class="report-model">
      <div class="report-body">
        <div class="report-content">
          <div
            class="icon icon-edit-warning"
            style="margin: auto 0; margin-right: 5px"
          ></div>
          <div class="text-edit-warning">
            Bạn có thực sự muốn xoá Nhân viên &#60;{{ codeDelete }}&#62; không ?
          </div>
        </div>
        <div class="mess-line"></div>
        <div class="report-footer" style="flex: 4">
          <div class="no-btn" @click="noClick">Không</div>
          <div class="yes-btn" @click="yesClick">Có</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    codeDelete: { type: String, default: "" }, //Biến chứa code nhân viên muốn xoá
  },
  methods: {
    /**
     * Bắt sự kiện khi nhấn phím không
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    noClick() {
      this.$emit("hideReportLog");
    },
    /**
     * Bắt sự kiện khi nhấn phím có
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    yesClick() {
      this.$emit("deleteEmployee");
      this.$emit("hideReportLog");
    },
  },
};
</script>

<style scoped>
.mess-line {
  height: 1px;
  background: #b8bcc3;
  margin-bottom: 20px;
  width: 80%;
  /* justify-content: center; */
  margin: 0 auto 15px;
}
.text-edit-warning {
  font-size: 13px;
  margin: auto 0;
  color: rgb(17, 17, 17) !important;
  width: 300px;
  padding-left: 20px;
  font-weight: 100 !important;
}
.report-model {
  width: 100%;
  background: rgba(0, 0, 0, 0.4);
  position: fixed;
  left: 0;
  top: 0;
  height: 100%;
  z-index: 999;
  transition: all 0.25s ease;
  opacity: 1;
}
.report-content {
  display: flex;
  margin: 0 auto;
  flex: 6;
  /* display: flex; */
  padding: 0;
  vertical-align: middle;
}
.report-body {
  height: 200px;
  width: 450px;
  top: calc(50% - 100px);
  left: calc(50% - 200px);
  background-color: #fff;
  position: absolute;
  display: flex;
  flex-direction: column;
}
.icon-edit-warning {
  background-position: -592px -456px !important;
  width: 48px;
  height: 48px;
  min-width: 48px;
  min-height: 48px;
}
.report-footer .yes-btn {
  font-weight: 600;
  padding: 12px 35px;
  border-radius: 4px;
  display: inline-block;
  margin: auto;
  transition: all 0.25s ease;
  background-color: #2ca01c;
  color: #fff;
  position: absolute;
  left: 80%;
  transform: translateX(-50%);
  white-space: nowrap;
  font-size: 13px;
  line-height: 10px;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}
.report-footer .yes-btn:hover {
  background-color: #35bf22;
  color: #fff;
}
.report-footer .no-btn {
  font-weight: 600;
  padding: 12px 25px;
  border-radius: 4px;
  display: inline-block;
  margin: auto;
  transition: all 0.25s ease;
  background-color: #ffffff;
  /* color: #fff; */
  position: absolute;
  left: 20%;
  transform: translateX(-50%);
  white-space: nowrap;
  font-size: 13px;
  line-height: 10px;
  justify-content: center;
  align-items: center;
  border: 1px solid #8d9096;
  cursor: pointer;
}
.report-footer .no-btn:hover {
  background-color: #d2d3d6;
}
</style>
