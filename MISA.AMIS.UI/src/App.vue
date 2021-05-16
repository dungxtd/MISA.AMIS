<template>
  <div id="app">
    <TheNavbar />
    <div id="container">
      <TheHeader />
      <TheContent />
    </div>
  </div>
</template>

<script>
import TheNavbar from "./components/layouts/TheNavbar";
import TheHeader from "./components/layouts/TheHeader";
import TheContent from "./components/layouts/TheContent";

export default {
  name: "App",
  components: {
    TheNavbar,
    TheHeader,
    TheContent,
  },
};
</script>

<style>
@import "./assets/css/index.css";
#container {
  width: calc(100% - 178px);
  display: inline-block;
}
</style>
