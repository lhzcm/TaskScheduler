import request from '../utils/request';
import qs from 'qs';


export default {
    taskList : query => {
        return request({
            url: 'Task/'+ query.pageIndex + '/' + query.pageSize + (query.name.trim() == '' ? '':('/'+query.name.trim())),
            method: 'get'
            //params: query
        });
    },
    addTask : query => {
        return request({
            url: 'Task/Add',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'post',
            data: query//qs.stringify(query)
        });
    },
    updateTask : query => {
        return request({
            url: 'Task',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'patch',
            data: query//qs.stringify(query)
        }
    )},
    deleteTask : query => {
        return request({
            url: 'Task',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'delete',
            data: qs.stringify(query)
        });
    },
    runTask : query => {
        return request({
            url: 'Task/Run',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'post',
            data: qs.stringify(query)
        });
    },
    killTask : query => {
        return request({
            url: 'Task/Kill',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
              },
            method: 'post',
            data: qs.stringify(query)
        });
    },
    getLogList : query => {
        return request({
            url: 'Log/'+ query.taskId + '/' + query.page + '/' + query.pageSize,
            method: 'get'
        });
    }
};
