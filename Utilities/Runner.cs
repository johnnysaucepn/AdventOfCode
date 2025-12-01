using Spectre.Console;

namespace Utilities;

public static class Runner
{
    public static void Run<T>(string title, Func<T> action)
    {
        TextWriter defaultStream = Console.Out;

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        T result = action();
        Console.SetOut(defaultStream);

        AnsiConsole.Write(new Rule(title));
        var escaped = stringWriter.ToString().EscapeMarkup();
        AnsiConsole.MarkupLine($"[grey]{escaped}[/]");
        AnsiConsole.Write(new Rule("Result"));
        AnsiConsole.MarkupLine($"[green]{result}[/]");
        AnsiConsole.Write(new Rule());
    }
}
