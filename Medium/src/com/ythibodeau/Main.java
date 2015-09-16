import java.util.*;
import java.io.*;
import java.math.*;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player {

    public static void main(String args[]) {
        Scanner in = new Scanner(System.in);
        int nbFloors = in.nextInt(); // number of floors
        int width = in.nextInt(); // width of the area
        int nbRounds = in.nextInt(); // maximum number of rounds
        int exitFloor = in.nextInt(); // floor on which the exit is found
        int exitPos = in.nextInt(); // getPosition() of the exit on its floor
        int nbTotalClones = in.nextInt(); // number of generated clones
        int nbAdditionalElevators = in.nextInt(); // ignore (always zero)
        int nbElevators = in.nextInt(); // number of elevators
        for (int i = 0; i < nbElevators; i++) {
            int elevatorFloor = in.nextInt(); // floor on which this elevator is found
            int elevatorPos = in.nextInt(); // position of the elevator on its floor
        }
        String requiredDirection = "NONE";
        // game loop
        while (true) {
            int cloneFloor = in.nextInt(); // floor of the leading clone
            int clonePos = in.nextInt(); // position of the leading clone on its floor
            String direction = in.next(); // direction of the leading clone: LEFT or RIGHT
            System.err.println(String.format("Droid floor %d | position %d | direction %s | required direction %s", cloneFloor, clonePos, direction, requiredDirection));
            Droid leadingDroid = new Droid(cloneFloor, clonePos, direction);

            if (leadingDroid.getFloor() == -1 && leadingDroid.getPosition() == -1 && leadingDroid.getDirection() != null)
            {
                System.out.println("WAIT");
            }
            else if (requiredDirection != "NONE" && leadingDroid.getDirection() != requiredDirection)
            {
                System.out.println("BLOCK");
            }
            else if (leadingDroid.getPosition() == width - 1)
            {
                requiredDirection = "LEFT";
                System.out.println("BLOCK");
            }
            else if (leadingDroid.getPosition() == 0)
            {
                requiredDirection = "RIGHT";
                System.out.println("BLOCK");
            }
            else
            {
                System.out.println("WAIT");
            }

            System.out.println("WAIT"); // action: WAIT or BLOCK
        }
    }
}

class Droid {
    int floor;
    int position;
    String direction;

    public Droid(int floor, int position, String direction) {
        this.floor = floor;
        this.position = position;
        this.direction = direction;
    }

    public int getFloor() {
        return floor;
    }

    public int getPosition() {
        return position;
    }

    public String getDirection() {
        return direction;
    }
}