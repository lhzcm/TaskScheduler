import request from '../utils/request'
import qs from 'qs';

export default {
    //获取命令列表信息
    getConfigs : (taskid)=>{
        return request({
            url: 'TaskConfig/' + taskid,
            method: 'get'
            //params: query
        });
    },
    //添加命令
    addConfig: (params) =>{
        return request({
            url: 'TaskConfig',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'post',
            data: qs.stringify(params)
        })
    },
    //删除配置
    delConfig: (tcid)=>{
        return  request({
            url: 'TaskConfig',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'delete',
            data:qs.stringify({'tcid': tcid})
        })
    }
}