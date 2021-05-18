<template>
  <div>
    <div class="datachanged-model">
      <div class="datachanged-body">
        <div class="datachanged-content">
          <div
            class="icon icon-edit-datachanged"
            style="margin: auto 0; margin-right: 10px"
          ></div>
          <div class="text-edit-datachanged">
            Dữ liệu đã bị thay đổi. Bạn có muốn cất không ?
          </div>
        </div>
        <div class="mess-line"></div>
        <div class="datachanged-footer" style="flex: 3 1 0%; display: flex">
          <div class="cancel-btn" @click="clickCancel">Huỷ</div>
          <div class="right-footer">
            <div class="no-btn" @click="clickNo">Không</div>
            <div class="yes-btn" @click="clickYes">Có</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {},
  methods: {
    /**
     * Hàm ấn vào nút cancel-btn
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    clickCancel() {
      this.$emit("hideDataChangedDialog");
    },
    /**
     * Hàm ấn vào nút có
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    clickNo() {
      this.$emit("hideDetailPage");
    },
    /**
     * Hàm ấn vào nút không
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    clickYes() {
      this.$emit("saveDataChanged");
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
.text-edit-datachanged {
  font-size: 13px;
  margin: auto 0;
  color: rgb(17, 17, 17) !important;
  width: 300px;
  /* padding-left: 20px; */
  font-weight: 100 !important;
}
.datachanged-model {
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
.datachanged-content {
  display: flex;
  margin: 0 45px;
  flex: 5;
  /* display: flex; */
  padding: 0;
  vertical-align: middle;
}
.datachanged-body {
  height: 200px;
  width: 450px;
  top: calc(50% - 100px);
  left: calc(50% - 225px);
  background-color: #fff;
  position: absolute;
  display: flex;
  flex-direction: column;
}
.icon-edit-datachanged {
  background-position: -826px -456px;
  width: 48px;
  height: 48px;
  min-width: 48px;
  min-height: 48px;
}
.datachanged-footer .yes-btn {
  font-weight: 600;
  padding: 13px 21px;
  border-radius: 3px;
  display: inline-block;
  margin: auto;
  transition: all 0.25s ease;
  background-color: #2ca01c;
  color: #fff;
  position: absolute;
  right: 0;
  transform: translateX(-50%);
  white-space: nowrap;
  font-size: 13px;
  line-height: 10px;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}
.datachanged-footer .no-btn {
  font-weight: 600;
  padding: 12px 21px;
  border-radius: 3px;
  display: inline-block;
  margin: auto;
  transition: all 0.25s ease;
  background-color: #ffffff;
  /* color: #fff; */
  position: absolute;
  right: 12%;
  transform: translateX(-50%);
  white-space: nowrap;
  font-size: 13px;
  line-height: 10px;
  justify-content: center;
  align-items: center;
  border: 1px solid #8d9096;
  cursor: pointer;
}
.no-btn:hover {
  background-color: #d2d3d6;
}
.cancel-btn:hover {
  background-color: #d2d3d6;
}
.cancel-btn {
  font-weight: 600;
  padding: 12px 21px;
  border-radius: 3px;
  display: inline-block;
  margin: auto;
  transition: all 0.25s ease;
  background-color: #ffffff;
  color: #111;
  position: absolute;
  left: 0;
  transform: translateX(50%);
  white-space: nowrap;
  font-size: 13px;
  line-height: 10px;
  justify-content: center;
  align-items: center;
  border: 1px solid #8d9096;
  cursor: pointer;
}

.right-footer {
}
</style>