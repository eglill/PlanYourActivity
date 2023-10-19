import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Login from '@/views/identity/Login.vue'
import RegisterVue from '@/views/identity/Register.vue'
import UserGroups from '../views/group/UserGroups.vue'
import CreateGroup from '../views/group/CreateGroup.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterVue
    },
    {
      path: '/user/groups',
      name: 'user-groups',
      component: UserGroups
    },
    {
      path: '/user/add-group',
      name: 'create-group',
      component: CreateGroup
    }
  ]
})

export default router
