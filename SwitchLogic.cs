using Spectre.Console;

namespace DroneDelivery
{
    public class SwitchSelection
    {
        private ControlTower _tower = new ControlTower();

        public async Task RunDroneThreads()
        {
            List<Thread> droneThread = new List<Thread>();
            int numberOfDrones = 3;

            for (int i = 1; i <= numberOfDrones; i++)
            {
                string droneId = $"Drone-{i}";
                var drone = new ThreadDrone();
                List<Checkpoint> missionRoute = await _tower.GetDroneRoute();
                Thread t = new Thread(() =>
                    drone.RunThreadedMission(missionRoute, _tower, droneId)
                );

                droneThread.Add(t);
                t.Start();

                Console.WriteLine($"{droneId} has been deployed!");
            }

            /*
              List<Checkpoint> missionRoute = await _tower.GetDroneRoute();
  
              var drone1 = new ThreadDrone();
              var drone2 = new ThreadDrone();
  
              Thread droneThread1 = new Thread(() => drone1.RunThreadedMission(missionRoute, _tower));
              Thread droneThread2 = new Thread(() => drone1.RunThreadedMission(missionRoute, _tower));
  
              droneThread1.Start();
              droneThread2.Start();
  
              droneThread1.Join();
              droneThread2.Join();*/

            AnsiConsole.MarkupLine($"[yellow]All drones have now returned from mission[/]");
        }
    }
}
