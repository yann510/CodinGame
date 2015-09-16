/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/


var n = parseInt(readline()); // Number of elements which make up the association table.
var q = parseInt(readline()); // Number Q of file names to be analyzed.
var mimeArray = [];
for (var i = 0; i < n; i++)
{
	var inputs = readline().split(' ');
	var eXT = inputs[0]; // file extension
	var mT = inputs[1]; // MIME type.
	mimeArray[eXT.toLowerCase()] = mT;
}

for (var i = 0; i < q; i++)
{
	var fNAME = readline(); // One file name per line.
	//ext = trim(fNAME, ".");
	var ext = fNAME.split(".");
	ext = ext[ext.length - 1].toLowerCase();
	var found = false;
	if (fNAME.indexOf('.') > -1)
	{
		printErr("extension : " + ext);
		var index = mimeArray.indexOf(ext);

		//printErr("mimeArray:" + mimeArray[j][0])
		if (mimeArray[ext])
		{
			found = true;
			print(mimeArray[ext]);
		}

	}
	if (!found)
	{
		print('UNKNOWN');
	}
	//printErr(fNAME);
}

