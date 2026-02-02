<!-------------------------------------------------- script -------------------------------------------------->
<script setup>
import { LoginStore } from '@/store';
import { inject, ref } from 'vue';

const apiClient = inject('apiClient');

const model = ref({
  username: 'admin@company.com',
  password: 'P@ssw0rd'
});

/************************* emits *************************/
/************************* computed *************************/
/************************* functions *************************/
async function login() {
  await new LoginStore(apiClient).login(model.value);
  // console.log("Login button clicked" + JSON.stringify(model.value));
}
</script>

<!-------------------------------------------------- template -------------------------------------------------->
<template>
  <div>
    <Card title="Login" saveText="Login" cancelText="" @onSubmit="login">
      <Input label="Username" v-model="model.username" />
      <Password label="Password" v-model="model.password" />
    </Card>
  </div>
</template>

<!-------------------------------------------------- style -------------------------------------------------->
<style scoped></style>
