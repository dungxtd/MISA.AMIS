<template>
  <div class="toast-wrapper">
    <div class="icon toast-icon"></div>
    <div class="toast">
      {{ msgStatus }}
    </div>
  </div>
</template>
<script>
export default {
  props: {
    msgStatus: { type: String, default: "" }, //Biến chứa câu thông báo
  },
};
</script>
<style>
.toast-wrapper {
  position: fixed;
  min-width: 230px;
  top: 20px;
  right: 30px;
  z-index: 999999;
  display: flex;
  padding: 10px;
  color: black;
  background: #ffffff;
  border-radius: 4px;
  box-shadow: 1px 3px 5px rgb(0 0 0 / 20%);
  max-width: 500px;
}
.toast {
  margin: auto 0;
  padding-left: 10px;
}
.toast-icon {
  background-position: -988px -460px !important;
  width: 40px;
  height: 40px;
}
</style>