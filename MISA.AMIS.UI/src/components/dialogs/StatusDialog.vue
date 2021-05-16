<template>
  <div>
    <div class="log-model">
      <div class="log-body">
        <div class="log-content">
          <div
            class="icon"
            :class="[
              { 'icon-success': statusLog },
              { 'icon-warning': !statusLog },
            ]"
            style="margin: auto 0"
          ></div>
          <div
            class="text-warning"
            style="font-size: 13px; margin: auto; color: #111 !important"
          >
            {{ mesStatus }}
          </div>
        </div>
        <div class="mess-line"></div>
        <div class="log-footer" style="flex: 3 1 0%">
          <div @click="cancelLog">Đóng</div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    mesStatus: { type: String, default: "" },
    statusLog: { type: Boolean, default: false },
  },
  methods: {
    cancelLog() {
      this.$emit("hideStatusLog");
    },
  },
};
</script>
<style scope>
.mess-line {
  height: 1px;
  background: #b8bcc3;
  margin-bottom: 20px;
  width: 80%;
  /* justify-content: center; */
  margin: 0 auto 15px;
}
.log-model {
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
.log-content {
  display: flex;
  margin: auto;
  flex: 6;
  /* display: flex; */
  padding: 0;
  vertical-align: middle;
}
.log-body {
  height: 200px;
  width: 400px;
  top: calc(50% - 100px);
  left: calc(50% - 200px);
  background-color: #fff;
  position: absolute;
  display: flex;
  flex-direction: column;
}
.icon-warning {
  background-position: -24px -954px !important;
  width: 48px;
  height: 48px;
  min-width: 48px;
  min-height: 48px;
}
.icon-success {
  background-position: -89px -954px !important;
  width: 48px;
  height: 48px;
  min-width: 48px;
  min-height: 48px;
}
.log-footer div {
  font-weight: 600;
  padding: 15px 40px;
  border-radius: 4px;
  display: inline-block;
  margin: auto;
  transition: all 0.25s ease;
  background-color: #35bf22;
  color: #fff;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}
</style>
