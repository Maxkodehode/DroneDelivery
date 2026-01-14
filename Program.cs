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

                switch (options)
                {
                    case "[blue]Thread Drone[/]":
                        var selector = new SwitchSelection();
                        await selector.RunDroneThreads();
                        break;

                    /*case "[blue]Task drone[/]":
                        var drone2 = new TaskDroneMission();
                        await drone2.StartMission(missionRoute);
                        break;
    
                    case "[blue]Async Drone[/]":
                        var drone3 = new AsyncDroneMission();
                        await drone3.StartMission(missionRoute);*/
                }

                AnsiConsole.MarkupLine("\n[grey]Press any key to return to the menu..[/]");
                Console.ReadKey(true);
            }
        }
    }
}
