<script setup>
/************************* Props *************************/

const columns = [
  { key: 'name', title: 'Name' },
  { key: 'age', title: 'Age' },
  { key: 'email', title: 'Email' },
]

const users = [
  { id: 1, name: 'Ada Lovelace', email: 'ada@history.dev', age: 36, createdAt: '2021-01-31' },
  { id: 2, name: 'Grace Hopper', email: 'grace@navy.mil', age: 85, createdAt: '2020-03-12' },
];

/************************* emits *************************/
/************************* computed *************************/
/************************* functions *************************/
</script>

<!-------------------------------------------------- template -------------------------------------------------->
<template>
  <Table :columns="columns" :rows="users"/>
</template>

<!-------------------------------------------------- style -------------------------------------------------->
<style scoped>
td {
  border: 1px solid #d0d0d0;
}
</style>
