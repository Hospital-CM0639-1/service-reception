# Hospital-Service-Reception
- providing information about patients


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
