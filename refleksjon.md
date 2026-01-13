The plan is to use Spectral Console as an initial selection tool.

As im working with "https://www.geonames.org/" to fetch raw data from weather stations,
im hoping to figure out how to make use of the data.

I might be too ambitious in my approach, but i hope to be able to do some of the ideas i have.

I planned to randomize the drone checkpoints, so the program would not static for each run.

I also wondered about the delat between checkpoints and if i should condition it on the distance (longitude and latitude)
between each checkpoint. Or if i should just use a random number between 0 and 100/arbirary consistent number.


CONTROL TOWER:
-----------------------------------------------------------------------------------
Fetch data from "https://www.geonames.org/"
convert data to json format
loop through json file and fetch data from each station
apply decision logic to determine if drone should fly to next checkpoint
-----------------------------------------------------------------------------------



 