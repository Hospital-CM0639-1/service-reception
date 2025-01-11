# Hospital-Service-Reception
- providing information about patients

### Start project
Make sure in `reception/key` is `public.key`, which refers to public key for `service-managment` for verifying JWT token signature

For starting up the project just run
```bash
docker compose up 
```
Containers listens on port `8080`

For `swagger` docs -> http://localhost:8080/swagger/index.html

Change `reception/appsettings.json` to connect to database
```bash
    "Psql": "Host=hospital-database;Port=5432;Database=hospital;Username=root;Password=root",
```

### Database generated via 
```bash
dotnet ef dbcontext scaffold --context-dir Data --output-dir Models "Host=localhost;Username=postgres;Password=postgres;Database=hospital" Npgsql.EntityFrameworkCore.PostgreSQL
```

### Database create migration via
```bash
dotnet ef migrations add --project DAL.Psql.Migrations\DAL.Psql.Migrations.csproj --startup-project reception\reception.csproj --context DataAccessLayer.Data.HospitalContext --configuration Debug Initial --output-dir ..\DAL.Psql.Migrations
```

### Database migration update via
```bash
dotnet ef database update --project DAL.Psql.Migrations\DAL.Psql.Migrations.csproj --startup-project reception\reception.csproj --context DataAccessLayer.Data.HospitalContext --configuration Debug 20241209183919_Initial
```




# reception - documentation
## Version: 1.0

### /api/v1/reception-service/info/doctor_types

#### GET
##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/info/doctors/{type}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| type | path       |             | Yes      | string |

##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/info/doctors_on_duty

#### GET
##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/info/free_bed

#### GET
##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/patient/exists

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema  |
| ---- | ---------- | ----------- | -------- | ------- |
| id   | query      |             | No       | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/patient/get

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema  |
| ---- | ---------- | ----------- | -------- | ------- |
| id   | query      |             | No       | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/patient/getall

#### GET
##### Parameters

| Name     | Located in | Description | Required | Schema  |
| -------- | ---------- | ----------- | -------- | ------- |
| Number   | query      |             | No       | integer |
| Page     | query      |             | No       | integer |
| Id       | query      |             | No       | integer |
| Surname  | query      |             | No       | string  |
| Status   | query      |             | No       | string  |
| Priority | query      |             | No       | string  |

##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/patient/create

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/patient/update

#### PUT
##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |

### /api/v1/reception-service/triage/create

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 201  | Created     |
