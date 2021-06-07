<template>
    <div>
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item>
                    <i class="el-icon-lx-cascades"></i> 任务列表
                </el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="container">
            <div class="handle-box">
                <!-- <el-button
                    type="primary"
                    icon="el-icon-delete"
                    class="handle-del mr10"
                    @click="delAllSelection"
                >批量删除</el-button> -->
                <!-- <el-select v-model="query.address" placeholder="地址" class="handle-select mr10">
                    <el-option key="1" label="广东省" value="广东省"></el-option>
                    <el-option key="2" label="湖南省" value="湖南省"></el-option>
                </el-select> -->
                <el-input v-model="query.name" placeholder="任务名称" class="handle-input mr10"></el-input>
                <el-button type="primary" icon="el-icon-search" @click="handleSearch">搜索</el-button>
                <el-button type="primary" icon="el-icon-add" @click="taskAddShow">添加</el-button>
            </div>
            <el-table
                :data="tableData"
                border
                class="table"
                ref="multipleTable"
                header-cell-class-name="table-header"
                @selection-change="handleSelectionChange"
            >
                <!-- <el-table-column type="selection" width="55" align="center"></el-table-column> -->
                <el-table-column prop="id" label="ID" width="55" align="center"></el-table-column>
                <el-table-column prop="name" label="任务名称"></el-table-column>
                <!-- <el-table-column prop="taskGuid" label="全局ID"></el-table-column> -->
                <el-table-column label="任务状态" align="center">
                    <template #default="scope">
                        <el-tag
                            :type="
                                scope.row.isRuning?'success':''
                            "
                        >{{ scope.row.isRuning?'运行中':'已停止' }}</el-tag>
                    </template>
                </el-table-column>
                <el-table-column prop="updateTime" label="更新时间"></el-table-column>
                <el-table-column prop="writeTime" label="创建时间"></el-table-column>
                <el-table-column label="操作" width="260" align="center">
                    <template #default="scope">
                        <el-button
                            type="text"
                            icon="el-icon-lx-text"
                            @click="taskLog(scope.row)"
                        >日志</el-button>

                        <el-button
                            type="text"
                            icon="el-icon-circle-close"
                            @click="taskKill(scope.row)"
                            v-if="scope.row.isRuning"
                        >停止</el-button>

                        <el-button
                            type="text"
                            icon="el-icon-caret-right"
                            @click="taskRun(scope.row)"
                            v-if="!scope.row.isRuning"
                        >运行</el-button>
      

                        <el-button
                            type="text"
                            icon="el-icon-edit"
                            @click="taskUpdateShow(scope.row)"
                        >更新</el-button>
                        <el-button
                            type="text"
                            icon="el-icon-delete"
                            class="red"
                            @click="handleDelete(scope.$index, scope.row)"
                        >删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <div class="pagination">
                <el-pagination
                    background
                    layout="total, prev, pager, next"
                    :current-page="query.pageIndex"
                    :page-size="query.pageSize"
                    :total="pageTotal"
                    @current-change="handlePageChange"
                ></el-pagination>
            </div>
        </div>

        <!-- 添加任务弹出框 -->
        <el-dialog title="添加任务" v-model="addVisible" width="30%">
            <el-form ref="form" id="addform" :model="addForm" label-width="70px">
                <el-form-item label="任务名称">
                    <el-input name="name" v-model="addForm.name"></el-input>
                </el-form-item>
                <el-form-item label="任务程序">
                    <el-input name="file" type="file" v-model="addForm.file"></el-input>
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="addVisible = false">取 消</el-button>
                    <el-button type="primary" @click="taskAdd">确 定</el-button>
                </span>
            </template>
        </el-dialog>

        <!-- 更新任务弹出框 -->
        <el-dialog title="更新任务" v-model="updateVisible" width="30%">
            <el-form ref="updateform" id="updateform" :model="updateForm" label-width="70px">
                <input name="id" v-model="updateForm.id" style="display:none"/>
                <el-form-item label="任务程序">
                    <el-input name="file" type="file" v-model="updateForm.file"></el-input>
                </el-form-item>
            </el-form>
            <template #footer>
                <span class="dialog-footer">
                    <el-button @click="updateVisible = false">取 消</el-button>
                    <el-button type="primary" @click="taskUpdate">确 定</el-button>
                </span>
            </template>
        </el-dialog>

        <!-- 任务日志 -->
        <el-dialog title="任务日志" v-model="logVisible" width="50%" :before-close="closeLogList">
            <el-container class="tableLogSection">
            <el-table
                :data="logList"
                :row-key="(row)=> { return row.id}"
                class="table"
                :show-header="false"
                :row-style="taskRowClass"
                >
                <el-table-column prop="message" ></el-table-column>
                <el-table-column prop="writeTime" width="200" label="创建时间"></el-table-column>
            </el-table>
            </el-container>
        </el-dialog>
    </div>
</template>

<script>
import { addTask, deleteTask, fetchData, getLogList, killTask, runTask, updateTask } from "../api/index";
export default {
    name: "tasklist",
    data() {
        return {
            ticker : {},
            tickerLog:{},
            query: {
                name: "",
                pageIndex: 1,
                pageSize: 10
            },
            tableData: [],
            multipleSelection: [],
            delList: [],
            updateVisible: false,
            logVisible: false,
            addVisible: false,
            pageTotal: 0,
            updateForm: {
                id:0,
                file:""
            },
            addForm:{
                name:"",
                file: "",
            },
            logList:[],
            idx: -1,
            id: -1
        };
    },
    created() {
        this.getData();
        var that = this;
        this.ticker = setInterval(()=>{
            that.getData();
        },2000)
    },
    methods: {
        getData() {
            fetchData(this.query).then(res => {
                console.log(res);
                this.tableData = res.data.list;
                this.pageTotal = res.data.total || 50;
            });
        },
        // 触发搜索按钮
        handleSearch() {
            this.query.pageIndex = 1;
            //this.$set(this.query, "pageIndex", 1);
            this.getData();
        },
        // 删除操作
        handleDelete(index, row) {
            // 二次确认删除
            this.$confirm("确定要删除吗？", "提示", {
                type: "warning"
            })
                .then(() => {
                    deleteTask({id:row.id}).then(res=>{
                        if(res.code == 0){
                            this.$message.success(res.msg);
                            this.tableData.splice(index, 1);
                        }else{
                            this.$message.error(res.msg);
                        }
                    })
                    
                })
                .catch(() => {});
        },

        //运行任务
        taskRun(row){
            runTask({id:row.id}).then(res=>{
                if(res.code == 0){
                    this.$message.success(res.msg);
                }else{
                    this.$message.error(res.msg);
                }
            })
            this.getData();
        },

        //停止任务
        taskKill(row){
            killTask({id:row.id}).then(res=>{
                if(res.code == 0){
                    this.$message.success(res.msg);
                }else{
                    this.$message.error(res.msg);
                }
            })
            this.getData();
        },
        // 更新显示
        taskUpdateShow(row) {
            this.updateForm.id = row.id;
            this.updateForm.file = "";
            this.updateVisible = true;
        },
         // 更新任务
        taskUpdate() {
            var formData = new FormData(document.getElementById("updateform"));
            updateTask(formData).then(res=>{
               if(res.code == 0){
                    this.$message.success(res.msg);
                    this.updateVisible = false;
                }else{
                    this.$message.console.error(res.msg);
                }
            })
        },
        taskAddShow(){
            this.addVisible = true;
        },
        //添加任务
        taskAdd(){
            var formData = new FormData(document.getElementById("addform"));
            addTask(formData).then(res=>{
               if(res.code == 0){
                    this.$message.success(res.msg);
                    this.addVisible = false;
                }else{
                    this.$message.error(res.msg);
                }
            })
        },
        // 任务编辑
        taskEdit() {
            this.editVisible = false;
            var formData = new FormData(document.getElementById("editform"));
            addTask(formData).then(res=>{
                if(res.code == 0){
                    this.$message.success(res.msg);
                }else{
                    this.$message.console.error(res.msg);
                }
            })
            
        },
        // 显示日志
        taskLog(row){
            this.logVisible = true;
            var that = this;
            getLogList({taskId:row.id, page:1, pageSize:100}).then((res)=>{
                that.logList = res.data.list;
            })
            this.tickerLog = setInterval(()=>{
                    getLogList({taskId:row.id, page:1, pageSize:100}).then((res)=>{
                    that.logList = res.data.list;
                })
            },2000)
        },
        //日志行列
        taskRowClass(row){

            if(row.row.level == 0 ){
                return {"background": "rgba(255, 0, 0, 0.466)"}
            }
            if(row.row.level == 1 ){
               return {"background": "rgba(255, 255, 0, 0.466)"}
            }
            if(row.row.level == 2 ){
               return {"background": "rgba(0, 0, 255, 0.466)"}
            }
            if(row.row.level == 3 ){
                return {"background": "rgba(0, 128, 0, 0.466)"}
            }
        },
        //关闭日志
        closeLogList(done){
            clearInterval(this.tickerLog);
            done();
        },
        // 分页导航
        handlePageChange(val) {
            this.query.pageIndex = val;
            //this.$set(this.query, "pageIndex", val);
            this.getData();
        }
    },
    unmounted(){
        clearInterval(this.ticker)
        clearInterval(this.tickerLog)
    }
};
</script>

<style scoped>
.handle-box {
    margin-bottom: 20px;
}

.handle-select {
    width: 120px;
}

.handle-input {
    width: 300px;
    display: inline-block;
}
.table {
    width: 100%;
    font-size: 14px;
}
.tableLogSection {
    width: 100%;
    font-size: 14px;
    height: 500px;
    overflow: scroll;
}
.red {
    color: #ff0000;
}
.mr10 {
    margin-right: 10px;
}
.table-td-thumb {
    display: block;
    margin: auto;
    width: 40px;
    height: 40px;
}
.logRow{
    height: 20px;
}
.logList{
    height: 80%;
    overflow:hide
}

.rowRed{
    background: #ff0000;
    color: white;
}
.rowYellow{
    background:yellow;
    color: black;
}

.rowBlue{
    background: blue;
    color: white;
}

.rowGreen{
    background: green;
    color: white;
}

</style>
