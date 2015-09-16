/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

var mESSAGE = readline();

var ABC = {
	toAscii: function (bin)
	{
		return bin.replace(/\s*[01]{8}\s*/g, function (bin)
		{
			return String.fromCharCode(parseInt(bin, 2))
		})
	},
	toBinary: function (str, spaceSeparatedOctets)
	{
		return str.replace(/[\s\S]/g, function (str)
		{
			str = ABC.zeroPad(str.charCodeAt().toString(2));
			str = str.substr(1);
			return !1 == spaceSeparatedOctets ? str : str + ""
		})
	},
	zeroPad: function (num)
	{
		return "00000000".slice(String(num).length) + num
	}
};
var binaryMessage = ABC.toBinary(mESSAGE);
printErr(binaryMessage);
var answer = "";

for (var i = 0; i < binaryMessage.length; i++)
{
	printErr("I entered the loop");
	var consecutive = 0;
	var previousNumber = -1;
	for (var j = i; j < binaryMessage.length; j++)
	{
		printErr("letter to check:" + binaryMessage[j])
		printErr("j = "+j + "message Lenght = " + binaryMessage.length)

		if (previousNumber == binaryMessage[j])
		{
			consecutive++;
		}
		else if (previousNumber != -1)
		{
			printErr("i break");
			if (j == binaryMessage.length)
			{
				var consecutiveToTheEnd = true;
				printErr("consecutive to the end")
			}

			i = j - 1;

			break;
		}
		if (previousNumber == -1)
		{
			previousNumber = binaryMessage[j];
			printErr("I set previous: " + previousNumber);
		}
		if (j == binaryMessage.length - 1)
		{
			var consecutiveToTheEnd = true;
		}
	}

	printErr("consecutive :" + consecutive);
	printErr("prevousNumber: " + previousNumber + "\n");
	answer += previousNumber == 0 ? "00 " : "0 ";
	for (var k = 0; k <= consecutive; k++)
	{
		printErr("I write 0");
		answer += "0";
	}
	if (consecutiveToTheEnd)
		break;
	answer += " ";
}
printErr("answer:"+answer);
// Write an action using print()
// To debug: printErr('Debug messages...');

print(answer.trim());