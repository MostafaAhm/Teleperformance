<template>
  <div class="container">
    <div class="card">
      <div class="card-body">
        <h1>Sign Up</h1>
        <p>Please fill in this form to create an account</p>
        <hr />

        <label for="username"><p>Username</p></label>
        <input
          v-model="user.username"
          ref="username"
          type="text"
          placeholder="Enter Username"
          name="username"
        />
        <label for="email"><p>Email</p></label>
        <input
          v-model="user.email"
          ref="email"
          type="text"
          placeholder="Enter Email"
          name="email"
        />

        <label for="psw"><p>Password</p></label>
        <input
          v-model="user.password"
          ref="psw"
          type="text"
          placeholder="Enter Password"
          name="psw"
        />

        <div class="clearfix">
          <button type="button" class="cancelbtn" v-on:click="back">
            Cancel
          </button>
          <button type="submet" class="signupbtn" v-on:click="signup">
            Sign Up
          </button>
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
        email: "",
        password: "",
      },
    };
  },
  methods: {
    back() {
      this.$router.push({ name: "Login" });
    },
    signup() {
      if (this.checkValidation) {
        axios
          .post(this.hostname + "/api/account/register", this.user)
          .then((response) => {
            if (response.data.Id > 0) {
              Swal.fire("Successfully Registered").then(() => {
                this.back();
              });
            } else {
              Swal.fire("Error : Something went wrong.");
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
        Swal.fire("Give username");
        return;
      }
      if (!this.user.email) {
        this.$refs.email.focus();
        Swal.fire("Give Email");
        return;
      }
      if (!this.user.password) {
        this.$refs.psw.focus();
        Swal.fire("Give Password");
        return;
      }
    },
  },
};
</script>

<style scoped>
.container {
  width: 40%;
  padding: 16px;
}
input[type="text"],
input[type="password"],
input[type="email"] {
  width: 100%;
  padding: 15px;
  margin: 5px 0 22px 0;
  display: inline-block;
  border: none;
  background-color: #f1f1f1;
}
input[type="text"]:focus,
input[type="password"]:focus,
input[type="email"]:focus {
  background-color: #ddd;
  outline: none;
}
</style>
