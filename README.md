# Todo API

Simple dotnet CRUD todo api

## Usage

```bash
git clone git@github.com:brysonbw/dotnet-todo-api.git SimpleTodoApi && rm -rf SimpleTodoApi/.git
```

```bash
cd SimpleTodoApi
```

## Enable and set [secret storage](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0) for db connection string

> Note: Using SQL Server - see [install instructions](https://learn.microsoft.com/en-us/sql/database-engine/install-windows/install-sql-server?view=sql-server-ver17). Or use another 'flavor' of SQL of your choice. Also, secret storage for development purposes only

`Enable`

```bash
dotnet user-secrets init
```

`Set Key/Values`

```bash
dotnet user-secrets set "DbHost" "<DB_HOST_VALUE>" && dotnet user-secrets set "DbUser" "<DB_USER_VALUE>" && dotnet user-secrets set "DbPassword" "<DB_PASSWORD_VALUE>" && dotnet user-secrets set "Database" "<DB_NAME_VALUE>"
```

## Apply db migrations

```bash
dotnet ef database update
```

## Start server

```bash
dotnet watch run --launch-profile https
```

## Api Doc(s)

- Swagger:
  - Available at: `https://localhost:<port>/swagger`
- Redoc:
  - Available at: `https://localhost:<port>/redoc`
