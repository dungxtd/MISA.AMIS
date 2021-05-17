<template>
  <div id="EmployeeDetail" class="dialog">
    <div class="detail" title="Thông tin nhân viên">
      <div class="dialog-model"></div>
      <div class="dialog-content">
        <div class="dialog-header">
          <div class="left-header">
            Thông tin nhân viên
            <div class="customer">
              <input type="checkbox" />
              <div class="tit">Là khách hàng</div>
            </div>
            <div class="provider">
              <input type="checkbox" />
              <div class="tit">Là nhà cung cấp</div>
            </div>
          </div>
          <div class="right-header">
            <div class="icon icon-24 icon-help"></div>
            <div
              class="icon icon-24 icon-cancel"
              @click="hideDetailPage()"
            ></div>
          </div>
        </div>
        <div class="dialog-body">
          <div class="flex first-inf">
            <div class="flex-1 left-f-inf">
              <div class="flex">
                <div class="flex flex-1">
                  <div style="position: relative">
                    <div class="label">
                      Mã
                      <div style="color: red; display: inline">*</div>
                    </div>
                    <input
                      type="text"
                      v-model="employee.employeeCode"
                      style="width: 95%"
                      ref="employeeCode"
                      v-bind:class="{
                        error:
                          formValidate(employee.employeeCode) && codeBlured,
                      }"
                      v-on:blur="codeBlured = true"
                    />
                    <div class="warning-text">Mã không được để trống.</div>
                  </div>
                  <div class="flex-1" style="position: relative">
                    <div class="label">
                      Tên
                      <div style="color: red; display: inline">*</div>
                    </div>
                    <input
                      type="text"
                      style="width: 83%"
                      v-model="employee.employeeName"
                      v-bind:class="{
                        error:
                          formValidate(employee.employeeName) && nameBlured,
                      }"
                      v-on:blur="nameBlured = true"
                    />
                    <div class="warning-text">Tên không được để trống.</div>
                  </div>
                </div>
                <div class="flex flex-1">
                  <div class="mgr10 flex-1">
                    <div class="label">Ngày sinh</div>
                    <input type="date" v-model="employee.dateOfBirth" />
                  </div>
                  <div class="flex-1">
                    <div class="label">Giới tính</div>
                    <div class="flex options">
                      <input
                        type="radio"
                        id="male"
                        name="gender"
                        value="0"
                        v-model="employee.gender"
                      />
                      <label for="male" class="margin0">Nam</label>
                      <input
                        type="radio"
                        id="female"
                        name="gender"
                        value="1"
                        v-model="employee.gender"
                      />
                      <label for="female" class="margin0">Nữ</label>
                      <input
                        type="radio"
                        id="other"
                        name="gender"
                        value="2"
                        v-model="employee.gender"
                      />
                      <label for="other" class="margin0">Khác</label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="flex">
                <div class="flex-1" style="position: relative">
                  <div class="label">
                    Đơn vị
                    <div style="color: red; display: inline">*</div>
                  </div>
                  <ComboBox
                    @setPssBlured="setPssBlured"
                    @setDepartmentName="setDepartmentName"
                    :pssBlured="pssBlured"
                    :departmentName="employee.departmentName"
                  />
                </div>
                <div class="flex flex-1">
                  <div class="mgr10 flex-1">
                    <div class="label">Số CMND</div>
                    <input type="text" v-model="employee.identityNumber" />
                  </div>
                  <div class="flex-1">
                    <div class="label">Ngày cấp</div>
                    <input
                      type="date"
                      style="width: 90%"
                      v-model="employee.identityDate"
                    />
                  </div>
                </div>
              </div>
              <div class="flex">
                <div class="flex-1">
                  <div class="label">Chức danh</div>
                  <input
                    type="text"
                    style="width: 90%"
                    v-model="employee.employeePosition"
                  />
                </div>
                <div class="flex-1">
                  <div class="label">Nơi cấp</div>
                  <input
                    type="text"
                    class="width95"
                    v-model="employee.identityPlace"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="second-inf">
            <div>
              <div class="label">Địa chỉ</div>
              <input type="text" class="width97" v-model="employee.address" />
            </div>
            <div class="flex">
              <div class="mgr10">
                <div class="label">ĐT di động</div>
                <input type="text" v-model="employee.phoneNumber" />
              </div>
              <div class="mgr10">
                <div class="label">ĐT cố định</div>
                <input type="text" v-model="employee.telephoneNumber" />
              </div>
              <div class="mgr10">
                <div class="label">Email</div>
                <input type="text" v-model="employee.email" />
              </div>
            </div>
            <div class="flex">
              <div class="mgr10">
                <div class="label">Tài khoản ngân hàng</div>
                <input type="text" v-model="employee.bankAccountNumber" />
              </div>
              <div class="mgr10">
                <div class="label">Tên ngân hàng</div>
                <input type="text" v-model="employee.bankName" />
              </div>
              <div class="mgr10">
                <div class="label">Chi nhánh</div>
                <input type="text" v-model="employee.bankBranchName" />
              </div>
            </div>
          </div>
        </div>
        <div class="divide"></div>
        <div class="dialog-footer">
          <div class="cancel-botton" @click="hideDetailPage()">Huỷ</div>
          <div class="flex">
            <div class="accept-btn1" @click="btnAddEdit">Cất</div>
            <div class="accept-btn2" @click="btnAddMore">Cất và Thêm</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import ComboBox from "./Combobox";
export default {
  components: { ComboBox },
  created() {
    this.$nextTick(() => this.$refs.employeeCode.focus());
    this.employee = { ...this.employeeProp };
  },
  props: {
    employeeProp: { type: Object, default: Object.create(null) },
    employeeBackUp: { type: Object, default: Object.create(null) },
    formMode: { type: String, default: "" },
    inputFocus: { type: Boolean, default: false },
  },
  data() {
    return {
      employee: {},
      codeBlured: false, //Blur ra khỏi ô input code
      nameBlured: false, //Blur ra khỏi ô input name
      pssBlured: false, //Blur ra khỏi ô input đơn vị
    };
  },
  methods: {
    //Ham an vao nut thoat
    hideDetailPage() {
      this.$emit("hideDetailPage");
    },
    //CreatedBy: TDDUUNG
    //Date : 11/5/2021
    //Ham an vao nut xong
    btnAddEdit() {
      this.codeBlured = true;
      this.nameBlured = true;
      this.pssBlured = true;
      this.employee;
      this.addEmployee();
    },
    /**
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    //Goi ipa them nhan vien
    async addEmployee() {
      var aipUrl = "https://localhost:44368/api/v1/Employees/";
      if (this.formMode == "add") {
        await axios
          .post(aipUrl, this.employee)
          .then((res) => {
            console.log(res);
            if (res.status == 200) {
              this.hideDetailPage();
              console.log(res);
            }
          })
          .catch((err) => {
            console.log(err);
            var errMsg = err.response.data.errMsg.split("/");
            if (errMsg[0] == 1) this.$emit("showStatusLog", errMsg[1]);
            else if (errMsg[0] == 0) this.$emit("showWarningLog", errMsg[1]);
          });
      }
      //CreatedBy: TDDUUNG
      //Date : 11/5/2021
      // Goi api sua nhan vien
      else {
        await axios
          .put(aipUrl, this.employee)
          .then((res) => {
            console.log(res.data);
            if (res.status == 200) {
              this.hideDetailPage();
              console.log(res);
            }
          })
          .catch((err) => {
            console.log(err.response);
            var errMsg = err.response.data.errMsg.split("/");
            if (errMsg[0] == 1) this.$emit("showStatusLog", errMsg[1]);
            else if (errMsg[0] == 0) this.$emit("showWarningLog", errMsg[1]);
          });
      }
    },
    /**
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    //Goi ipa them nhan vien
    async addMoreEmployee() {
      var aipUrl = "https://localhost:44368/api/v1/Employees/";
      if (this.formMode == "add") {
        await axios
          .post(aipUrl, this.employee)
          .then((res) => {
            console.log(res);
            if (res.status == 200) {
              this.resetForm();
              console.log(res);
            }
          })
          .catch((err) => {
            console.log(err);
            var errMsg = err.response.data.errMsg.split("/");
            if (errMsg[0] == 1) this.$emit("showStatusLog", errMsg[1]);
            else if (errMsg[0] == 0) this.$emit("showWarningLog", errMsg[1]);
          });
      } else {
        await axios
          .put(aipUrl, this.employee)
          .then((res) => {
            console.log(res.data);
            if (res.status == 200) {
              this.resetForm();
              console.log(res);
            }
          })
          .catch((err) => {
            console.log(err.response);
            var errMsg = err.response.data.errMsg.split("/");
            if (errMsg[0] == 1) this.$emit("showStatusLog", errMsg[1]);
            else if (errMsg[0] == 0) this.$emit("showWarningLog", errMsg[1]);
          });
      }
    },
    btnAddMore() {
      this.addMoreEmployee();
      this.$nextTick(() => this.$refs.employeeCode.focus());
    },
    async resetForm() {
      this.employee = { ...this.employeeBackUp };
      this.codeBlured = false;
      this.nameBlured = false;
      this.pssBlured = false;
      this.$emit("setFormModeAdd");
      var aipUrl = "https://localhost:44368/api/v1/Employees/getMaxCode";
      await axios
        .get(aipUrl)
        .then((res) => {
          var temp = res.data[0].split("-");
          this.employee.employeeCode =
            temp[0] + "-" + (parseInt(temp[1]) + 1).toString();
          this.employee.employeeCode;
          console.log(this.employee.employeeCode);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    /**
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     * Hàm check thông tin ở client
     */
    formValidate(value) {
      if (value == "" || value == null) return true;
      else return false;
    },
    setPssBlured(value) {
      this.pssBlured = value;
    },
    setDepartmentName(value) {
      this.employee.departmentName = value;
      switch (value) {
        case "Phòng Nhân sự":
          this.employee.departmentId = "11452b0c-768e-5ff7-0d63-eeb1d8ed8cef";
          break;
        case "Phòng Kế toán":
          this.employee.departmentId = "142cb08f-7c31-21fa-8e90-67245e8b283e";
          break;
        case "Phòng Đào tạo":
          this.employee.departmentId = "17120d02-6ab5-3e43-18cb-66948daf6128";
          break;
        case "Phòng Marketing":
          this.employee.departmentId = "469b3ece-744a-45d5-957d-e8c757976496";
          break;
        case "Phòng Nghiên cứu":
          this.employee.departmentId = "4e272fc4-7875-78d6-7d32-6a1673ffca7c";
          break;
      }
    },
  },
  //CreatedBy: TDDUUNG
  //Date : 11/5/2021

  watch: {},
  computed: {},
};
</script>

<style scoped>
.cancel-botton:hover,
.accept-btn1:hover,
.accept-btn2:hover {
  background-color: #babec5;
}
input[type="text"],
select[type="text"],
input[type="date"] {
  padding: 6px 10px;
  font-size: 13px;
  height: 32px;
  color: inherit;
  position: relative;
  padding: 6px 10px;
  border-radius: 2px;
  border: 1px solid #babec5;
  box-sizing: border-box;
  width: 100%;
  outline: none;
}
input[type="radio"] {
  margin: 5px;
  padding: 0px !important;
  width: 20px;
  height: 20px;
}
label {
  margin-bottom: 0;
}
.dialog-hide {
  display: none;
}
.options {
  justify-content: space-around;
  padding-right: 83px;
}
.margin0 {
  margin: auto 0;
}
.error {
  border: 1px solid red !important;
}
.warning-text {
  position: absolute;
  display: none;
  top: 50%;
  left: 50%;
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
input:focus {
  border: 1px solid #2ca01c;
}
select:focus {
  border: 1px solid #2ca01c;
}
input[type="radio"] {
  width: 18px;
  height: 18px;
  appearance: none;
  -webkit-appearance: none;
  border-radius: 50%;
  background: white;
  border: 1px solid #afafaf;
  display: flex;
  align-items: center;
  justify-content: center;
}

input:checked[type="radio"] {
  border: 1px solid #2ca01c;
}

input[type="radio"]::before {
  content: "";
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background: #2ca01c;
  display: none;
}

input:checked[type="radio"]::before {
  display: block;
}
</style>
