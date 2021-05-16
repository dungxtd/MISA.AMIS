import Vue from 'vue'
import App from './App.vue'
import moment from 'moment';
Vue.config.productionTip = false
Vue.filter("formatDate", function (value) {
  if (value) {
    value = value.substring(0, 10);
    return moment(String(value)).format("MM/DD/YYYY");
  }
});
Vue.filter("formatIndexPagePre", function (value) {
  if (value) {
    value = value - 1;
    return value;
  }
});
Vue.filter("formatIndexPageNext", function (value) {
  if (value) {
    value = value + 1;
    return value;
  }
});
Vue.filter("formatGender", function (value) {
  if (value == 0) {
    return "Nam";
  }
  if (value == 1) {
    return "Nữ";
  }
  if (value == 2) {
    return "Khác";
  }
  else {
    return null;
  }
});

new Vue({
  render: h => h(App),
}).$mount('#app')
