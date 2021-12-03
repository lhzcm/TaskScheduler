import request from '../utils/request';
import qs from 'qs';

export default {
    login : query => {
        return request({
            url: 'User/Login',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'post',
            data: qs.stringify(query)
        });
    },
    userList : query => {
        return request({
            url: 'User/'+ (query.userId == 0 ? 0:(query.userId)),
            method: 'get'
        });
    },
    userInfo: ()=>{
        return request({
            url: 'User/UserInfo',
            method: 'get',
        });
    },
    addUser: query=>{
        return request({
            url: 'User/Add',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'post',
            data: query
        });
    },
    updateUser : query => {
        return request({
            url: 'User',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'patch',
            data: query//qs.stringify(query)
        }
    )},
    deleteUser : query => {
        return request({
            url: 'User',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'delete',
            data: qs.stringify(query)
        });
    }
};