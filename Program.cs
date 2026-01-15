using Spectre.Console;

namespace DroneDelivery
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                Console.Clear();

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
                    "[blue]Task drone[/]"   => selector.RunDroneTasks(), 
                    "[blue]Async Drone[/]" => selector.RunDroneAsync(),
                    _                       => Task.CompletedTask          
                };
                await selectedTask;

                AnsiConsole.MarkupLine("\n[grey]Press any key to return to the menu..[/]");
                Console.ReadKey(true);
            }
        }
    }
}
