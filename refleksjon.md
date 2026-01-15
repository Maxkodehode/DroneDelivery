The plan is to use Spectral Console as an initial selection tool.

As I'm working with "https://www.geonames.org/" to fetch raw data from weather stations,
 I'm hoping to figure out how to make use of the data.

I might be too ambitious in my approach, but I hope to be able to do some of the ideas I have.

I planned to randomize the drone checkpoints, so the program would not be static for each run.

I also wondered about the delay between checkpoints and if I should condition it on the distance (longitude and latitude)
between each checkpoint. Or if I should just use a random number between 0 and 100/arbirary consistent number.

I got an AI to do the equation for me so I can have a dynamic delay variable depending on the distance between checkpoints.


CONTROL TOWER:
-----------------------------------------------------------------------------------
Fetch data from "https://www.geonames.org/"
convert data to JSON format
loop through JSON file and fetch data from each station
apply decision logic to determine if the drone should/can fly to the next checkpoint.
-----------------------------------------------------------------------------------

When commenting out the t.join in Thread-Drone,
the program will tell me all drones are finished while the drone is still flying to the next checkpoint.



 