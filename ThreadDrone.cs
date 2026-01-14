using Spectre.Console;

namespace DroneDelivery
{
    public class ThreadDrone
    {
        public void RunThreadedMission(List<Checkpoint> route, ControlTower tower, string droneId)
        {
            double currentLat = 60.39;
            double currentLng = 5.32;

            AnsiConsole.MarkupLine(
                $"[bold darkblue]{droneId}[/][bold green]: Initializing engines...[/]"
            );

            foreach (var point in route)
            {
                //AnsiConsole.MarkupLine($"[yellow]Checking checkpoint:[/] {point.Name}");

                double distance = tower.CalculateDistance(
                    currentLat,
                    currentLng,
                    point.Lat,
                    point.Lng
                );
                int flightTimeInMs = (int)(distance);

                if (flightTimeInMs < 1000)
                    flightTimeInMs = 1000;

                if (point.Wind > 12)
                {
                    AnsiConsole.MarkupLine(
                        $"[bold darkblue]{droneId}[/]_[red][[ALERT]][/] {point.Name} is too windy! Aborting."
                    );

                    return;
                }
                AnsiConsole.MarkupLine(
                    $"[blue][[FLYING]][/]Weather is good_Expected flight [blue]time:{flightTimeInMs / 1000}s[/]"
                );
                AnsiConsole.MarkupLine(
                    $"[bold darkblue]{droneId}[/]_[blue][[FLYING]][/] Reached {point.Name}. Weather is good."
                );

                Thread.Sleep(flightTimeInMs);
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
