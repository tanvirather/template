import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import PostgresType from '../views/PostgresType.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', name: 'home', component: Login },
    { path: '/Login', name: 'Login', component: Login },
    { path: '/PostgresType', name: 'PostgresType', component: PostgresType },
  ],
})

export default router
