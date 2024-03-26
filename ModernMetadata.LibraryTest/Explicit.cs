using System.Reflection;

namespace ModernMetadata.LibraryTest;

public static class Explicit
{
    public static void Method()
    {
        Assembly assembly = Assembly.LoadFrom(@"C:\Users\maksg\Desktop\CODING\methodsandtech\ModernMetadata\ModernMetadata.Library\bin\Debug\net8.0\ModernMetadata.Library.dll");
        Type type = assembly.GetTypes().First(type => type.Name == "MenuItemData");
        object? obj = Activator.CreateInstance(type, ["DLL TEST", "DLL METHOD TEST"]);
        Console.WriteLine($"{type.GetProperty("Method")?.GetValue(obj)} + {type.GetProperty("Name")?.GetValue(obj)}");
    }
}