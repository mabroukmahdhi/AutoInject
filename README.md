# AutoInject - Simplified Dependency Injection for .NET

AutoInject is a lightweight and efficient **dependency injection automation library** for .NET. It eliminates the need for manual service registration by automatically discovering and injecting services using custom attributes.

[![Nuget](https://img.shields.io/nuget/v/Campsis.AutoInject?logo=nuget&color=blue)](https://www.nuget.org/packages/Campsis.AutoInject)
![Nuget](https://img.shields.io/nuget/dt/Campsis.AutoInject?logo=nuget&color=blue&label=Downloads)


## 🚀 Features
- **Automatic Service Registration**: Registers **Singleton**, **Scoped**, and **Transient** services dynamically.
- **Supports Keyed Dependencies**: Inject services based on custom keys.
- **Minimal Configuration**: Just add `[Singleton]`, `[Scoped]`, or `[Transient]` attributes.
- **Seamless Integration**: Works with .NET's built-in `IServiceCollection`.

---

## 📦 Installation

**.NET CLI**

```sh
dotnet add package Campsis.AutoInject
```

Or **Package Manager Console**:

```sh
Install-Package Campsis.AutoInject
```

---

## 🔧 Usage
### 1️⃣ **Add AutoInject to Your DI Container**
Call `UseAutoInjection()` in `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.UseAutoInjection();

var app = builder.Build();
app.Run();
```

Or specify a **target assembly**:
```csharp
builder.Services.UseAutoInjection(typeof(MyService).Assembly);
```

### 2️⃣ **Annotate Services with Attributes**

Define your services with `[Singleton]`, `[Scoped]`, or `[Transient]` attributes:

```csharp
[Singleton(typeof(IMyService))]
public class MyService : IMyService { }

[Scoped(typeof(IRepository))]
public class Repository : IRepository { }

[Transient(typeof(ILogger))]
public class Logger : ILogger { }
```

### 3️⃣ **Keyed Injection**
AutoInject supports **keyed dependency injection**:

```csharp
[Singleton(typeof(IStorageBroker), "SQL")]
    public class SqlStorageBroker : IStorageBroker
    {
        public async ValueTask<string> InsertStudentAsync(string student)
        {
            await Task.Delay(10); // Simulate async operation
            return $"Student {student} inserted sql.";
        }
    }

    [Singleton(typeof(IStorageBroker), "MONGO")]
    public class MongoStorageBroker : IStorageBroker
    {
        public async ValueTask<string> InsertStudentAsync(string student)
        {
            await Task.Delay(10); // Simulate async operation
            return $"Student {student} inserted to mongo.";
        }
    }
```

Then resolve the service by key:
```csharp
var cache = serviceProvider.GetRequiredKeyedService<ICacheService>("Redis");
```

Or consume the services in other classes:
```csharp
[Singleton(typeof(IStudentService))]
public class StudentService(
    [FromKeyedServices("SQL")] IStorageBroker sqlStorageBroker,
    [FromKeyedServices("MONGO")] IStorageBroker mongoStorageBroker) : IStudentService
{
    public ValueTask<string> InsertStudentIntoMongoAsync(string student) =>
        mongoStorageBroker.InsertStudentAsync(student);

    public ValueTask<string> InsertStudentIntoSQLAsync(string student) =>
        sqlStorageBroker.InsertStudentAsync(student);
}
```
---

## 🔍 How It Works
1. **Scans the assembly** for classes with `[Singleton]`, `[Scoped]`, or `[Transient]` attributes.
2. **Registers services** in the DI container with their appropriate lifetimes.
3. **Supports keyed registrations** if a `WithKey` parameter is provided.

---

## 📌 Notes
- By default, `UseAutoInjection()` scans **the calling assembly**.
- Use `UseAutoInjection(Assembly assembly)` to target a specific assembly.
- Make sure your service interfaces are **correctly referenced** in attributes.

---

## 📄 License
AutoInject is licensed under the **MIT License**.

---

## 👥 Contributing
We welcome contributions! Feel free to submit issues or pull requests.

---

## 💬 Need Help?
- **GitHub Issues**: Report bugs and feature requests.
- **Discussions**: Share ideas and improvements.

Happy coding! 🚀

