<template>
  <div>
    <div class="header-list">
      <div class="title-list">Nhân viên</div>
      <div class="btn-add" @click="showDialogDetail">Thêm mới nhân viên</div>
    </div>
    <div class="grid-list">
      <div class="right-grid">
        <div class="search-input">
          <input
            type="text"
            class="filter"
            placeholder="Tìm theo mã, tên nhân viên"
            v-model="inputFilter"
            style="width: 240px !important"
          />
          <div class="icon icon-24 icon-search"></div>
        </div>
        <div class="wrap-refresh" @click="loadData">
          <div class="icon icon-24 refresh-icon"></div>
        </div>
        <div class="wrap-export" @click="exportFile">
          <div class="icon icon-24 export-icon"></div>
        </div>
      </div>
    </div>
    <div class="main-list">
      <LoadingScreen v-if="status == 0" />
      <div class="list-control">
        <div class="list-grid">
          <table>
            <thead>
              <tr>
                <td class="multi-checkbox">
                  <input type="checkbox" />
                  <div class="border-checkbox-th"></div>
                </td>
                <th style="min-width: 150px">Mã nhân viên</th>
                <th style="min-width: 250px">Tên nhân viên</th>
                <th style="min-width: 120px">Giới tính</th>
                <th style="min-width: 150px">Ngày sinh</th>
                <th style="min-width: 200px">Số CMND</th>
                <th style="min-width: 250px">Chức danh</th>
                <th style="min-width: 250px">Tên đơn vị</th>
                <th style="min-width: 150px">Số tài khoản</th>
                <th style="min-width: 250px">Tên ngân hàng</th>
                <th style="min-width: 200px; border-right: 0 !important">
                  Chi nhánh TK ngân hàng
                </th>
                <th
                  class="action-title"
                  style="min-width: 120px; padding-top: 7px !important"
                >
                  Chức năng
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(employee, index) in employees" :key="index">
                <th>
                  <input type="checkbox" />
                  <div class="border-checkbox-th"></div>
                </th>
                <td>{{ employee.employeeCode }}</td>
                <td>{{ employee.employeeName }}</td>
                <td>{{ employee.gender | formatGender }}</td>
                <td>{{ employee.dateOfBirth | formatDate }}</td>
                <td>{{ employee.identityNumber }}</td>
                <td>{{ employee.employeePosition }}</td>
                <td>{{ employee.departmentName }}</td>
                <td>{{ employee.bankAccountNumber }}</td>
                <td>{{ employee.bankName }}</td>
                <td style="border-right: 0 !important">
                  {{ employee.bankBranchName }}
                </td>
                <div class="action">
                  <div
                    class="edit-text"
                    style="
                      font-weight: 600;
                      margin-right: 10px;
                      cursor: pointer;
                    "
                    @click="clickEditEmployee(employee)"
                  >
                    Sửa
                  </div>
                  <div
                    class="icon icon-24 icon-more"
                    v-on:click="clickShowMoreOption(index)"
                    @click="someMethod"
                  ></div>
                </div>
                <MoreOptions
                  @getData="getData()"
                  @hideMoreOption="hideMoreOption()"
                  :idDelete="employee.employeeId"
                  :codeDelete="employee.employeeCode"
                  :screenX="screenX"
                  :screenY="screenY"
                  v-if="index == indexTemp && isShowMoreOption"
                />
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div class="footer">
        <div class="title-footer">
          Tống số:
          <div class="cout-paging">{{ count }}</div>
          bản ghi
        </div>
        <div class="paging-footer">
          <div class="page-size"></div>
          <div class="pre-page">Trước</div>
          <div class="number-page">
            <div class="page">...</div>
            <div class="page pageSelected">1</div>
            <div class="page">2</div>
            <div class="page">3</div>
            <div class="page">...</div>
          </div>
          <div class="next-page">Sau</div>
        </div>
      </div>
      <!-- v-if="isShowDetail == true" -->
    </div>
    <EmployeeDetail
      v-if="isShowDetail == true"
      :employee="employee"
      @hideDetailPage="hideDetailPage"
      @showStatusLog="showStatusLog"
      :formMode="formMode"
      :inputFocus="inputFocus"
    />
    <StatusDialog
      v-if="isShowStatusLog == true"
      @hideStatusLog="hideStatusLog"
      :mesStatus="mesStatus"
    />
  </div>
</template>
<script>
import axios from "axios";
import MoreOptions from "../dialogs/MoreOption";
import LoadingScreen from "../dialogs/LoadingScreen";
import EmployeeDetail from "../dialogs/EmployeeDetail";
import StatusDialog from "../dialogs/StatusDialog";
const LOADING = 0;
const SUCCESS = 1;
export default {
  components: {
    MoreOptions,
    LoadingScreen,
    EmployeeDetail,
    StatusDialog,
  },
  data() {
    return {
      employees: [], //Bien chua mang nhan vien
      employee: {},
      maxPage: 0, // Số số trang
      pageIndex: 1, // Biến chỉ trang hiện tại
      pageSize: 30, // Biến chứa số lượng bản ghi 1 trang
      indexTemp: 0, //Biến tạm chỉ giá trị index vòng for
      isShowMoreOption: false, //Bien hien thi bang more option
      screenX: 0, // Biến toạ độ con trỏ chuột
      screenY: 0, // Biến toạ độ con trỏ chuột
      status: 0, // Biến chỉ trạng thái load của trang
      count: 0, // Biến chứa số lượng bản ghi
      debouncedInput: "", // Biến hiện thị filter search sau khi có trễ time
      isShowDetail: false, //biến ẩn hay hiện bảng detail
      formMode: "add", //Biến chứa thông tin thêm hay sửa gửi thị lên dialog
      emptyData: true, //Biến theo dõi data
      isShowStatusLog: false, //Biến hiển thị cửa sổ thồn báo lỗi
      mesStatus: "", // Biến chứa câu thông báo lỗi
    };
  },
  created() {
    this.getData();
  },
  methods: {
    /**
     * Ham load lai data
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    loadData() {
      this.pageIndex = 1;
      this.pageSize = 10;
      this.getData();
    },
    /**
     * Ham lay ve data
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    async getData() {
      this.status = LOADING;
      var aipUrl =
        "https://localhost:44368/api/v1/Employees/paging?pageIndex=" +
        this.pageIndex +
        "&pageSize=" +
        this.pageSize;
      if (this.inputFilter != String.Empty)
        aipUrl += "&filter=" + this.inputFilter;
      console.log(this.pageSize);
      await axios
        .get(aipUrl)
        .then((res) => {
          this.employees = res.data;
        })
        .catch((err) => {
          console.log(err);
        });
      this.getCount();
      this.status = SUCCESS;
      // console.log(this.employees);
    },
    /**
     * Ham dem so luong ban ghi
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    async getCount() {
      var aipUrl =
        "https://localhost:44368/api/v1/Employees/count-paging?filter=" +
        this.inputFilter;
      await axios
        .get(aipUrl)
        .then((res) => {
          this.count = res.data[0];
        })
        .catch((err) => {
          console.log(err);
        });
      this.maxPage = (this.count / this.pageSize).toFixed();
    },
    /**
     * Ham tim kiem va sap xep trang
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    searchAndArrangePage() {
      this.pageIndex = 1;
      this.getData();
      this.getCount();
    },
    /**
     * Ham xuat ra file excel
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    async exportFile() {
      var apiUrl = "https://localhost:44368/api/v1/Employees/export";
      await axios({
        url: apiUrl,
        method: "GET",
        responseType: "blob",
      }).then((response) => {
        var fileURL = window.URL.createObjectURL(new Blob([response.data]));
        var fileLink = document.createElement("a");

        fileLink.href = fileURL;
        fileLink.setAttribute("download", "data.xlsx");
        document.body.appendChild(fileLink);

        fileLink.click();
      });
    },
    /**
     * Ham click de hien bang thong tin them
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    clickShowMoreOption(index) {
      this.indexTemp = index;
      console.log(index + " " + this.indexTemp);
      if (this.isShowMoreOption == false) this.isShowMoreOption = true;
      else this.isShowMoreOption = false;
    },
    /**
     * Ham lay vi tri cua con tro chuot
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    someMethod(event) {
      this.screenX = event.clientX;
      this.screenY = event.clientY;
      if (this.indexTemp == this.employees.length - 1)
        this.screenY = this.screenY - 80;
      // console.log(this.screenX);
      // console.log(this.screenY);
    },
    /**
     * Ham hien dialog xoa
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    hideMoreOption() {
      this.isShowMoreOption = false;
    },
    /**
     * Ham lay so ban ghi lon nhat va hien cua so them
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    async showDialogDetail() {
      this.formMode = "add";
      this.employee = {};
      this.employee.employeeCode = "";
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
      this.isShowDetail = true;
      this.inputFocus = true;
    },
    /**
     * Ham Ham an cua so dialog
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    hideDetailPage() {
      this.isShowDetail = false;
      this.inputFocus = false;
      this.getData();
    },
    /**
     * Ham hiển thị cửa sổ detail khi ấn sửa
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    clickEditEmployee(employee) {
      this.formMode = "edit";
      this.employee = employee;
      if (this.employee.dateOfBirth != null)
        this.employee.dateOfBirth = this.employee.dateOfBirth.substring(0, 10);
      if (this.employee.identityDate != null)
        this.employee.identityDate = this.employee.identityDate.substring(
          0,
          10
        );
      this.isShowDetail = true;
      this.inputFocus = true;
    },
    /**
     * Ham hiển thị cửa sổ cảnh báo
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    showStatusLog(sttMsg) {
      this.isShowStatusLog = true;
      this.mesStatus = sttMsg;
    },
    /**
     * Ham ẩn cửa sổ cảnh báo
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    hideStatusLog() {
      this.isShowStatusLog = false;
    },
  },
  computed: {
    inputFilter: {
      get() {
        return this.debouncedInput;
      },
      set(val) {
        if (this.timeout) clearTimeout(this.timeout);
        this.timeout = setTimeout(() => {
          this.debouncedInput = val;
        }, 500);
      },
    },
  },
  watch: {
    debouncedInput() {
      if (this.debouncedInput != String.Empty) {
        this.searchAndArrangePage();
      }
    },
  },
};
</script>

<style>
@import "../../assets/css/common/table.css";
input[type="checkbox"] {
  flex-shrink: 0;
  width: 18px;
  height: 18px;
  appearance: none;
  -webkit-appearance: none;
  background: white;
  outline: none;
  border: 1px solid #afafaf;
  border-radius: 2px;
  transition: 0.5s;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}
input:checked[type="checkbox"] {
  border: 1px solid #2ca01c;
  transform: rotate(90deg);
}

input[type="checkbox"]::before {
  content: "";
  position: absolute;
  width: 16px;
  height: 16px;
  background: url(../../assets/img/Sprites.svg);
  background-position: -1224px -360px;
  display: none;
}

input:checked[type="checkbox"]::before {
  display: block;
  transform: rotate(-90deg);
}
.filter {
  box-sizing: border-box;
  width: 100%;
  position: relative;
  font-size: 13px;
  height: 32px;
  padding: 6px 10px;
  padding-right: 1.75rem;
  background: #fff;
  border-radius: 3px;
  color: #111;
  border: 1px solid #ebedf0;
  padding-left: 0.85em;
  max-width: 300px;
  min-width: 200px;
}
.icon-more {
  background-position: -884px -359px !important;
  cursor: pointer;
}
</style>