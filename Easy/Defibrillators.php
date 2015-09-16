<?php
/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

fscanf(STDIN, "%s", $LON);
fscanf(STDIN, "%s", $LAT);
fscanf(STDIN, "%d", $N);

$closestDistance = 100000;

for ($i = 0; $i < $N; $i++)
{
	$DEFIB = stream_get_line(STDIN, 256, "\n");
	$defibInfo = explode(";", $DEFIB);
	$defibrillator = new Defibrillator($defibInfo[0], $defibInfo[1], $defibInfo[2], $defibInfo[3], $defibInfo[4], $defibInfo[5]);


	$x = ($defibrillator->longitude - $LON) * cos(($LAT + $defibrillator->latitude) / 2);
	$y = $defibrillator->latitude - $LAT;
	$distance = (float)sqrt((pow(2, $x) + pow(2, $y))) * 6371;
	if ($defibrillator->number < 37)
	{
		error_log(var_export("Number : $defibrillator->number name: $defibrillator->name : $distance", true));
	}
	if ($distance <= $closestDistance)
	{
		$closestDistance = $distance;
		$closestDefibrillator = $defibrillator;
	}
	//error_log(var_export($DEFIB, true));
}
//error_log(var_export($defibrillatorsArray, true));


error_log(var_export($closestDefibrillator, true));
echo($closestDefibrillator->name);

class Defibrillator
{
	var $number;
	var $name;
	var $adress;
	var $contactPhone;
	var $longitude;
	var $latitude;

	/**
	 * Defibrillator constructor.
	 *
	 * @param $number
	 * @param $name
	 * @param $adress
	 * @param $contactPhone
	 * @param $longitude
	 * @param $latitude
	 */
	public function __construct($number, $name, $adress, $contactPhone, $longitude, $latitude)
	{
		$this->number = $number;
		$this->name = $name;
		$this->adress = $adress;
		$this->contactPhone = $contactPhone;
		$this->longitude = str_replace(",", ".", $longitude);
		$this->latitude = str_replace(",", ".", $latitude);
	}
}