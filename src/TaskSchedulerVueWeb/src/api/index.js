import request from '../utils/request';
import qs from 'qs';

export const fetchData = query => {
    return request({
        url: 'Task/'+ query.pageIndex + '/' + query.pageSize + (query.name.trim() == '' ? '':('/'+query.name.trim())),
        method: 'get'
        //params: query
    });
};


export const addTask = query => {
    return request({
        url: 'Task/Add',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
          },
        method: 'post',
        data: query//qs.stringify(query)
    });
};

export const updateTask = query => {
    return request({
        url: 'Task',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
          },
        method: 'patch',
        data: query//qs.stringify(query)
    });
};

export const deleteTask = query => {
    return request({
        url: 'Task',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
          },
        method: 'delete',
        data: qs.stringify(query)
    });
};

export const runTask = query => {
    return request({
        url: 'Task/Run',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
          },
        method: 'post',
        data: qs.stringify(query)
    });
};

export const killTask = query => {
    return request({
        url: 'Task/Kill',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
          },
        method: 'post',
        data: qs.stringify(query)
    });
};

export const getLogList = query => {
    return request({
        url: 'Log/'+ query.taskId + '/' + query.page + '/' + query.pageSize,
        method: 'get'
    });
};