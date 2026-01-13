namespace DroneDelivery
{
    public class ThreadDrone
    {
        public void RunThreadedMission(List<Checkpoint> route, ControlTower tower)
        {
            double currentLat = 60.39;
            double currentLng = 5.32;

            Console.WriteLine("Thread Drone: Initializing engines...");

            foreach (var point in route)
            {
                double distance = tower.CalculateDistance(
                    currentLat,
                    currentLng,
                    point.Lat,
                    point.Lng
                );
                int flightTimeInMs = (int)(distance);
                if (flightTimeInMs < 500)
                    flightTimeInMs = 500;
              
                if (point.Wind > 12)
                {
                    Console.WriteLine($"[ALERT] {point.Name} is too windy! Aborting.");
                    
                    return;
                }

                Console.WriteLine($"[FLYING] Reached {point.Name}. Weather is good.");
                Console.WriteLine($"Expected flight time:{flightTimeInMs / 1000}s");
                
                Thread.Sleep(flightTimeInMs);
                //So next checkpoint starts from previous checkpoint's location'
                currentLat = point.Lat;
                currentLng = point.Lng;
            }

            Console.WriteLine("Thread Drone: Delivery Complete!");
        }
    }
}
