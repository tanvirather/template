#!/bin/bash
DB_HOST="localhost"
DB_PORT="5432"
DB_USER="postgres"
DB_PASSWORD="P@ssw0rd"
DB_NAME=product

export PGPASSWORD=$DB_PASSWORD
dropdb --host=$DB_HOST --port=$DB_PORT --username=$DB_USER --force --if-exists $DB_NAME
createdb --host=$DB_HOST --port=$DB_PORT --username=$DB_USER $DB_NAME
psql --host=$DB_HOST --port=$DB_PORT --username=$DB_USER -d $DB_NAME -c "create extension pgcrypto;"
psql --host=$DB_HOST --port=$DB_PORT --username=$DB_USER -d $DB_NAME -c "CREATE SCHEMA IF NOT EXISTS product;"
psql --host=$DB_HOST --port=$DB_PORT --username=$DB_USER -d $DB_NAME -f "product/numeric_type.table.sql"
unset PGPASSWORD
