<template>
  <div class="header">
    <div class="header-left">
      <div class="icon icon-24 header-icon"></div>
      <div class="header-branch">
        <div class="header-branch-name">CÔNG TY CỔ PHẦN MISA</div>
        <div class="cover-icon">
          <div class="icon icon-24 header-branch-icon"></div>
        </div>
      </div>
    </div>
    <div class="header-right">
      <div class="filter-input">
        <input
          type="text"
          placeholder="Nhập từ khóa tìm kiếm"
          class="filter-input-label"
        />
        <div class="icon icon-24 filter-input-icon"></div>
      </div>
      <div class="option-cover bell-cover">
        <div class="icon icon-24 bell-icon"></div>
      </div>

      <div class="cover-user">
        <div class="user-logo">
          <img
            src="https://id.misa.com.vn/image/getavatar?userId=c5b890cb-e9a5-4966-9e2c-2aa6813eaf70&width=32&height=32"
          />
        </div>
        <div class="user-name">Tạ Đình Dũng</div>
        <div class="icon icon-24 extend-icon"></div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "Header",
};
</script>
<style>
*,
::after,
::before {
  box-sizing: content-box;
}
</style>
