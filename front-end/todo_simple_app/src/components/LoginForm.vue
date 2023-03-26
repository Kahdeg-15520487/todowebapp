<template>
  <form @submit.prevent="onSubmit">
    <h2 class="label-wrapper">
      <label for="username-input" class="label__lg">
        Username
      </label>
    </h2>
    <input type="text" id="username-input" name="username" autocomplete="off" v-model.lazy.trim="username"
      class="input__lg" />
    <h2 class="label-wrapper">
      <label for="password-input" class="label__lg">
        Password
      </label>
    </h2>
    <input type="text" id="password-input" name="password" autocomplete="off" v-model.lazy.trim="password"
      class="input__lg" />
    <button type="submit" class="btn btn__primary btn__lg">Add</button>
  </form>
</template>

<script>
import { userInfoStore } from '@/stores/userinfo.ts';

export default {
  methods: {
    async onSubmit() {
      if (this.username === "" || this.password === "") {
        return;
      }
      await this.login();
    },
    async login() {
      let user = JSON.parse(localStorage.getItem('user'));
      if (!user || !user.authdata) {
         user = { authdata: window.btoa(this.username + ':' + this.password) };
      }
      let res = await fetch(process.env.VUE_APP_API_URL + "/api/users", {
        method: 'GET',
        headers: {
          "Authorization": 'Basic ' + user.authdata,
          'Content-Type': 'application/json'
        }
      }
      );
      let data = await res.json();
      if (data.userName === user.username || data.userName === this.username) {
        console.log("login success");
        this.$emit("login-success");
        this.userInfo.setUserInfo(data.userName, data.id, user.authdata);
        user.username = data.userName;
        user.userId = data.id;
        console.log(this.userInfo.userId);
        localStorage.setItem('user', JSON.stringify(user));
        this.username = "";
        this.password = "";
      } else {
        console.log("login failure");
        console.log(data);
      }
    },
  },
  data() {
    return {
      username: "",
      password: "",
      userInfo: userInfoStore(),
    };
  },
  mounted() {
    this.login();
  }
};
</script>
