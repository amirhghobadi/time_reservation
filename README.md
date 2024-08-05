# تنظیمات پایگاه داده

تنظیمات مربوط به پایگاه داده درون فایل `program.cs` به صورت زیر است:

```csharp
builder.Services.AddDbContext<TimeReservationContext>(options =>
{
    options.UseSqlServer("Data Source =DESKTOP-AVK98P9;Initial Catalog=TimeReservationDB;Integrated Security=true;Trust Server Certificate=true");
});
```

DESKTOP-AVK98P9 is server name in SQL server management studio
