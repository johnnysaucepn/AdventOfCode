using System.Diagnostics;
using Spectre.Console;

namespace Utilities;

public static class Runner
{
    public static void Run<T>(string title, Func<T> action)
    {
        TextWriter defaultStream = Console.Out;

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var timer = new Stopwatch();
        timer.Start();
        T result = action();
        timer.Stop();
        Console.SetOut(defaultStream);

        AnsiConsole.Write(new Rule(title) { Border = BoxBorder.Double });
        var escaped = stringWriter.ToString().EscapeMarkup();
        AnsiConsole.MarkupLine($"[grey]{escaped}[/]");
        AnsiConsole.Write(new Rule("Elapsed") { Justification = Justify.Left });
        AnsiConsole.MarkupLine($"[yellow]{timer.Elapsed.Milliseconds}ms[/]");
        AnsiConsole.Write(new Rule("Result") { Justification = Justify.Left });
        AnsiConsole.MarkupLine($"[green]{result}[/]");

    }
}
