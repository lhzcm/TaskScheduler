import request from '../utils/request'
import qs from 'qs';

export default {
    //获取命令列表信息
    getCommands : (taskid)=>{
        return request({
            url: 'TaskCommand/' + taskid,
            method: 'get'
            //params: query
        });
    },
    //添加命令
    addCommand: (params) =>{
        return request({
            url: 'TaskCommand/Add',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'post',
            data: qs.stringify(params)
        })
    },
    //发送执行命令
    sendCommand: (taskId, tcid)=>{
        return request({
            url: 'TaskCommand/Command',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'post',
            data:qs.stringify({
                'taskId': taskId,
                'tcid': tcid
            })
        })
    },
    //删除命令
    delCommand: (tcid)=>{
        return  request({
            url: 'TaskCommand',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'delete',
            data:qs.stringify({'tcid': tcid})
        })
    }
}