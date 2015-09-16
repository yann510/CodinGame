<?php
/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

fscanf(STDIN, "%d %d %d %d %d %d %d %d", $nbFloors, // number of floors
	$width, // width of the area
	$nbRounds, // maximum number of rounds
	$exitFloor, // floor on which the exit is found
	$exitPos, // position of the exit on its floor
	$nbTotalClones, // number of generated clones
	$nbAdditionalElevators, // ignore (always zero)
	$nbElevators // number of elevators
);
$elevetorPositions = [];
for ($i = 0; $i < $nbElevators; $i++)
{
	fscanf(STDIN, "%d %d", $elevatorFloor, // floor on which this elevator is found
		$elevatorPos // position of the elevator on its floor
	);
	$elevetorPositions[$elevatorFloor] = $elevatorPos;
}
$elevetorPositions[$exitFloor] = $exitPos;
$requiredDirection = null;
// game loop
while (TRUE)
{
	fscanf(STDIN, "%d %d %s", $cloneFloor, // floor of the leading clone
		$clonePos, // position of the leading clone on its floor
		$direction // direction of the leading clone: LEFT or RIGHT
	);
	$leadingDroid = new Droid($cloneFloor, $clonePos, $direction);
	if ($elevetorPositions[$leadingDroid->floor] < $leadingDroid->position)
	{
		$requiredDirection = "LEFT";
	}
	else if ($elevetorPositions[$leadingDroid->floor] > $leadingDroid->position)
	{
		$requiredDirection = "RIGHT";
	}


	error_log(var_export("droid floor {$leadingDroid->floor} | droid position: {$leadingDroid->position} |
	droid direction {$leadingDroid->direction} | required direction {$requiredDirection} | elevator position
	{$elevetorPositions[$leadingDroid->floor]}", true));


	if ($leadingDroid->floor == -1 && $leadingDroid->position == -1 && !isset($leadingDroid->direction))
	{
		echo("WAIT\n");
	}
	else if (isset($requiredDirection) && $leadingDroid->direction != $requiredDirection)
	{
		echo("BLOCK\n");
	}
	else if ($leadingDroid->position == $width - 1)
	{
		echo("BLOCK\n");
	}
	else if ($leadingDroid->position == 0)
	{
		echo("BLOCK\n");
	}
	else
	{
		echo("WAIT\n");
	}
	// Write an action using echo(). DON'T FORGET THE TRAILING \n
	// To debug (equivalent to var_dump): error_log(var_export($var, true));

}

class Droid
{
	var $floor;
	var $position;
	var $direction;

	/**
	 * Droid constructor.
	 *
	 * @param $floor
	 * @param $position
	 * @param $direction
	 */
	public function __construct($floor, $position, $direction)
	{
		$this->floor = $floor;
		$this->position = $position;
		$this->direction = $direction;
	}

	public function GetRequiredDirection()
	{

	}
}