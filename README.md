# Bingo

A minimal desktop application for Bingo calling. Generates random numbers between 1 and 90 until all numbers have been 'called'

### Tech stack

* WPF MVVM

### Logging

The application uses log4net for logging to files. Logs are written to the `logs/bingo.log` file in the application directory.

**Logging Levels:**
- **Debug builds**: Minimum logging level is set to DEBUG, capturing detailed debugging information
- **Release builds**: Minimum logging level is set to INFO, capturing important runtime information

**Example Usage:**

See `BingoCallerViewModel.cs` for an example of how to use logging in your classes:

```csharp
using log4net;

public class MyClass
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(MyClass));
    
    public void MyMethod()
    {
        Log.Debug("Debug message");
        Log.Info("Info message");
        Log.Warn("Warning message");
        Log.Error("Error message");
    }
}
```

**Configuration:**

The logging configuration can be modified in the `log4net.config` file. The default configuration includes:
- Rolling file appender with a maximum file size of 5MB
- Up to 10 backup files
- Console appender for development
