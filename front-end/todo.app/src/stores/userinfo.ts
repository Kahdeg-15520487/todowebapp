import { defineStore } from 'pinia'

export const userInfoStore = defineStore('userInfo', {
    state: () => {
        return { username: "", userId: "", authData: "" }
    },
    actions: {
        setUserInfo(usn, uid, authData) {
            this.username = usn;
            this.userId = uid;
            this.authData = authData;
        },
        clearUserInfo() {
            this.username = "";
            this.userId = "";
            this.authData = "";
        }
    },
})
