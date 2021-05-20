<template>
  <div class="combobox">
    <div class="header-combobox" v-click-outside="clickOutSide">
      <input
        type="text"
        autocomplete="off"
        v-model="option"
        @input="filterOptions()"
        @focus="focusInput"
        @keyup.up="keyup"
        @keyup.down="keydown"
        @keyup.enter="enter"
        @keydown.tab="keyout"
        @keydown="model = true"
        @click="model = !model"
        @blur="checkValue"
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
    },
    /**
     * Bắt sự kiện khi focus vào ô input
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    focusInput() {
      // this.model = true;
      this.filterOptions();
    },
    /**
     * Bắt sự kiện khi click bên ngoài
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    clickOutSide() {
      this.model = false;
      // this.checkValue();
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
        return options.toLowerCase().match(this.option.toLowerCase());
      });
    },
    /**
     * Set giá trị cho ô input khi click
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    setOption(option) {
      this.isValided = false;
      this.option = option;
      this.model = false;
      this.filteredOptions = this.options;
      // this.checkValue();
      // this.isValided = false;
      this.searchIdByName();
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
      this.$emit("setPssBlured", true);
      if (this.indexSelect >= 0)
        this.option = this.filteredOptions[this.indexSelect];
      this.model = false;
      this.filteredOptions = this.options;
      this.indexSelect = -1;
      this.searchIdByName();
    },
    /**
     * Check giá trị của ô input có phải đúng phòng ban hay Không
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    checkValue() {
      // this.$emit("setPssBlured", true);
      this.isChecked = false;
      this.options.forEach((element) => {
        if (this.option == element) {
          this.isChecked = true;
          this.isValided = false;
          this.filteredOptions = this.options;
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
    },
    /**
     * Lấy id của phòng ban theo tên
     * CrearedBy: TDDUNG
     * Date: 11/5/2021
     */
    searchIdByName() {
      this.departments.find((o) => {
        if (o.departmentName === this.option)
          this.$emit("setDepartmentName", o.departmentId, o.departmentName);
      });
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
    pssBlured() {
      this.checkValue();
    },
    model() {
      if (!this.model) this.$emit("setPssBlured", true);
    },
  },
  mounted() {
    // this.filterOptions();
  },
  computed: {},
};
</script>

<style >
@import "../../assets/css/dialogs/combobox.css";
</style>
