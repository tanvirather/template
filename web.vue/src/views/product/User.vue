<script setup>
import * as signalR from '@microsoft/signalr';
import { onMounted, ref } from 'vue';
/************************* Props *************************/
const totalViews = ref(0);

/************************* emits *************************/
/************************* computed *************************/
/************************* functions *************************/
onMounted(async () => {
  console.log('onMounted');
  // Create a connection to the SignalR hub
  var connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5103/hubs/user', {
      withCredentials: false
    })
    .configureLogging(signalR.LogLevel.Information)
    .build();
  console.log(`connection ${JSON.stringify(connection)}`);

    // start the connection
  try {
    await connection.start();
    console.log('SignalR Connected.');
  } catch (err) {
    console.error('SignalR Connection Error: ', err);
  }

  // connect to the method that the server hub will call to send updates, e.g. "myClientFunction"
  connection.on('myClientFunction', (count) => {
    console.log(`Received count from server: ${count}`);
    totalViews.value = count;
  });

  // invoke hub methods from the client, e.g. "myServerFunction"
  await connection.send('myServerFunction');
});
</script>

<!-------------------------------------------------- template -------------------------------------------------->

<template>
  <section>
    <h3>Total Views : {{ totalViews }}</h3>
  </section>
</template>


<!-------------------------------------------------- style -------------------------------------------------->
<style scoped>
</style>
