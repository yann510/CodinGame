/**
 * Don't let the machines win. You are humanity's last hope...
 **/

var width = parseInt(readline()); // the number of cells on the X axis
var height = parseInt(readline()); // the number of cells on the Y axis

printErr("Width:"+width + " | height:"+height);
for (var i = 0; i < height; i++)
{
	var line = readline(); // width characters, each either 0 or .
	printErr(line);
}

print('0 0 1 0 0 1'); // Three coordinates: a node, its right neighbor, its bottom neighbor