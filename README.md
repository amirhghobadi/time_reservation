# تنظیمات پایگاه داده

تنظیمات مربوط به پایگاه داده درون فایل `program.cs` به صورت زیر است:

```csharp
builder.Services.AddDbContext<TimeReservationContext>(options =>
{
    options.UseSqlServer("Data Source =DESKTOP-AVK98P9;Initial Catalog=TimeReservationDB;Integrated Security=true;Trust Server Certificate=true");
});
```

DESKTOP-AVK98P9 is server name in SQL server management studio

پروژه رو که باز کردید پوشه migrations رو پاک کنید بعد در قسمت Package Manager Console یک migrations ایجاد کنید 
مثلا : Add-Migration init
سپس دستور Update-Database رو اجرا کنید.
