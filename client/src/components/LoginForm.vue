<template>
  <div class="col-md-12">
    <div class="container">
      <h3 class="e-shop-fonr">Login Page</h3>
      <div class="card">
        <div class="card-body">
          <div class="form-group">
            <label for="username"> User Name</label>
            <input
              v-model="user.username"
              ref="username"
              type="text"
              class="form-control"
              placeholder="Enter username"
              name="username"
            />
          </div>
          <div class="form-group">
            <label for="pwd"> Password</label>
            <input
              v-model="user.password"
              ref="psw"
              type="password"
              class="form-control"
              placeholder="Enter password"
              name="pwd"
            />
          </div>
          <div class="form-groub form-check">
            <label class="form-check-label">
              <input class="form-check-input" type="checkbox" name="remember" />
              Remember me
            </label>
          </div>
          <div class="clearfix">
            <button type="button" class="signin" v-on:click="login">
              Sign in
            </button>
            <button type="button" class="signup" v-on:click="signup">
              Sign up
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Swal from "sweetalert2";

export default {
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
    };
  },
  methods: {
    signup() {
      this.$router.push({ name: "Register" });
    },
    login() {
      if (this.checkValidation()) {
        axios
          .get(
            this.hostname +
              "/api/account/login" +
              this.user.username +
              "/" +
              this.user.password
          )
          .then((response) => {
            if (response.data.id > 0) {
              localStorage.setItem(
                "token",
                JSON.stringify(response.data.token)
              );
              response.data.token = "";
              localStorage.setItem("user", JSON.stringify(response.data));
              this.$router.push({ name: "Dashboard" });
            }
          })
          .catch((error) => {
            if (error.response) {
              Swal.fire(error.response.data);
            }
          });
      }
    },
    checkValidation() {
      if (!this.user.username) {
        this.$refs.username.focus();
        Swal.fire("Give Username");
        return;
      }
      if (!this.user.password) {
        this.$refs.password.focus();
        Swal.fire("Give password");
        return;
      }
      return true;
    },
  },
};
</script>

<style scoped>
.container {
  max-width: 360px;
}
button {
  background-color: red;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
}
</style>
