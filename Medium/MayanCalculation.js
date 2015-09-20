/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

var inputs = readline().split(' ');
var l = parseInt(inputs[0]);
var h = parseInt(inputs[1]);
var row = [];
var number = [];

for (var i = 0; i < h; i++)
{
	row[i] = readline();
}
//writes the given number
function writeArray(array)
{
	for (var i = 0; i < array.length; i++)
	{
		printErr(array[i]);
	}
}
//Assign array variables
function setNumber()
{
	for (var i = 0; i < 20; i++)
	{
		number[i] = "";
	}
//Put the given number in array to form 0 to 19
	for (var i = 0; i < h; i++)
	{
		for (var j = 0; j < 20; j++)
		{
			number[j] += row[i].substring(j * l, (j + 1) * l);
			if (i != h - 1)
			{
				number[j] += "\n";
			}
		}
	}
}

function arraySearch(array, value)
{
	for (var i = 0; i < array.length; i++)
		if (array[i] === value)
		{
			return i;
		}
	return false;
}

function log20(val)
{
	return Math.log(val) / Math.log(20);
}

function writeMayanAnswer(result)
{
	var mayanAnswer = "";
	var startingBase = Math.floor(log20(result));
	if (result != 0)
	{
		printErr("Number: " + result + " | Log answer:" + log20(result) + " | startingbase : " + startingBase);

		for (var i = startingBase; i >= 0; i--)
		{
			var modulo = result % Math.pow(20, i);
			if (modulo == result || modulo == 0)
			{
				var numberOfTimes = result / Math.pow(20, i);
				mayanAnswer += number[numberOfTimes];
			}
			else
			{
				var tempResult = result - modulo;
				var numberOfTimes = tempResult / Math.pow(20, i);
				mayanAnswer += number[numberOfTimes];
			}
			result = result - Math.pow(20, i) * numberOfTimes;
			mayanAnswer += "\n";
		}
	}
	else
	{
		mayanAnswer+= number[0];
	}
	printErr("Mayan answer:\n" + mayanAnswer);
	return mayanAnswer;
}

var trim = (function ()
{
	"use strict";

	function escapeRegex(string)
	{
		return string.replace(/[\[\](){}?*+\^$\\.|\-]/g, "\\$&");
	}

	return function trim(str, characters, flags)
	{
		flags = flags || "g";
		if (typeof str !== "string" || typeof characters !== "string" || typeof flags !== "string")
		{
			throw new TypeError("argument must be string");
		}

		if (!/^[gi]*$/.test(flags))
		{
			throw new TypeError("Invalid flags supplied '" + flags.match(new RegExp("[^gi]*")) + "'");
		}

		characters = escapeRegex(characters);

		return str.replace(new RegExp("^[" + characters + "]+|[" + characters + "]+$", flags), '');
	};
}());

function readMayanNumber(mayanNumber)
{
	var startingBase = mayanNumber.split("\n").length / h - 1;
	var result = 0;
	var mayanArray = [];

	for (var i = 0; i < 20; i++)
	{
		mayanArray[i] = "";
	}
	printErr("STarting base: " + startingBase);
	for (var i = startingBase; i >= 0; i--)
	{
		for (var j = 0; j < h; j++)
		{
			mayanArray[i] += mayanNumber.substring(j * (l + 1), (j + 1) * (l + 1));
		}
		mayanNumber = mayanNumber.replace(mayanArray[i], "");
		if ((mayanArray[i].match(/\n/g) || []).length == 4)
		{
			mayanArray[i] = trim(mayanArray[i], "\n ");
		}
	}

	//printErr("Mayan array:\n" + mayanArray[1]);
	for (var i = startingBase; i >= 0; i--)
	{
		printErr("Mayannumber calculus:\n"+mayanArray[i]);
		var nb = arraySearch(number, mayanArray[i]);
		printErr("Calculations:" + nb + " * " + Math.pow(20, i));
		result += nb * Math.pow(20, i);
	}
	return result;
}

var operators = {
	'+': function (a, b)
	{
		return a + b
	},
	'-': function (a, b)
	{
		return a - b
	},
	'*': function (a, b)
	{
		return a * b
	},
	'/': function (a, b)
	{
		return a / b;
	}
}

writeArray(row);
setNumber();
//writeArray(number);

var keys = Object.keys(number);

var s1 = parseInt(readline());

var mayanNumber1 = "";
for (var i = 0; i < s1; i++)
{
	mayanNumber1 += readline();
	if (i != s1 - 1)
	{
		mayanNumber1 += "\n";
	}
}

var s2 = parseInt(readline());
var mayanNumber2 = "";
for (var i = 0; i < s2; i++)
{
	mayanNumber2 += readline();
	if (i != s2 - 1)
	{
		mayanNumber2 += "\n";
	}
}
var operation = readline();
var number1 = readMayanNumber(mayanNumber1);
var number2 = readMayanNumber(mayanNumber2);
var result = operators[operation](number1, number2);
printErr("Number1:" + number1 + "\n Mayan number1:\n" + mayanNumber1 + "\n | Number2: " + number2 + "\n Mayan number2:\n" + mayanNumber2 + " \n | Operator :" + operation + " | Result:" + result);

print(writeMayanAnswer(result));