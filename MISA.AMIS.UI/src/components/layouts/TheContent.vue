<template>
  <div id="content">
    <EmployeeList />
  </div>
</template>

<script>
import EmployeeList from "../pages/EmployeeList";
export default {
  name: "TheContent",
  data() {
    return {};
  },
  components: {
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
}
</style>
