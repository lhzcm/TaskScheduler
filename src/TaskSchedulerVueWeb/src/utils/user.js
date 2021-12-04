import userApi from "../api/user";
var user = null

export default {
    GetUserInfo: async function () {
        if (user) {
            return user;
        } else {
           let a =  await userApi.userInfo();
           user = a.data;
           return user;
        }
    }
}