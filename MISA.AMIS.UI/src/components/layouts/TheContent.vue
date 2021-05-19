<template>
  <div id="content">
    <EmployeeList />
    <!-- <Test /> -->
  </div>
</template>

<script>
import EmployeeList from "../pages/EmployeeList";
// import Test from "../dialogs/Test";
export default {
  name: "TheContent",
  data() {
    return {};
  },
  components: {
    // Test,
    EmployeeList,
  },
  methods: {},
};
</script>
<style>
#content {
  height: calc(100% - 48px);
  padding: 0 30px 0 20px;
  background-color: #f4f5f8;
  /* background-color: #68718d; */
}
</style>
