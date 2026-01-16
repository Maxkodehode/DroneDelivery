# C# Intermediate Assignment: Drone Dash

This is a drone delivery simulator where I test different ways of handling multiple tasks at once in C#.
I used a real weather API to make the simulation dynamic.

## Requirements
* **Framework**: The program is built using .NET 10.
* **Internet Connection**: Needed to fetch weather data from the GeoNames API.
* **NuGet Packages**:
    * `Spectre.Console`
    * `System.Text.Json`

## How to run the program
You can run this project in several ways:

### 1. Using the "Play" button (IDE)
If you are using **JetBrains Rider** or **Visual Studio**, open the project and press the **Run/Play** button.

### 2. Using the Terminal (IDE or OS)
1. Open your terminal (Built-in IDE terminal or Command Prompt/Bash).
2. Navigate to the project folder.
3. Type the following command and press Enter:
```bash
dotnet run
```
### 3. Navigation
--------------------------------------------------------
When the program starts, you will see a menu. Use the arrow keys to navigate and press Enter to select:
1. Thread Drone: Runs the ThreadDrone.cs logic.
2. Task Drone: Runs the TaskDrone.cs logic.
3. Async Drone: Runs the AsyncDrone.cs logic.
4. Exit: Closes the program.
--------------------------------------------------------
Use the **arrow keys** to navigate the menu and press **Enter** to select the menu option.


## What I implemented
* **Thread Drone**: Uses manual threads. I implemented t.Join() because I discovered that without it, the main program finishes and reports
* "All drones returned" while the drones are actually still flying in the background.

* **Task Drone**: Uses `TaskCompletionSource` to signal when a drone is done.
* **Failure Simulation**: I added a "Motor Failure" error in Task Drone. If the wind is over 17 m/s, the drone will crash and report an error.

* **Async Drone**: The modern way using `async` and `await`.

* * **Safety**: If the wind is between 12 and 17 m/s, the drone is "safe" but skips the checkpoint to avoid risk.
* **Control Tower**: This connects to GeoNames and gets real coordinates.
* I used a math formula (Haversine) to calculate the real distance between checkpoints so the flight time is dynamic.

## Troubleshooting
If the API is down or the internet is slow, I added a **5-second timeout**.
The program will catch the error and use a "fallback" route so it doesn't crash.






