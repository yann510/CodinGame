/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

var surfaceN = parseInt(readline()); // the number of points used to draw the surface of Mars.
var landArray = [];
var previousLandingSiteX = 0;
var previousLandingSiteY = 0;
var landingSiteStartX;
var landingSiteStartY;
var landingSiteEndX;
var landingSiteEndY;
for (var i = 0; i < surfaceN; i++)
{
	var inputs = readline().split(' ');
	var landX = parseInt(inputs[0]); // X coordinate of a surface point. (0 to 6999)
	var landY = parseInt(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
	if (previousLandingSiteY == landY)
	{
		landingSiteEndX = landX;
		landingSiteEndY = landY;
		landingSiteStartX = previousLandingSiteX;
		landingSiteStartY = previousLandingSiteY;
	}
	previousLandingSiteX = landX;
	previousLandingSiteY = landY;
	landArray[i] = [landX, landY];
}

// game loop
while (true)
{
	var inputs = readline().split(' ');
	var x = parseInt(inputs[0]);
	var y = parseInt(inputs[1]);
	var hSpeed = parseInt(inputs[2]); // the horizontal speed (in m/s), can be negative.
	var vSpeed = parseInt(inputs[3]); // the vertical speed (in m/s), can be negative.
	var fuel = parseInt(inputs[4]); // the quantity of remaining fuel in liters.
	var rotate = parseInt(inputs[5]); // the rotation angle in degrees (-90 to 90).
	var power = parseInt(inputs[6]); // the thrust power (0 to 4).

	//We can start going down in straight line
	if (x >= landingSiteStartX && x <= landingSiteEndX && hSpeed < 20)
	{
		var straight = true;
		slowHorizontal = false;
		printErr("We can now go down straight");
	}
	else
	{
		var direction = x < landingSiteStartX ? "LEFT" : "RIGHT";
		printErr("Direction:" + direction);
		var straight = false;
		var slowHorizontal = (Math.abs(x - landingSiteStartX) || Math.abs(x - landingSiteEndX)) < 700 ? true : false;
		printErr("Slow horizontal:" +slowHorizontal + "test :" + Math.abs(x - landingSiteEndX));
	}

	printErr("yShip : " + y + " | landingShipY : " + landingSiteStartY);
	if (y > landingSiteStartY)
	{
		if (Math.abs(vSpeed) < 40 && straight)
		{
			printErr("I do get here");
			power = 0;
		}

		else
		{
			power = 4;
		}
	}


	if (slowHorizontal)
	{
		if (hSpeed != 0)
		{
			if (direction == "RIGHT")
			{
				rotate -= 15;
				if (rotate < -25)
				{
					rotate = -25;
				}
			}
			else
			{
				rotate += 15;
				if (rotate > 25)
				{
					rotate = 25;
				}
			}
		}
	}
	//Sets the right rotation angle
	else if (straight && rotate != 0)
	{
		if (rotate < 0)
		{
			rotate = Math.abs(rotate);
		}
		else
		{
			rotate = rotate * -1;
		}
	}
	else if (!straight)
	{
		if (direction == "RIGHT")
		{
			rotate += 15;
			if (rotate > 25)
			{
				rotate = 25;
			}
		}
		else
		{
			rotate -= 15;
			if (rotate < -25)
			{
				rotate = -25;
			}
		}
	}
	//WE want to go straight
	else
	{
		if (rotate < 0)
		{
			rotate += 15
			if (rotate > 0)
			{
				rotate = 0;
			}
		}
		else
		{
			rotate -= 15;
			if (rotate < 0)
			{
				rotate = 0;
			}
		}
	}

	// Write an action using print()
	// To debug: printErr('Debug messages...');

	print(rotate + " " + power);
}

function setTreshold(value, treshold)
{
	if (treshold > 0)
	{
		if (value > treshold)
		{
			return treshold;
		}
	}
	else
	{
		if (value < treshold)
		{
			return treshold;
		}
	}
	return value;
}