<template>
  <div class="dialog">
    <div class="dialog-model"></div>
    <div class="detail-dialog-content">
      <div class="detail-dialog-header">
        <div class="detail-header-title">
          <div class="wrap-header-title">
            <div class="header-title-text">Thông tin nhân viên</div>
            <div class="header-title-customer">
              <input type="checkbox" class="customer-checkbox" />
              <div class="customer-content">Là khách hàng</div>
            </div>
            <div class="header-title-customer">
              <input type="checkbox" class="customer-checkbox" />
              <div class="customer-content">Là nhà cung cấp</div>
            </div>
          </div>
        </div>
        <div class="detail-header-close">
          <div class="icon icon-24 detail-icon-help"></div>
          <div
            class="icon icon-24 detail-icon-close"
            @click="cancelDetailPage()"
          ></div>
        </div>
      </div>
      <div class="detail-dialog-main">
        <div class="layer-detail-inf">
          <div class="dflex">
            <div style="padding-right: 26px; width: 50% !important">
              <div class="dflex">
                <div style="padding-right: 6px; width: 40% !important">
                  <div class="dflex">
                    <div class="input-title">Mã</div>
                    <div class="input-title-require">*</div>
                  </div>
                  <div class="detail-inf-label">
                    <input
                      type="text"
                      maxlength="20"
                      class="detail-inf-input"
                      v-model="employee.employeeCode"
                      ref="employeeCode"
                      v-bind:class="{
                        error:
                          formValidate(employee.employeeCode) && codeBlured,
                      }"
                      v-on:blur="codeBlured = true"
                    />
                    <div class="warning-text">Mã không được để trống.</div>
                  </div>
                </div>
                <div style="width: 60% !important">
                  <div style="position: relative; width: 100%">
                    <div class="dflex">
                      <div class="input-title">Tên</div>
                      <div class="input-title-require">&nbsp;*</div>
                    </div>
                    <div class="detail-inf-label">
                      <input
                        type="text"
                        maxlength="100"
                        class="detail-inf-input"
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
                </div>
              </div>
              <div class="detail-inf-lable">
                <div class="w-100">
                  <div class="dflex">
                    <div class="input-title">Đơn vị</div>
                    <div class="input-title-require">&nbsp;*</div>
                  </div>
                  <div class="detail-inf-label">
                    <ComboBox
                      @setPssBlured="setPssBlured"
                      @setDepartmentName="setDepartmentName"
                      :pssBlured="pssBlured"
                      :departmentName="employee.departmentName"
                    />
                  </div>
                </div>
              </div>
              <div class="detail-inf-lable">
                <div class="w-100">
                  <div class="w-100">
                    <div class="dflex">
                      <div class="input-title">Chức danh</div>
                    </div>
                    <div class="detail-inf-label">
                      <input
                        maxlength="225"
                        type="text"
                        class="detail-inf-input"
                        v-model="employee.employeePosition"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div style="width: 50% !important">
              <div class="dflex">
                <div style="padding-right: 6px; width: 40% !important">
                  <div class="detail-inf-lable">
                    <div class="dflex">
                      <div class="input-title">Ngày sinh</div>
                    </div>
                    <div class="detail-inf-label">
                      <input
                        type="date"
                        :max="today"
                        class="detail-inf-input"
                        v-model="employee.dateOfBirth"
                      />
                    </div>
                  </div>
                </div>
                <div style="padding-right: 6px; width: 60% !important">
                  <div class="detail-inf-lable">
                    <div class="dflex">
                      <div class="input-title">Giới tính</div>
                    </div>
                    <div
                      class="detail-inf-label dflex"
                      style="justify-content: space-around; height: 32px"
                    >
                      <div class="dflex">
                        <input
                          type="radio"
                          name="gender"
                          class="checkboxGender"
                          value="0"
                          v-model="employee.gender"
                        />
                        <label class="lable-input-radio">Nam</label>
                      </div>
                      <div class="dflex">
                        <input
                          type="radio"
                          name="gender"
                          class="checkboxGender"
                          value="1"
                          v-model="employee.gender"
                        />
                        <label class="lable-input-radio">Nữ</label>
                      </div>
                      <div class="dflex">
                        <input
                          type="radio"
                          class="checkboxGender"
                          name="gender"
                          value="2"
                          v-model="employee.gender"
                        />
                        <label class="lable-input-radio">Khác</label>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div style="display: flex !important">
                <div style="padding-right: 6px; width: 60% !important">
                  <div class="detail-inf-lable">
                    <div class="dflex">
                      <div class="input-title">Số CMND</div>
                    </div>
                    <div class="detail-inf-label">
                      <input
                        maxlength="20"
                        type="text"
                        class="detail-inf-input"
                        v-model="employee.identityNumber"
                      />
                    </div>
                  </div>
                </div>
                <div style="padding-right: 6px; width: 40% !important">
                  <div class="detail-inf-lable">
                    <div class="dflex">
                      <div class="input-title">Ngày cấp</div>
                    </div>
                    <div class="detail-inf-label">
                      <input
                        type="date"
                        :max="today"
                        class="detail-inf-input"
                        v-model="employee.identityDate"
                      />
                    </div>
                  </div>
                </div>
                <div
                  style="padding-bottom: 12px; display: flex !important"
                ></div>
              </div>
              <div class="detail-inf-lable">
                <div class="w-100">
                  <div class="w-100">
                    <div class="dflex">
                      <div class="input-title">Nơi cấp</div>
                    </div>
                    <div class="detail-inf-label">
                      <input
                        maxlength="225"
                        type="text"
                        class="detail-inf-input"
                        v-model="employee.identityPlace"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="layer-detail-inf">
          <div class="w-100">
            <div class="dflex">
              <div class="input-title">Địa chỉ</div>
            </div>
            <div class="detail-inf-label">
              <input
                maxlength="20"
                type="text"
                class="detail-inf-input"
                v-model="employee.address"
              />
            </div>
          </div>
          <div class="w-100 dflex">
            <div class="pnumber">
              <div class="detail-inf-lable">
                <div class="dflex">
                  <div class="input-title">ĐT di động</div>
                </div>
                <div class="detail-inf-label">
                  <input
                    maxlength="20"
                    type="text"
                    class="detail-inf-input"
                    v-model="employee.phoneNumber"
                  />
                </div>
              </div>
            </div>
            <div class="pnumber">
              <div class="detail-inf-lable">
                <div class="dflex">
                  <div class="input-title">ĐT cố định</div>
                </div>
                <div class="detail-inf-label">
                  <input
                    maxlength="20"
                    type="text"
                    class="detail-inf-input"
                    v-model="employee.telephoneNumber"
                  />
                </div>
              </div>
            </div>
            <div class="pnumber">
              <div class="detail-inf-lable">
                <div class="dflex">
                  <div class="input-title">Email</div>
                </div>
                <div class="detail-inf-label">
                  <input
                    maxlength="50"
                    type="text"
                    class="detail-inf-input"
                    v-model="employee.email"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="w-100 dflex">
            <div class="pnumber">
              <div class="detail-inf-lable">
                <div class="dflex">
                  <div class="input-title">Tài khoản ngân hàng</div>
                </div>
                <div class="detail-inf-label">
                  <input
                    type="text"
                    maxlength="20"
                    class="detail-inf-input"
                    v-model="employee.bankAccountNumber"
                  />
                </div>
              </div>
            </div>
            <div class="pnumber">
              <div class="detail-inf-lable">
                <div class="dflex">
                  <div class="input-title">Tên ngân hàng</div>
                </div>
                <div class="detail-inf-label">
                  <input
                    maxlength="225"
                    type="text"
                    class="detail-inf-input"
                    v-model="employee.bankName"
                  />
                </div>
              </div>
            </div>
            <div class="pnumber">
              <div class="detail-inf-lable">
                <div class="dflex">
                  <div class="input-title">Chi nhánh</div>
                </div>
                <div class="detail-inf-label">
                  <input
                    type="text"
                    maxlength="225"
                    class="detail-inf-input"
                    v-model="employee.bankBranchName"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="divide-footer"></div>
        <div class="detail-dialog-footer">
          <div class="btn-footer">
            <div class="btn-footer-left">
              <button class="btn-detail" @click="hideDetailPage()">Huỷ</button>
            </div>
            <div class="btn-footer-right">
              <button class="btn-detail" @click="btnAddEdit">Cất</button>
              <button class="btn-detail btn-detail-yes" @click="btnAddMore">
                Cất và thêm
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <DataChangedDialog
      v-if="isShowDataChanged == true"
      @hideDataChangedDialog="hideDataChangedDialog"
      @hideDetailPage="hideDetailPage"
      @saveDataChanged="saveDataChanged"
    />
  </div>
</template>
<script>
import axios from "axios";
import ComboBox from "./Combobox";
import DataChangedDialog from "./DataChangedDialog";
export default {
  components: { ComboBox, DataChangedDialog },
  created() {
    //focus vào ô mã nhân viên

    if (this.formMode == "edit") this.getEmployeeById();
    if (this.formMode == "add") this.getMaxCode();
    if (this.formMode == "clone") {
      this.getEmployeeCodeMaxById();
      this.$emit("setFormModeAdd");
    }
    this.employeeBackUp = { ...this.employee };
    this.$nextTick(() => this.$refs.employeeCode.focus());
  },
  props: {
    employeeSelectId: { type: String, default: "" }, //Biến truyền Id nhân viên đã chọn lên
    formMode: { type: String, default: "" }, // biến chỉ kiểu cất là sửa hay thêm
  },
  data() {
    return {
      employee: {
        employeeCode: "",
        employeeName: "",
        departmentName: "",
        departmentId: "",
        employeePosition: "",
        dateOfBirth: null,
        gender: null,
        identityNumber: "",
        identityDate: null,
        identityPlace: "",
        address: "",
        telephoneNumber: "",
        phoneNumber: "",
        email: "",
        bankAccountNumber: "",
        bankName: "",
        bankBranchName: "",
      },
      employeeEqual: {},
      employeeBackUp: {},
      isShowDataChanged: false, //Biến hiển thị cửa sổ thồn báo data đã bị thay đổi
      codeBlured: false, //Blur ra khỏi ô input code
      nameBlured: false, //Blur ra khỏi ô input name
      pssBlured: false, //Blur ra khỏi ô input đơn vị
      today: new Date().toISOString().split("T")[0],
    };
  },
  methods: {
    /**
     * Hàm ấn vào nút cancel x ở trên
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    cancelDetailPage() {
      if (!this.deepEqual(this.employee, this.employeeEqual))
        this.isShowDataChanged = true;
      else this.hideDetailPage();
    },
    /**
     * Hàm gọi API lấy về thông tin nhân viên đã max code
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    getEmployeeCodeMaxById() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees/employeeMaxCode/" +
        this.employeeSelectId;
      axios
        .get(aipUrl)
        .then((res) => {
          this.employee = res.data;
          this.employee.dateOfBirth = this.filterDate(
            this.employee.dateOfBirth
          );
          this.employee.identityDate = this.filterDate(
            this.employee.identityDate
          );
          this.employeeEqual = { ...this.employee };
        })
        .catch((err) => {
          console.log(err);
        });
    },
    /**
     * Hàm gọi API lấy về thông tin nhân viên
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    getEmployeeById() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees/" + this.employeeSelectId;
      axios
        .get(aipUrl)
        .then((res) => {
          this.employee = res.data;
          this.employee.dateOfBirth = this.filterDate(
            this.employee.dateOfBirth
          );
          this.employee.identityDate = this.filterDate(
            this.employee.identityDate
          );
          this.employeeEqual = { ...this.employee };
        })
        .catch((err) => {
          console.log(err);
        });
    },
    /**
     * Hàm gọi API lấy về max code
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    getMaxCode() {
      var aipUrl = "https://localhost:44368/api/v1/Employees/getMaxCode";
      axios
        .get(aipUrl)
        .then((res) => {
          this.employee.employeeCode = res.data;
          this.employeeEqual = { ...this.employee };
        })
        .catch((err) => {
          console.log(err);
        });
    },
    /**
     * Hàm so sánh sâu 2 object
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    deepEqual(object1, object2) {
      const keys1 = Object.keys(object1);
      const keys2 = Object.keys(object2);
      if (keys1.length !== keys2.length) {
        return false;
      }
      for (const key of keys1) {
        const val1 = object1[key];
        const val2 = object2[key];
        const areObjects = this.isObject(val1) && this.isObject(val2);
        if (
          (areObjects && !this.deepEqual(val1, val2)) ||
          (!areObjects && val1 !== val2)
        ) {
          return false;
        }
      }
      return true;
    },
    /**
     * Hàm kiểm tra xem có phải object hay không
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    isObject(object) {
      return object != null && typeof object === "object";
    },
    /**
     * Hàm ẩn form chi tiết
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    hideDetailPage() {
      this.$emit("hideDetailPage");
    },

    /**
     * Hàm gọi API thêm nhân viên và cất form chi tiết
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    //Goi ipa them nhan vien
    addEmployee() {
      var aipUrl = "https://localhost:44368/api/v1/Employees/";
      this.employee.employeeCode = this.employee.employeeCode.trim();
      if (this.formMode == "add") {
        axios
          .post(aipUrl, this.employee)
          .then((res) => {
            if (res.status == 200) {
              this.hideDetailPage();
              this.$emit("shownoti", "Thêm nhân viên thành công");
            }
          })
          .catch((err) => {
            console.log(err);
            this.$emit("showWarningLog", err.response.data.errMsg);
          });
      }
      // Goi api sua nhan vien
      else {
        axios
          .put(aipUrl, this.employee)
          .then((res) => {
            if (res.status == 200) {
              this.hideDetailPage();
              this.$emit("shownoti", "Sửa nhân viên thành công");
            }
          })
          .catch((err) => {
            console.log(err.response);
            this.$emit("showWarningLog", err.response.data.errMsg);
          });
      }
    },
    /**
     * Hàm gọi API thêm nhân viên nhưng không cất form chi tiết
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    addMoreEmployee() {
      //Goi ipa them nhan vien
      var aipUrl = "https://localhost:44368/api/v1/Employees/";
      this.employee.employeeCode = this.employee.employeeCode.trim();
      if (this.formMode == "add") {
        axios
          .post(aipUrl, this.employee)
          .then((res) => {
            if (res.status == 200) {
              this.resetForm();
              this.$emit("shownoti", "Thêm nhân viên thành công");
            }
          })
          .catch((err) => {
            console.log(err);
            this.$emit("showWarningLog", err.response.data.errMsg);
          });
      } else {
        //Goi ipa sửa nhan vien
        axios
          .put(aipUrl, this.employee)
          .then((res) => {
            if (res.status == 200) {
              this.resetForm();
              this.$emit("shownoti", "Sửa nhân viên thành công");
            }
          })
          .catch((err) => {
            console.log(err.response);
            this.$emit("showWarningLog", err.response.data.errMsg);
          });
      }
    },
    /**
     * Hàm ấn vào nút cất
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    btnAddEdit() {
      this.codeBlured = true;
      this.nameBlured = true;
      this.pssBlured = true;
      if (this.checkAndAlert()) this.addEmployee();
    },
    /**
     * Hàm ấn vào nút cất và thêm
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    btnAddMore() {
      this.codeBlured = true;
      this.nameBlured = true;
      this.pssBlured = true;
      if (this.checkAndAlert()) this.addMoreEmployee();
      this.$nextTick(() => this.$refs.employeeCode.focus());
    },
    /**
     * Hàm reset form chi tiết
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    async resetForm() {
      this.$emit("setFormModeAdd");
      this.getMaxCode();
      this.codeBlured = false;
      this.nameBlured = false;
      this.pssBlured = false;
      this.employee = { ...this.employeeBackUp };
      this.employeeEqual = this.employee;
    },
    /**
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     * Hàm check thông tin ở client ( 1 hàm dùng chung nhaaaaaaaaaaa)
     */
    formValidate(value) {
      if (value.trim() == "" || value.trim() == null) return true;
      else return false;
    },
    /**
     * Hàm set giá trị khi ko focus vào ô vị trí ( vì phải dùng conbobox )
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    setPssBlured(value) {
      this.pssBlured = value;
    },
    /**
     * Hàm gán departmentId từ departmentName
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    setDepartmentName(departmentId, departmentName) {
      // this.employee.departmentName = value;
      this.employee.departmentId = departmentId;
      this.employee.departmentName = departmentName;
    },
    /**
     * Hàm ẩn cửa sổ dữ liệu đã bị thay đổi
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    hideDataChangedDialog() {
      this.isShowDataChanged = false;
    },
    /**
     * Hàm ẩn cửa sổ dữ liệu đã bị thay đổi và cất dữ liệu thay đổi
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    saveDataChanged() {
      this.hideDataChangedDialog();
      this.btnAddEdit();
    },
    /**
     * Hàm định dạng lại ngày tháng cho date
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    filterDate(dateString) {
      if (dateString === null || dateString === "") {
        return null;
      } else {
        return dateString.substring(0, 10);
      }
    },
    /**
     * Hàm check validate các mục bắt buộc nhật và gọi thông báo
     * CreatedBy: TDDUNG
     * Date: 11/5/2021
     */
    checkAndAlert() {
      if (this.formValidate(this.employee.employeeCode) && this.codeBlured) {
        this.$emit("showStatusLog", "Mã nhân viên không được để trống.");
        return false;
      } else if (
        this.formValidate(this.employee.employeeName) &&
        this.nameBlured
      ) {
        this.$emit("showStatusLog", "Tên nhân viên không được để trống.");
        return false;
      } else if (
        this.formValidate(this.employee.departmentId) &&
        this.pssBlured
      ) {
        this.$emit("showStatusLog", "Đơn vị không được để trống.");
        return false;
      }
      return true;
    },
  },
  watch: {},
  computed: {},
};
</script>

<style>
@import "../../assets/css/pages/employeeDetail.css";
</style>