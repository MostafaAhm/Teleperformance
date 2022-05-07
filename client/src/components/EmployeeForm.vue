<template>
  <layout-form title="employee"></layout-form>
  <br />
  <label
    for="upload-pic"
    style="cursor: pointer; height: 200px; weight: 200px; margin-bottom: 50px"
  >
    <img
      class="rounded-circle mt-5 imgTap"
      style="width=100%;height=100%"
      :src="fileScr"
    />
    <input
      id="upload-pic"
      @change="uploadImage($event)"
      class="upload-pic"
      type="file"
      accept="image/*"
      name="photo"
      style="dispay: none"
    />
  </label>
  <br />
  <input
    v-model="employee.name"
    ref="name"
    type="text"
    placeholder="Employee Name"
  />
  <br />
  <input
    v-model="employee.phoneNumber"
    ref="phone"
    type="text"
    placeholder="Enter Phonenumber"
  />
  <br />
  <input
    v-model="employee.employeetypeid"
    ref="type"
    type="text"
    placeholder="Enter type"
  />
  <br />
  <input
    v-model="employee.email"
    ref="email"
    type="text"
    placeholder="Employee Email"
  />
  <br />
  <input
    v-model="employee.password"
    ref="psw"
    type="text"
    placeholder="Enter Password"
  />
  <br />
  <button class="my-button" v-on:click="save">Save</button>
</template>

<script>
import axios from "axios";
import Swal from "sweetalert2";
import LayoutForm from "./LayoutForm.vue";
import { toHandlerKey } from "@vue/shared";
export default {
  data() {
    return {
      file: "",
      fileScr: require("@/assets/logo.png"),
      employee: {
        id: 0,
        name: "",
        password: "",
        email: "",
        phonenumber: "",
        employeetypeid: 0,
      },
    };
  },
  async created() {
    if (this.$route.params.id > 0) await this.getEmployees();
  },
  components: { LayoutForm },
  methods: {
    async getEmployees() {
      var employeeid = this.$route.params.id;

      await axios
        .get(
          this.hostname + "/api/employee/getemployees/" + employeeid,
          this.getTokenConfig()
        )
        .then((response) => {
          this.employee = response.data;
          if (this.employee.image != null && this.employee.image.Lenght > 0) {
            this.fileScr = "data:image/jpeg;base64," + this.employee.image;
            this.file = "data:image/jpeg;base64," + this.employee.image;
          }
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
    save() {
      if (this.checkValidation()) {
        var formData = new FormData();
        for (var key in this.employee) {
          formData.append(key, this.employee[key]);
        }
        formData.append("Files", this.file);
        axios
          .post(
            this.hostname + "/api/employee/createemployee",
            formData,
            this.getTokenConfig()
          )
          .then((response) => {
            if (response.data.id > 0) {
              Swal.fire("Successfully Saved");
              this.$router.push({ name: "Dashboard" });
            } else {
              Swal.fire("Something Went Wrong");
            }
          })
          .catch((error) => {
            if (error.response) {
              Swal.fire(error.response.data);
            }
          });
      }
    },
    uploadImage(e) {
      this.file = e.target.files[0];
      this.fileScr = window.URL.createObjectURL(this.file);
    },
    checkValidation() {
      if (!this.employee.name) {
        this.$refs.name.focus();
        Swal.fire("Give Name");
        return;
      }
      if (!this.employee.email) {
        this.$refs.email.focus();
        Swal.fire("Give Email");
        return;
      }
      if (!this.employee.password) {
        this.$refs.psw.focus();
        Swal.fire("Give Password");
        return;
      }
      if (!this.employee.employeetypeid) {
        this.$refs.type.focus();
        Swal.fire("Give TypeID");
        return;
      }
      if (!this.employee.phonenumber) {
        this.$refs.phone.focus();
        Swal.fire("Give PhoneNumber");
        return;
      }
    },
  },
};
</script>
