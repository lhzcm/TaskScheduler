import request from '../utils/request';

export default {
    getTaskManageList : query => {
        return request({
            url: 'TaskManage/'+ query.pageIndex + '/' + query.pageSize + '/'+query.userId,
            method: 'get'
            //params: query
        });
    },
}