# Auto-generated code below aims at helping you parse
# the standard input according to the problem statement.
#!/bin/bash

read N
declare -a horseArray
for (( i=0; i<N; i++ )); do
    read Pi
	horseArray[$i]=$Pi
	current=$(($N - 1))
	if [ $i == $current ]
		then
			sortedArray=( $( printf "%s\n" "${horseArray[@]}" | sort -n ) )
			echo ${sortedArray[@]} >&2
			smallest=10000000
			for (( j=0; j<N; j++ )); do
				diff=$((sortedArray[$j]-sortedArray[$j+1]))
				diff=${diff#-}
				#echo ${diff#-} >&2
				#echo ${smallest#-} >&2
				current=$(($N - 1))
				echo "$diff" >&2
				if [ "$diff" -lt "$smallest" ] && [ $j \< $current ]
					then
						smallest=${diff#-}
				fi
				echo $smallest >&2
				if [ $j == $current ]
					then
						echo ${smallest#-}
				fi
			done
	fi
done

# Write an action using echo
# To debug: echo "Debug messages..." >&2

#echo "answer"