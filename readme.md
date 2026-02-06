# Development environmetn setup
- Run the file `scripts.sh` with following functions to build and deploy the databases
```sh
rebuild_postgres_server
solution_initilize
recreate_database "Identity"
update_database "Identity"
```

# Run Project
```sh
cd Identity && clear && dotnet run
cd web.vue && clear && npm run dev
```
