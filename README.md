# تنظیمات پایگاه داده

تنظیمات مربوط به پایگاه داده درون فایل `program.cs` به صورت زیر است:

```csharp
builder.Services.AddDbContext<TimeReservationContext>(options =>
{
    options.UseSqlServer("<span style='color: red;'>Data Source =DESKTOP-AVK98P9</span>;Initial Catalog=TimeReservationDB;Integrated Security=true;Trust Server Certificate=true");
});
