import java.util.*;
import java.util.concurrent.ExecutionException;

/**
 * Don't let the machines win. You are humanity's last hope...
 **/
class Player {

    public static void main(String args[]) {
        Scanner in = new Scanner(System.in);
        int width = in.nextInt(); // the number of cells on the X axis
        in.nextLine();
        int height = in.nextInt(); // the number of cells on the Y axis
        in.nextLine();

        System.err.println(String.format("width :%d | height: %d", width, height));
        Hashtable<int[], Integer> map = new Hashtable<int[], Integer>();
        List<Node> unvisitedNodes = new ArrayList<Node>();

        for (int i = 0; i < height; i++) {
            String line = in.nextLine(); // width characters, each either 0 or .
            System.err.println(line);

            char[] cell = line.toCharArray();
            for (int j = 0; j < line.length(); j++) {
                int[] position = {i, j};

                Integer value = (cell[j] == '0') ? 1 : 0;
                map.put(position, value);
                System.err.println("cell value:" + cell[j] + " | Node value:" + value + " | cell position:"+position[0]+position[1]);

                if (value != 0) {
                    unvisitedNodes.add(new Node(position, value, false));
                }
            }
        }
        System.err.println("map count:" + map.size());

        for (int[] key: map.keySet()){
            System.err.println(key[0] + " " + key[1]);
        }

        for (Node node : unvisitedNodes) {
            if (node.getValue() != 0) {
                System.out.println(Player.printAnswer(node.getPosition(), node.getRightNeighbor(map), node.getBottomNeighbor(map)));
            }
        }
    }

    private static String printAnswer(int[] position, int[] positionRightNeighbor, int[] positionBottomNeighbor) {
        return position[0] + " " + position[1] + " " + positionRightNeighbor[0] + " " + positionRightNeighbor[1] + " " + positionBottomNeighbor[0] + " " + positionBottomNeighbor[1];
    }
}

class Node {
    private int[] position;
    private Integer value;
    private boolean visited;

    public Node(int[] position, Integer empty, boolean visited) {
        this.position = position;
        this.value = empty;
        this.visited = visited;
    }

    public int[] getRightNeighbor(Hashtable<int[], Integer> map) {

        int[] rightNeighborPosition = {this.position[0]+1, this.position[1]};
        Integer value = map.get(rightNeighborPosition);
        System.err.println(String.format("Node position: %d %d | rightNeighbor: %d %d | is contained in array: %s",this.position[0],this.position[1], rightNeighborPosition[0], rightNeighborPosition[1], map.containsKey(rightNeighborPosition)));
        if (value != null && value == 1) {
            return rightNeighborPosition;
        }
        int[] emptyNeighbor = {-1, -1};
        return emptyNeighbor;

    }

    public int[] getBottomNeighbor(Hashtable<int[], Integer> map) {
        int[] bottomNeighborPosition = {this.position[0], this.position[1]+1};
        Integer value = map.get(bottomNeighborPosition);
        System.err.println(String.format("Node position: %d %d | bottomNeighbor: %d %d",this.position[0],this.position[1], bottomNeighborPosition[0], bottomNeighborPosition[1]));

        if (value != null && value == 1) {
            return bottomNeighborPosition;
        }
        int[] emptyNeighbor = {-1, -1};
        return emptyNeighbor;
    }

    public int[] getPosition() {
        return position;
    }

    public void setPosition(int[] position) {
        this.position = position;
    }

    public Integer getValue() {
        return value;
    }

    public void setValue(Integer value) {
        this.value = value;
    }

    public boolean isVisited() {
        return visited;
    }

    public void setVisited(boolean visited) {
        this.visited = visited;
    }
}