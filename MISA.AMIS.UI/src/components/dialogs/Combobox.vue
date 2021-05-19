<template>
  <div class="combobox">
    <div class="header-combobox" v-click-outside="clickOutSide">
      <input
        type="text"
        autocomplete="off"
        v-model="option"
        @input="filterOptions"
        @focus="focusInput"
        @keyup.up="keyup"
        @keyup.down="keydown"
        @keyup.enter="enter"
        @keydown.tab="keyout"
        v-on:blur="blurInput()"
        @click="model = !model"
        v-bind:class="{ error: isValided && pssBlured }"
      />

      <div class="warning-text">Vị trí không được để trống.</div>
      <div class="icon-combobox" @click="model = !model"></div>
    </div>

    <div v-if="filteredOptions && model" class="main-cbb">
      <ul class="wrap-option">
        <div
          class="option-combobox"
          v-for="(filteredOption, index) in filteredOptions"
          v-on:click="setOption(filteredOption)"
          :key="index"
          :class="{ select: index == indexSelect }"
        >
          {{ filteredOption }}
        </div>
      </ul>
    </div>
  </div>
</template>
<script>
import Vue from "vue";
import vClickOutside from "v-click-outside";
import axios from "axios";
Vue.use(vClickOutside);
export default {
  props: {
    pssBlured: { type: Boolean, default: false }, // Biến khi plur khỏi ô input
    departmentName: { type: String, default: "" }, //Biến tên phòng ban truyền lên
  },
  data() {
    return {
      departments: {}, //Biến chứa tất cả phòng ban gọi về từ api
      options: [], //Biến chứa tất cả tên phòng ban
      option: "", //Biến chỉ option đã chọn
      model: false, // Biến chỉ trạng thái dialog
      filteredOptions: [], //Biến chứa tên phòng ban sau khi lọc
      indexSelect: -1, //Biến chỉ index đầu tiền khi ấn từ bàn phím
      isChecked: false, //Biến chỉ đã check trống hay chưa
      isValided: false, //Biến chỉ đã check validate hay chưa
    };
  },
  created() {
    // Gọi lấy mảng departments khi created
    this.getDepartments();
  },
  methods: {
    /**
     * Hàm gọi API lấy mảng departments
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    async getDepartments() {
      if (this.departmentName != null || this.departmentName != "") {
        this.option = this.departmentName;
      }
      var aipUrl = "https://localhost:44368/api/v1/Departments";
      await axios
        .get(aipUrl)
        .then((res) => {
          this.departments = res.data;
        })
        .catch((err) => {
          console.log(err);
        });
      this.options = this.departments.map(function (obj) {
        return obj.departmentName;
      });
      this.filteredOptions = this.options;
    },
    /**
     * Bắt sự kiện khi phím được tha lên
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    keyout() {
      this.checkValue();
      this.model = false;
    },
    /**
     * Bắt sự kiện khi focus vào ô input
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    focusInput() {
      this.filterOptions();
      // this.model = !this.model;
    },
    /**
     * Bắt sự kiện khi click bên ngoài
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    clickOutSide() {
      this.model = false;
      this.checkValue();
    },
    /**
     * Hàm lọc giá trị input với mảng departments
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    filterOptions() {
      if (this.option == "") {
        this.filteredOptions = this.options;
      }
      this.filteredOptions = this.options.filter((options) => {
        // return options.toLowerCase().startsWith(this.option.toLowerCase());
        return options.toLowerCase().match(this.option.toLowerCase());
      });
    },
    /**
     * Set giá trị cho ô input khi click
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    setOption(option) {
      // this.isValided = false;
      this.option = option;
      this.model = false;
      this.filterOptions();
      this.checkValue();
      // this.isValided = false;
      this.$emit("setDepartmentName", this.option);
    },
    /**
     * Bắt sự kiện khi nhấn phím xuống
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    keydown() {
      this.model = true;
      if (this.indexSelect < this.filteredOptions.length - 1)
        this.indexSelect++;
      else this.indexSelect = 0;
    },
    /**
     * Bắt sự kiện khi nhấn phím lên
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    keyup() {
      if (this.indexSelect > 0) this.indexSelect--;
    },
    /**
     * Bắt sự kiện khi nhấn phím enter
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    enter() {
      // console.log(this.indexSelect);
      if (this.indexSelect >= 0)
        this.option = this.filteredOptions[this.indexSelect];
      this.checkValue();
      this.filterOptions();
      this.indexSelect = -1;
      this.model = false;
    },
    blurInput() {
      // this.checkValue();
    },
    /**
     * Check giá trị của ô input có phải đúng phòng ban hay Không
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    checkValue() {
      this.$emit("setPssBlured", true);
      this.isChecked = false;
      this.options.forEach((element) => {
        if (this.option == element) {
          this.isChecked = true;
          this.isValided = false;
        }
      });
      if (!this.isChecked) {
        this.option = "";
        this.isValided = true;
      }
      if (this.option == "") {
        this.isValided = true;
        // this.setOption("");
      }
      this.model = false;
    },
  },
  watch: {
    // Theo dõi biến tên phòng ban
    departmentName() {
      this.option = this.departmentName;
    },
    // Theo dõi biến ô input
    option() {
      if (this.option == "") this.$emit("setDepartmentName", this.option);
    },
    // pssBlured() {
    //   if (this.pssBlured) this.checkValue();
    // },
  },
  mounted() {
    this.filterOptions();
  },
  computed: {},
};
</script>

<style >
.icon-combobox {
  width: 32px;
  background-color: transparent;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  background-position: -553px -354px !important;
  border-right: 1px solid #babec5;
  border-top: 1px solid #babec5;
  border-bottom: 1px solid #babec5;
  background: url(/img/Sprites.5f05e81f.svg) no-repeat;
}
input {
  border: none;
  font-size: 13px;
  padding: 5px 10px;
  padding-right: unset;
  text-overflow: ellipsis;
  background-color: transparent;
  width: 100%;
  background: white;
  color: black !important;
  border: 1px solid #babec5;
}
input:focus-visible {
  outline: none;
  border: 1px solid #2ca01c;
}
.warning-combox {
  border: 1px solid red !important;
}
.combobox {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 90%;
}
.header-combobox {
  display: flex;
  z-index: 10;
  width: 100%;
}
.wrap-option {
  position: absolute;
  width: 90%;
  background-color: rgb(255, 255, 255);
  cursor: pointer;
  padding: 0;
  margin: 0;
}
.main-cbb {
  z-index: 10;
  width: 100%;
}
.select {
  color: #ebedf0 !important;
  background-color: #35bf22 !important;
}
.option-combobox {
  border-right: 1px solid #babec5;
  border-bottom: 1px solid #babec5;
  border-left: 1px solid #babec5;
}
.option-combobox:hover {
  color: #35bf22;
  background-color: #ebedf0;
}
ul div {
  text-align: left !important;
  padding: 5px 0px 5px 11px;
  border: 1px solid #ebedf0;
}
.error {
  border: 1px solid red !important;
}
.warning-text {
  position: absolute;
  display: none;
  top: 30%;
  left: 60%;
  transform: translate(-50% -50%);
  font-size: 10px;
  z-index: 20;
  width: fit-content;
  height: 16px;
  text-align: center;
  background: rgb(49, 49, 49);
  padding: 0 5px;
  border: 1px solid #111;
  color: #fff;
}
.error:hover + .warning-text {
  display: block;
}
</style>
