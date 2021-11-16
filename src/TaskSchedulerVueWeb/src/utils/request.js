import axios from 'axios';
import router from '../router/index.js'

const service = axios.create({
    // process.env.NODE_ENV === 'development' 来判断是否开发环境
    // easy-mock服务挂了，暂时不使用了
    baseURL: 'http://127.0.0.1:8686/',
    //baseURL:'http://113.108.233.81:8687/',
    timeout: 10000
});

service.interceptors.request.use(
    config => {
        config.withCredentials = true; //withCredentials: true
        return config;
    },
    error => {
        console.log(error);
        return Promise.reject();
    }
);

service.interceptors.response.use(
    response => {
        if (response.status === 200) {
            if(response.data.code == -2){
                router.push("Login");
                Promise.reject();
            }
            return response.data;
        } else {
            Promise.reject();
        }
    },
    error => {
        console.log(error);
        return Promise.reject();
    }
);

export default service;
