import { createRouter, createWebHashHistory } from 'vue-router'
import Home from "../views/Home.vue";

const routes = [
    {
        path: '/',
        redirect: '/TaskList'
    }, {
        path: "/",
        name: "Home",
        component: Home,
        children: [
            {
                path: "/TaskList",
                name: "TaskList",
                meta: {
                    title: '任务列表'
                },
                component: () => import(
                    /* webpackChunkName: "table" */
                    "../views/TaskList.vue")
            }, {
                path: "/UserList",
                name: "UserList",
                meta: {
                    title: '用户列表'
                },
                component: () => import(
                    /* webpackChunkName: "table" */
                    "../views/UserList.vue")
            }, {
                path: '/404',
                name: '404',
                meta: {
                    title: '找不到页面'
                },
                component: () => import(/* webpackChunkName: "404" */
                    '../views/404.vue')
            }, {
                path: '/403',
                name: '403',
                meta: {
                    title: '没有权限'
                },
                component: () => import(/* webpackChunkName: "403" */
                    '../views/403.vue')
            }
        ]
    }, {
        path: "/login",
        name: "Login",
        meta: {
            title: '登录'
        },
        component: () => import(
            /* webpackChunkName: "login" */
            "../views/Login.vue")
    }
];

const router = createRouter({
    history: createWebHashHistory(),
    routes
})

export default router
