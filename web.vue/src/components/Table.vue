<script setup>
/************************* Props *************************/
const props = defineProps({
  columns: { type: Array, default: [] },  
  rows: { type: Array, default: [] },  
});

/************************* emits *************************/
/************************* computed *************************/
/************************* functions *************************/
</script>

<!-------------------------------------------------- template -------------------------------------------------->
<template>
  <table>
    <thead>
      <tr>
        <th v-for="col in columns" :key="col.key">{{ col.title }}</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="row in rows" :key="row.id">
        <td v-for="col in columns" :key="col.key">{{ row[col.key] }}</td>
      </tr>
    </tbody>
  </table>
</template>

<!-------------------------------------------------- style -------------------------------------------------->
<style scoped>
</style>
