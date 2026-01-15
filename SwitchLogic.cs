using Spectre.Console;

namespace DroneDelivery
{
    public class SwitchSelection
    {
        private readonly ControlTower _tower = new();

        public async Task RunDroneThreads()
        {
            List<Thread> droneThread = new List<Thread>();
            int numberOfDrones = 3;

            //place within for different delivery route and outside the for loop for the same route for all drones
            List<Checkpoint> missionRoute = await _tower.GetDroneRoute();

            for (int i = 1; i <= numberOfDrones; i++)
            {
                string droneId = $"Thread-Drone-{i}";
                var drone = new ThreadDrone();

                Thread t = new Thread(() =>
                    drone.RunThreadedMission(missionRoute, _tower, droneId)
                );

                droneThread.Add(t);
                t.Start();

                Console.WriteLine($"{droneId} has been deployed!");
            }

            foreach (Thread t in droneThread)
            {
                //t.Join();
            }
            AnsiConsole.MarkupLine($"[yellow]All drones have now returned to base[/]");
        }

        public async Task RunDroneTasks()
        {
            var droneTasks = new List<Task>();
            int numberOfDrones = 3;

            //place within the for loop for different delivery route and outside the for loop for the same route for all drones
            List<Checkpoint> missionRoute = await _tower.GetDroneRoute();

            for (int i = 1; i <= numberOfDrones; i++)
            {
                string droneId = $"Task-Drone-{i}";
                var drone = new TaskDrone();

                Task droneWork = drone.RunTaskMission(missionRoute, _tower, droneId);
                droneTasks.Add(droneWork);

                Console.WriteLine($"{droneId} has been deployed!");
            }

            try 
            {
                await Task.WhenAll(droneTasks);
                AnsiConsole.MarkupLine($"[yellow]All drones have now returned to base[/]");
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine("[bold red]Mission aborted: One or more drones suffered a critical failure.[/]");
            }
        }

        public async Task RunDroneAsync()
        {
            var droneAsync = new List<Task>();
            int numberOfDrones = 3;

            //place within the for loop for different delivery route and outside the for loop for the same route for all drones
            List<Checkpoint> missionRoute = await _tower.GetDroneRoute();

            for (int i = 1; i <= numberOfDrones; i++)
            {
                string droneId = $"Async-Drone-{i}";
                var drone = new AsyncDrone();

                Task droneWork = drone.RunAsyncMission(missionRoute, _tower, droneId);
                droneAsync.Add(droneWork);

                Console.WriteLine($"{droneId} has been deployed!");
            }

            await Task.WhenAll(droneAsync);

            AnsiConsole.MarkupLine($"[yellow]All drones have now returned to base[/]");
        }
    }
}
