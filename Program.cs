using Spectre.Console;

namespace DroneDelivery
{
    public static class Program
    {
        private static async Task Main()
        {
            var keepRunning = true;

            while (keepRunning)
            {
                Console.Clear();

                try
                {

                    var options = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Options")
                            .WrapAround()
                            .AddChoices(
                                "[blue]Thread Drone[/]",
                                "[blue]Task drone[/]",
                                "[blue]Async Drone[/]",
                                "[red]Exit[/]"
                            )
                    );

                    if (options == "[red]Exit[/]")
                    {
                        keepRunning = false;
                        continue; // Skips the switch and exits the loop
                    }

                    AnsiConsole.MarkupLine($"You selected [bold blue]{options}[/]");

                    var selector = new SwitchSelection();

                    Task selectedTask = options switch
                    {
                        "[blue]Thread Drone[/]" => selector.RunDroneThreads(),
                        "[blue]Task drone[/]" => selector.RunDroneTasks(),
                        "[blue]Async Drone[/]" => selector.RunDroneAsync(),
                        _ => Task.CompletedTask
                    };
                    await selectedTask;
                }
                catch (Exception ex)
                {
                    if (ex.Data.Contains("WindSpeed"))
                    {
                        AnsiConsole.MarkupLine($"[bold red]CRITICAL FAILURE:[/] Dangerous winds detected at [bold blue][[{ex.Data["Location"]}]][/].");
                        AnsiConsole.MarkupLine($"[bold yellow]WIND SPEED:[/] {ex.Data["WindSpeed"]} m/s (Limit: 12 m/s)");
                    }
                    
                    AnsiConsole.MarkupLine($"\n[Orange1]System Message:[/] [bold steelblue]{ex.Message}[/]");
                }

                AnsiConsole.MarkupLine("\n[grey]Press any key to return to the menu..[/]");
                Console.ReadKey(true);
            }
        }
    }
}
