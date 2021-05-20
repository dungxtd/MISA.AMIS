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
      <EmptyData v-if="status == 1 && employees.length == 0" />
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
                <th style="min-width: 150px; text-align: center">Ngày sinh</th>
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
                <td>{{ employee.EmployeeCode }}</td>
                <td>{{ employee.EmployeeName }}</td>
                <td>{{ employee.Gender | formatGender }}</td>
                <td style="text-align: center">
                  {{ employee.DateOfBirth | formatDate }}
                </td>
                <td>{{ employee.IdentityNumber }}</td>
                <td>{{ employee.EmployeePosition }}</td>
                <td>{{ employee.DepartmentName }}</td>
                <td>{{ employee.BankAccountNumber }}</td>
                <td>{{ employee.BankName }}</td>
                <td style="border-right: 0 !important">
                  {{ employee.BankBranchName }}
                </td>
                <div class="action">
                  <div
                    class="edit-text"
                    style="font-weight: 600; margin: 0 20px; cursor: pointer"
                    @click="clickEditEmployee(employee.EmployeeId)"
                  >
                    Sửa
                  </div>
                  <div
                    class="icon icon-24 icon-more"
                    :class="{ outLine: index == indexTemp && isShowMoreOption }"
                    v-on:click="clickShowMoreOption(index, employee.EmployeeId)"
                    @click="someMethod"
                  ></div>
                </div>
                <MoreOptions
                  @getData="getData()"
                  @hideMoreOption="hideMoreOption()"
                  @cloneEmployee="cloneEmployee"
                  :employee="employee"
                  :screenX="screenX"
                  :screenY="screenY"
                  v-if="index == indexTemp && isShowMoreOption"
                  v-click-outside="clickOutSide"
                  @shownoti="shownoti"
                />
              </tr>
            </tbody>
          </table>
        </div>
        <div class="footer">
          <div class="title-footer">
            Tống số:
            <div class="cout-paging">{{ count }}</div>
            bản ghi
          </div>
          <div class="paging-footer">
            <div class="page-size" style="width: 200px">
              <PageSizeBox
                :options="[
                  '10 bản ghi trên 1 trang',
                  '20 bản ghi trên 1 trang',
                  '30 bản ghi trên 1 trang',
                  '40 bản ghi trên 1 trang',
                ]"
                :isHidePageSize="isHidePageSize"
                :default="'20 bản ghi trên 1 trang'"
                :pageSize="pageSize"
                class="select"
                @setPageSize="setPageSize"
              />
            </div>
            <div style="margin: auto; display: flex">
              <div
                class="pre-page"
                @click="toPrePage()"
                :class="{ hideBtn: pageIndex == 1 }"
              >
                Trước
              </div>
              <div class="number-page">
                <div
                  class="page"
                  @click="toFirstPage()"
                  :class="{ pageSelected: pageIndex == 1 }"
                >
                  1
                </div>
                <div
                  class="page"
                  @click="toPageIndexDisplayPre()"
                  v-if="pageIndex >= 3 && pageIndexDisplay > 3 && pageMax > 4"
                >
                  ...
                </div>
                <div
                  class="page"
                  @click="
                    pageIndex = pageMax - 2;
                    pageIndexDisplay = pageMax - 2;
                  "
                  v-if="pageIndex == pageMax && pageMax > 3"
                >
                  {{ pageMax | formatPagePre2 }}
                </div>
                <div
                  class="page"
                  v-if="pageIndex > 2"
                  :class="{
                    pageSelected:
                      pageIndex - 1 == pageIndexDisplay &&
                      pageIndex > 1 &&
                      pageIndex < pageMax,
                  }"
                  @click="btnFirst()"
                >
                  {{ pageIndexDisplay | formatIndexPagePre }}
                </div>
                <div
                  class="page"
                  v-if="pageIndex != 1 && pageIndex != pageMax && pageMax >= 3"
                  @click="btnSecond()"
                  :class="{
                    pageSelected:
                      pageIndex == pageIndexDisplay &&
                      pageIndex > 1 &&
                      pageIndex < pageMax,
                  }"
                >
                  {{ pageIndexDisplay }}
                </div>
                <div
                  class="page"
                  v-if="pageIndex < pageMax - 1"
                  :class="{
                    pageSelected:
                      pageIndex + 1 == pageIndexDisplay &&
                      pageIndex > 1 &&
                      pageIndex < pageMax,
                  }"
                  @click="btnThird()"
                >
                  {{ pageIndexDisplay | formatIndexPageNext }}
                </div>
                <div
                  class="page"
                  @click="
                    pageIndex = 3;
                    pageIndexDisplay = 3;
                  "
                  v-if="pageIndex == 1 && pageMax != 0 && pageMax > 3"
                >
                  3
                </div>
                <div
                  class="page"
                  @click="toPageIndexDisplayNext()"
                  v-if="
                    pageIndex <= pageMax - 2 &&
                    pageIndexDisplay < pageMax - 2 &&
                    pageMax > 4
                  "
                >
                  ...
                </div>
              </div>
              <div
                v-if="pageMax > 1"
                class="page"
                @click="toMaxPage()"
                :class="{ pageSelected: pageIndex == pageMax }"
              >
                {{ pageMax }}
              </div>
              <div
                class="page"
                @click="toNextPage()"
                :class="{ hideBtn: pageIndex == pageMax || pageMax == 0 }"
              >
                Sau
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- v-if="isShowDetail == true" -->
    </div>
    <EmployeeDetail
      v-if="isShowDetail == true"
      :employeeSelectId="employeeSelectId"
      :formMode="formMode"
      @hideDetailPage="hideDetailPage"
      @showStatusLog="showStatusLog"
      @setFormModeAdd="setFormModeAdd"
      @showWarningLog="showWarningLog"
      @shownoti="shownoti"
    />
    <StatusDialog
      v-if="isShowStatusLog == true"
      @hideStatusLog="hideStatusLog"
      :mesStatus="mesStatus"
    />
    <WarningDialog
      v-if="isShowWarningLog == true"
      :warningMsg="errMsg"
      @hideWarningLog="hideWarningLog"
    />
    <transition name="slide-fade">
      <Notification v-if="isShowNoti == true" :msgStatus="msgStatus" />
    </transition>
  </div>
</template>
<script>
import axios from "axios";
import MoreOptions from "../dialogs/MoreOption";
import LoadingScreen from "../dialogs/LoadingScreen";
import EmployeeDetail from "../dialogs/EmployeeDetail";
import StatusDialog from "../dialogs/StatusDialog";
import WarningDialog from "../dialogs/WarningDialog";
import PageSizeBox from "../dialogs/PageSizeBox";
import EmptyData from "../dialogs/EmptyData";
import Notification from "../dialogs/Notification";
import Vue from "vue";
import vClickOutside from "v-click-outside";
Vue.use(vClickOutside);
const LOADING = 0; // Biến chỉ trạng thái page là đang loading
const SUCCESS = 1; // Biến chỉ trạng thái page là loading thành công
export default {
  components: {
    MoreOptions,
    LoadingScreen,
    EmployeeDetail,
    StatusDialog,
    WarningDialog,
    PageSizeBox,
    EmptyData,
    Notification,
  },
  data() {
    return {
      employees: [], //Bien chua mang nhan vien
      employee: {}, // Biến nhân viên
      pageIndex: 1, // Biến chỉ trang hiện tại
      pageIndexDisplay: 1, // Biến chỉ trang hiển thị trên màn hình
      pageMax: 0, //// Biến chỉ tổng số trang
      pageSize: 20, // Biến chứa số lượng bản ghi 1 trang
      indexTemp: 0, //Biến tạm chỉ giá trị index vòng for
      isShowMoreOption: false, //Bien hien thi bang more option
      screenX: 0, // Biến toạ độ con trỏ chuột
      screenY: 0, // Biến toạ độ con trỏ chuột
      status: 0, // Biến chỉ trạng thái load của trang
      count: 0, // Biến chứa số lượng bản ghi
      debouncedInput: "", // Biến hiện thị filter search sau khi có trễ time
      isShowDetail: false, //biến ẩn hay hiện bảng detail
      formMode: "add", //Biến chứa thông tin thêm hay sửa gửi thị lên dialog
      isShowStatusLog: false, //Biến hiển thị cửa sổ thồn báo lỗi
      mesStatus: "", // Biến chứa câu thông báo lỗi
      isShowWarningLog: false, //Biến hiển thị cửa sổ thồn báo trùng nhân viên
      errMsg: "", //Biến chứa câu thông báo cảnh báo
      timeout: 500, //Biến chứa thời gian trễ
      employeeSelectId: "", //Biến chứa id nhân viên đã chọn
      isHidePageSize: false, //Bien an hay hien page size
      msgStatus: "", //Câu thông báo truyền lên noti
      isShowNoti: false, //Biến hiển thị noti
    };
  },
  created() {
    this.loadData();
  },
  methods: {
    /**
     * Ham load lai data
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    loadData() {
      this.pageIndex = 1;
      this.pageSize = 20;
      this.getData();
    },
    /**
     * Ham gọi API lấy data về
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
      // console.log(this.pageSize);
      await axios
        .get(aipUrl)
        .then((res) => {
          this.employees = res.data.employees;
          this.count = res.data.countEmployees[0];
        })
        .catch((err) => {
          console.log(err);
        });
      this.pageMax = Math.ceil(this.count / this.pageSize);
      this.status = SUCCESS;
      // console.log(this.employees);
    },
    /**
     * Ham tim kiem va sap xep trang
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    searchAndArrangePage() {
      this.pageIndex = 1;
      this.pageIndexDisplay = 1;
      this.getData();
    },
    /**
     * Ham set pageSize từ PageSizeBox
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    setPageSize(pageSize) {
      this.pageSize = pageSize;
      this.pageIndex = 1;
      this.pageIndexDisplay = 1;
      this.getData();
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
        fileLink.setAttribute("download", "Danh_sach_nhan_vien.xlsx");
        document.body.appendChild(fileLink);
        fileLink.click();
      });
    },
    /**
     * Ham click de hien bang thong tin them
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    clickShowMoreOption(index, employeeId) {
      this.indexTemp = index;
      this.employeeSelectId = employeeId;
      this.isShowMoreOption = !this.isShowMoreOption;
    },
    /**
     * Ham lay vi tri cua con tro chuot
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    someMethod(event) {
      this.screenX = event.clientX;
      this.screenY = event.clientY;
      if (this.screenY > 770) this.screenY = 770;
    },
    /**
     * Ham hien dialog xoa
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    hideMoreOption() {
      this.isShowMoreOption = false;
      this.employee = Object.create(null);
    },
    /**
     * Ham lay so ban ghi lon nhat va hien cua so them
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    showDialogDetail() {
      this.formMode = "add";
      this.isShowDetail = true;
    },
    /**
     * Ham Ham an cua so dialog
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    hideDetailPage() {
      this.isShowDetail = false;
      this.getData();
    },
    /**
     * Ham hiển thị cửa sổ detail khi ấn sửa
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    clickEditEmployee(employeeId) {
      this.formMode = "edit";
      this.employeeSelectId = employeeId;
      this.isShowDetail = true;
    },
    /**
     * Ham hiển thị cửa sổ detail khi ấn nhân bản
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    cloneEmployee(employeeId) {
      this.formMode = "clone";
      this.employeeSelectId = employeeId;
      this.isShowDetail = true;
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
    /**
     * Ham bat su kien click ben ngoai
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    clickOutSide() {
      // console.log("click out side");
      this.hideMoreOption();
    },
    /**
     * Ham set form mode là add khi sửa nhưng nhấn cất và thêm
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    setFormModeAdd() {
      this.formMode = "add";
    },
    /**
     * Ham hiển thị cửa sổ cảnh báo là trùng mã
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    showWarningLog(errMsg) {
      this.errMsg = errMsg;
      this.isShowWarningLog = true;
    },
    /**
     * Ham ẩn cửa sổ cảnh báo
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    hideWarningLog() {
      this.isShowWarningLog = false;
    },
    //#region Các hàm gán sự kiện cho nút phân trang
    /**
     * Ham chuyen den trang truoc
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    toPrePage() {
      if (this.pageIndex > 1) {
        this.pageIndex--;
        this.pageIndexDisplay = this.pageIndex;
      }
    },
    /**
     * Ham chuyen den trang ke tiep
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    toNextPage() {
      if (this.pageIndex < this.pageMax) {
        this.pageIndex++;
        this.pageIndexDisplay = this.pageIndex;
      }
    },
    /**
     * Ham review den trang truoc
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    toPageIndexDisplayPre() {
      if (this.pageMax > 5 && this.pageIndex < this.pageMax - 1) {
        if (this.pageIndexDisplay > 4) {
          this.pageIndexDisplay = this.pageIndexDisplay - 2;
        }
      }
    },
    /**
     * Ham review den trang ke tiep
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    toPageIndexDisplayNext() {
      if (this.pageMax > 5 && this.pageIndex > 2) {
        if (this.pageIndexDisplay < this.pageMax - 3) {
          this.pageIndexDisplay = this.pageIndexDisplay + 2;
        }
      }
    },

    /**
     * Nút trang thứ nhất
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    btnFirst() {
      this.pageIndex = this.pageIndexDisplay - 1;
      this.pageIndexDisplay = this.pageIndex;
    },
    /**
     * Nút trang thứ hai
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    btnThird() {
      this.pageIndex = this.pageIndexDisplay + 1;
      this.pageIndexDisplay = this.pageIndex;
    },

    /**
     * Nút trang thứ ba
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    btnSecond() {
      this.pageIndex = this.pageIndexDisplay;
      this.pageIndexDisplay = this.pageIndex;
    },
    /**
     * Nút đến trang đầu tiên
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    toFirstPage() {
      this.pageIndex = 1;
      this.pageIndexDisplay = this.pageIndex;
    },
    /**
     * Nút đến trng cúng cuồi
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    toMaxPage() {
      this.pageIndex = this.pageMax;
      this.pageIndexDisplay = this.pageIndex;
    },
    //#endregion

    shownoti(msg) {
      this.msgStatus = msg;
      this.isShowNoti = true;
      setTimeout(() => {
        this.isShowNoti = false;
      }, 1500);
    },
  },
  mounted() {
    // this.shownoti();
  },
  computed: {
    /**
     * Theo dõi ô input filter có set time out
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
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
    /**
     * Ham theo dõi biến input đã set tiomout có thay đổi thì thực hiện tìm kiếm
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    debouncedInput() {
      if (this.debouncedInput != String.Empty) {
        this.searchAndArrangePage();
      }
    },
    /**
     * Ham theo dõi biến page index
     * CreatedBy: TDDUNG
     * DATE: 16/5/2021
     */
    pageIndex() {
      this.getData();
    },
  },
};
</script>

<style>
@import "../../assets/css/common/table.css";
.slide-fade-enter-active {
  transition: all 0.3s ease;
}
.slide-fade-leave-active {
  transition: all 0.5s cubic-bezier(1, 0.5, 0.8, 1);
}
.slide-fade-enter, .slide-fade-leave-to
/* .slide-fade-leave-active below version 2.1.8 */ {
  transform: translateX(10px);
  opacity: 0;
}
</style>