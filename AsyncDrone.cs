using Spectre.Console;

namespace DroneDelivery
{
    public class AsyncDrone
    {
        public async Task RunAsyncMission(
            List<Checkpoint> route,
            ControlTower tower,
            string droneId
        )
        {
            double currentLat = 60.39;
            double currentLng = 5.32;

            AnsiConsole.MarkupLine(
                $"[bold darkblue]{droneId}[/][bold green]: Initializing engines...[/]"
            );

            foreach (var point in route)
            {
                double distance = tower.CalculateDistance(
                    currentLat,
                    currentLng,
                    point.Lat,
                    point.Lng
                );
                int flightTimeInMs = (int)distance > 1000 ? (int)distance : 1000;

                if (!point.IsSafe)
                {
                    AnsiConsole.MarkupLine(
                        $"[bold darkblue]{droneId}[/]: [red][[HAZARD]][/] High winds at {point.Name} ([red]{point.Wind} m/s[/]). Skipping checkpoint."
                    );
                    continue;
                }

                AnsiConsole.MarkupLine(
                    $"[bold darkblue]{droneId}[/]: [blue][[EN ROUTE]][/] To [white]{point.Name}[/]. ETA: [yellow]{flightTimeInMs / 1000}s[/]."
                );

                await Task.Delay(flightTimeInMs);

                AnsiConsole.MarkupLine(
                    $"[bold darkblue]{droneId}[/]: [bold green][[ARRIVED]][/] Docked at {point.Name}. Updating telemetry..."
                );

                await Task.Delay(1000);
                //So next checkpoint starts from previous checkpoint's location'
                currentLat = point.Lat;
                currentLng = point.Lng;
            }

            AnsiConsole.MarkupLine(
                $"[bold darkblue]{droneId}[/]:[bold green] Delivery Complete![/]"
            );
        }
    }
}
