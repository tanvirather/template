import { createRouter, createWebHistory } from 'vue-router'
import Identity_User from '../views/Identity/User.vue'
import Login from '../views/Login.vue'
import PostgresType from '../views/PostgresType.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    // { path: '/', name: 'home', component: Login },
    { path: '/', name: 'home', component: Identity_User },
    { path: '/Login', name: 'Login', component: Login },
    { path: '/Identity/User', name: 'Identity_User', component: Identity_User },
    { path: '/PostgresType', name: 'PostgresType', component: PostgresType },
  ],
})

export default router
