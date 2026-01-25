import { createRouter, createWebHistory } from 'vue-router'
import Login from '../pages/Login.vue'
import PostgresType from '../pages/PostgresType.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', name: 'home', component: Login },
    { path: '/Login', name: 'Login', component: Login },
    { path: '/PostgresType', name: 'PostgresType', component: PostgresType },
  ],
})

export default router
