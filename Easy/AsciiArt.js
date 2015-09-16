/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

var l = parseInt(readline());
var h = parseInt(readline());
var t = readline().toUpperCase();
var text = "";
var row = [];
printErr(l);
printErr(h);
for (var i = 0; i < h; i++)
{
	row[i] = readline();
	printErr("start row: " +row[i]);
}


for (var i = 0; i < h; i++)
{
	for (var k = 0; k < t.length; k++)
	{
		var asciiCode = t[k].charCodeAt(0);
		if (asciiCode < 65 || asciiCode > 90)
		{
			asciiCode = 91;
		}
		asciiCode = (asciiCode - 65) * l;
		printErr(t[k]);
		printErr("ascii code:" + asciiCode);
		for (var j = 0; j < l; j++)
		{
			text += row[i][j + asciiCode];
		}
	}
	text += "\n";
}
printErr(text);

// Write an action using print()
// To debug: printErr('Debug messages...');

print(text);