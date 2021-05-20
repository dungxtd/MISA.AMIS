<template>
  <div class="custom-select" :tabindex="tabindex" @blur="open = false">
    <div class="items" :class="{ selectHide: !open }">
      <div
        v-for="(option, i) of options"
        :key="i"
        @click="
          selected = option;
          open = false;
          $emit('input', option);
        "
        :class="{ 'item-select': option == selected }"
      >
        {{ option }}
      </div>
    </div>
    <div class="wrap-header" @click="open = !open" :class="{ open: open }">
      <input
        class="input-selected selected"
        v-model="selected"
        READONLY
        @keyup.up="keyup"
        @keyup.down="keydown"
        @keyup.enter="enter"
      />
      <div class="box-action">
        <div class="wrap-action">
          <div class="icon icon-24 box-icon"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    options: { type: Array, required: true }, // Biến chứa mảng sau khi đã lọc
    default: { type: String, required: false, default: null }, // Biến mặc định cho ô input
    tabindex: { type: Number, required: false, default: 0 }, //Biến tabinex
    pageSize: { type: Number, required: false, default: 0 }, // Biến hiển thị size page
  },
  methods: {
    keyup() {
      if (this.pageSize > 10)
        this.selected = this.pageSize - 10 + " bản ghi trên 1 trang";
    },
    keydown() {
      if (this.pageSize < 40)
        this.selected = this.pageSize + 10 + " bản ghi trên 1 trang";
    },
    enter() {
      this.open = false;
    },
  },
  data() {
    return {
      selected: this.default // Biến chỉ ô đã chọn
        ? this.default
        : this.options.length > 0
        ? this.options[0]
        : null,
      open: false, //Biến chỉ trạng thái cửa sổ options
      numberSelected: 20, //Biến chỉ số bản ghi trên 1 trang đã chọn
    };
  },
  mounted() {},
  watch: {
    // Theo dõi biến page size
    selected() {
      this.numberSelected = parseInt(this.selected.split(" ")[0]);
      this.$emit("setPageSize", this.numberSelected);
    },
    pageSize() {
      this.selected = this.pageSize + " bản ghi trên 1 trang";
    },
  },
};
</script>

<style scoped>
.custom-select {
  position: relative;
  max-width: 200px;
  text-align: left;
  outline: none;
  height: 30px;
  line-height: 30px;
}
.wrap-header {
  display: flex;
  border: 1px solid #babec5;
}
.custom-select .selected {
  background-color: white;
  border-radius: 2px;
  /* border: 1px solid #babec5; */
  color: black;
  flex: 1;
  padding-left: 1em;
  cursor: pointer;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

.open {
  border: 1px solid #2ca01c !important;
}

.custom-select .items {
  border-radius: 2px 2px 0px 0px;
  overflow: hidden;
  border-right: 1px solid #babec5;
  border-left: 1px solid #babec5;
  border-top: 1px solid #babec5;
  position: absolute;
  background-color: #fff;
  left: 0;
  right: 0;
  bottom: 31px;
  z-index: 1;
}

.custom-select .items div {
  color: #0a0a0a;
  padding-left: 1em;
  cursor: pointer;
  user-select: none;
}

.custom-select .items div:hover {
  background-color: #e0e0e0;
}

.selectHide {
  display: none;
}
.item-select {
  background-color: #2ca01c !important;
}
.box-action {
  display: flex;
  background-color: #ffffff;
  /* border-left: 1px solid #babec5; */
}
.box-action:hover {
  background-color: #e0e0e0;
  border-color: #e0e0e0;
}
.wrap-action {
  width: 32px;
  background-color: transparent;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}
.box-icon {
  margin-right: 0;
  transform: rotate(0deg);
  transition: transform 0.15s linear;
  background-position: -556px -355px;
}
.input-selected {
  border: 0 !important;
}
.input-selected:hover {
  border: 0 !important;
}
</style>
