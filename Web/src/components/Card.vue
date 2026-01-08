<script setup>
/************************************************** Props **************************************************/
const props = defineProps({
  title: { type: String, default: "" },
});

/************************************************** Emits **************************************************/

/************************************************** Computed **************************************************/

/************************************************** functions **************************************************/
</script>

<!-------------------------------------------------- template -------------------------------------------------->
<template>
  <div class="card">
    <header>{{ title }}</header>
    <main><slot /></main>
    <footer></footer>
  </div>
</template>

<!-------------------------------------------------- style -------------------------------------------------->
<style scoped>
.card {
  display: flex;
  flex-direction: column;
  gap: 6px;
  border: 1px solid #d0d0d0;
  border-radius: 6px;
  padding: 16px;
}

.card header {
  font-weight: bold;
}

.card main {
  margin-top: 8px;
}

.card footer {
  margin-top: auto;
}
</style>
