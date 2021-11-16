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
    }
};