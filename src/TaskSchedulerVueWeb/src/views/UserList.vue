<template>
  <div>
    <div class="crumbs">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item>
          <i class="el-icon-lx-cascades"></i> 用户列表
        </el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <div class="container">
      <div class="handle-box">
        <el-input
          v-model="query.userId"
          placeholder="用户ID"
          class="handle-input mr10"
        ></el-input>
        <el-button type="primary" icon="el-icon-search" @click="handleSearch"
          >搜索</el-button
        >
        <el-button type="primary" icon="el-icon-add" @click="userAddShow"
          >添加</el-button
        >
      </div>
      <el-table
        :data="tableData"
        border
        class="table"
        ref="multipleTable1"
        header-cell-class-name="table-header"
      >
        <!-- <el-table-column type="selection" width="55" align="center"></el-table-column> -->

        <el-table-column prop="id" label="用户ID"></el-table-column>
        <el-table-column prop="name" label="昵称"></el-table-column>
        <el-table-column label="管理员">
          <template #default="scope">
            <el-tag>{{ scope.row.super ? "是" : "否" }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="380" align="center">
          <template #default="scope">
            <el-button
              type="text"
              icon="el-icon-set-up"
              @click="taskManageShow(scope.row)"
              >权限管理</el-button
            >
            <el-button
              type="text"
              icon="el-icon-refresh"
              @click="userUpdateShow(scope.row)"
              >更新</el-button
            >
            <el-button
              type="text"
              icon="el-icon-delete"
              class="red"
              @click="handleDelete(scope.$index, scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
    </div>

    <!-- 添加任务弹出框 -->
    <el-dialog title="添加用户" v-model="addVisible" width="30%">
      <el-form ref="form" id="addform" :model="addForm" label-width="70px">
        <el-form-item label="用户昵称">
          <el-input name="name" v-model="addForm.name"></el-input>
        </el-form-item>
        <el-form-item label="密码">
          <el-input name="password" v-model="addForm.password"></el-input>
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="addVisible = false">取 消</el-button>
          <el-button type="primary" @click="userAdd">确 定</el-button>
        </span>
      </template>
    </el-dialog>
    <!-- 更新任务弹出框 -->
    <el-dialog title="更新用户" v-model="updateVisible" width="50%">
      <el-form
        ref="updateform"
        id="updateform"
        :model="updateForm"
        label-width="100px"
      >
        <input name="id" v-model="updateForm.id" style="display: none" />
        <el-form-item label="用户昵称">
          <el-input name="name" v-model="updateForm.name"></el-input>
        </el-form-item>
        <el-form-item label="密码">
          <el-input name="password" v-model="updateForm.password"></el-input>
        </el-form-item>
        <el-form-item label="超级管理员">
          <el-input name="super" v-model="updateForm.super"></el-input>
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="updateVisible = false">取 消</el-button>
          <el-button type="primary" @click="userUpdate">确 定</el-button>
        </span>
      </template>
    </el-dialog>

    <!-- 任务权限 -->
    <el-dialog title="任务权限列表" v-model="taskManageVisiable" width="1000px">
      <el-button
        type="primary"
        icon="el-icon-add"
        @click="addTaskManageVisible = true"
        >添加</el-button
      >
      <el-container class="tableTaskManage">
        <el-table
          :data="taskManageList"
          border
          class="table"
          ref="multipleTable"
          header-cell-class-name="table-header"
        >
          <el-table-column
            prop="taskId"
            label="任务ID"
            width="80"
            align="center"
          ></el-table-column>
          <el-table-column prop="taskName" label="任务名称"></el-table-column>
          <el-table-column prop="access" label="权限" width="500">
            <template #default="">
              <span
                style="
                  background: #409eff;
                  color: white;
                  text-align: center;
                  padding: 5px;
                "
                >查看任务</span
              >
              <span
                style="
                  background: #409eff;
                  color: white;
                  text-align: center;
                  padding: 5px;
                "
                >添加任务</span
              >
              <span
                style="
                  background: #409eff;
                  color: white;
                  text-align: center;
                  padding: 5px;
                "
                >删除任务</span
              >
              <span
                style="
                  background: #409eff;
                  color: white;
                  text-align: center;
                  padding: 5px;
                "
                >运行任务</span
              >
              <span
                style="
                  background: #409eff;
                  color: white;
                  text-align: center;
                  padding: 5px;
                "
                >更新任务</span
              >
              <span
                style="
                  background: #409eff;
                  color: white;
                  text-align: center;
                  padding: 5px;
                "
                >操作命令</span
              >
              <span
                style="
                  background: #409eff;
                  color: white;
                  text-align: center;
                  padding: 5px;
                "
                >操作配置</span
              >
            </template>
          </el-table-column>
          <el-table-column label="操作" width="180" align="center">
            <template #default="scope">
              <el-button
                type="text"
                icon="el-icon-delete"
                class="red"
                @click="delConfig(scope.row)"
                >删除</el-button
              >
            </template>
          </el-table-column>
        </el-table>
      </el-container>
    </el-dialog>
  </div>
</template>

<script>
import userApi from "../api/user";
import taskManageApi from "../api/taskManage";

export default {
  name: "tasklist",
  data() {
    return {
      tableData: [],
      query: {
        userId: "",
      },
      addVisible: false,
      addForm: {
        name: "",
        password: "",
      },

      updateVisible: false,
      updateForm: {
        id: 0,
        name: "",
        password: "",
        super: false,
      },
      //任务权限
      taskManageVisiable: false,
      queryManage: {
        userId: 0,
        pageIndex: 1,
        pageSize: 10,
      },
      taskManageList: [],
      addTaskManageVisible: false,
      configAddForm: {
        key: "",
        value: "",
        taskId: 0,
      },
      access: [
        {
          code: 1,
          name: "添加任务",
        },
        {
          code: 2,
          name: "更新任务",
        },
        {
          code: 4,
          name: "删除任务",
        },
        {
          code: 8,
          name: "运行任务",
        },
        {
          code: 16,
          name: "操作命令",
        },
        {
          code: 32,
          name: "操作配置",
        },
        {
          code: 64,
          name: "查看任务",
        },
      ],
    };
  },
  created() {
    this.getData();
  },
  methods: {
    getData() {
      let that = this;
      userApi.userList(this.query).then((res) => {
        if (res.data.list != null) that.tableData = res.data.list;
      });
    },
    // 触发搜索按钮
    handleSearch() {
      this.getData();
    },
    userAddShow() {
      this.addVisible = true;
    },
    //添加任务
    userAdd() {
      var formData = new FormData(document.getElementById("addform"));
      userApi.addUser(formData).then((res) => {
        if (res.code == 0) {
          this.$message.success(res.msg);
          this.addVisible = false;
        } else {
          this.$message.error(res.msg);
        }
      });
    },
    // 更新显示
    userUpdateShow(row) {
      this.updateForm.name = row.name;
      this.updateForm.password = row.password;
      this.updateForm.super = row.super;
      this.updateVisible = true;
    },
    // 更新任务
    userUpdate() {
      var formData = new FormData(document.getElementById("updateform"));
      userApi.updateUser(formData).then((res) => {
        if (res.code == 0) {
          this.$message.success(res.msg);
          this.updateVisible = false;
        } else {
          this.$message.error(res.msg);
        }
      });
    },
    // 删除操作
    handleDelete(index, row) {
      // 二次确认删除
      this.$confirm("确定要删除用户" + row.id + "吗？", "提示", {
        type: "warning",
      })
        .then(() => {
          userApi.deleteUser({ userId: row.id }).then((res) => {
            if (res.code == 0) {
              this.$message.success(res.msg);
              this.tableData.splice(index, 1);
            } else {
              this.$message.error(res.msg);
            }
          });
        })
        .catch(() => {});
    },
    //显示配置列表
    taskManageShow(row) {
      var that = this;
      that.taskManageVisiable = true;
      that.taskManageList = [];
      // that.configAddForm.taskId = row.id
      this.queryManage.userId = row.id;
      taskManageApi.getTaskManageList(this.queryManage).then((res) => {
        that.taskManageList = res.data.list;
      });
    },
  },
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
.logRow {
  height: 20px;
}
.logList {
  height: 80%;
  overflow: hide;
}

.rowRed {
  background: #ff0000;
  color: white;
}
.rowYellow {
  background: yellow;
  color: black;
}

.rowBlue {
  background: blue;
  color: white;
}

.rowGreen {
  background: green;
  color: white;
}
</style>