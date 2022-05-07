<template>
  <layout-form title="DashboardForm"></layout-form>
  <br />
  <button
    v-on:click="addEmployee"
    class="my-button"
    style="margin: 10px 0 10px 0"
  >
    Add New Employee
  </button>
  <table class="basic-table">
    <thread>
      <tr>
        <th>Name</th>
        <th>PhoneNumber</th>
        <th>Type</th>
      </tr>
    </thread>
    <tbody>
      <tr v-for="emp in employees" :key="emp.Id">
        <td>{{ emp.name }}</td>
        <td>{{ emp.phoneNumber }}</td>
        <td>{{ emp.type }}</td>
        <td>
          <button
            v-on:click="editEmployee(emp.Id)"
            class="my-button"
            style="float: left"
          >
            Edit
          </button>
          <button
            v-on:click="deleteEmployee(emp.Id)"
            class="my-button"
            style="float: left; margin-right: 5px"
          >
            Delete
          </button>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>
import axios from "axios";
import Swal from "sweetalert2";
import LayoutForm from "./LayoutForm.vue";

export default {
  components: { LayoutForm },
  data() {
    return {
      employees: [],
    };
  },
  // eslint-disable-next-line no-dupe-keys
  components: {
    LayoutForm,
  },
  async created() {
    await this.getEmployees();
  },
  method: {
    addEmployee() {
      this.$router.push({ name: "employee", params: { id: 0 } });
    },
    editEmployee(employeeid) {
      this.$router.push({ name: "employee", params: { id: employeeid } });
    },
    deleteEmployee(employeeid) {
      axios
        .delete(
          this.hostname + "/api/employee/deleteemployee/",
          this.getTokenConfig()
        )
        .then((response) => {
          Swal.fire(response.data);
          var removeIndex = this.employees.findIndex(
            (x) => x.Id === employeeid
          );
          ~removeIndex && this.employees.splice(removeIndex, 1);
        })
        .catch((error) => {
          if (error.response) {
            Swal.fire(error.response.data);
          }
        });
    },
    getTokenConfig() {
      var token = JSON.parse(localStorage.getItem("token"));
      const config = {
        headers: { Authorization: `Bearer ${token}` },
      };
      return config;
    },
    async getEmployees() {
      await axios
        .get(
          this.hostname + "/api/employee/getemployees/",
          this.getTokenConfig()
        )
        .then((response) => {
          this.employees = response.data;
        })
        .catch((error) => {
          if (error.response) {
            Swal.fire(error.response.data);
          }
        });
    },
  },
};
</script>
