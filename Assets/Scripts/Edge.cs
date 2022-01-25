//2021 Levi D. Smith
public class Edge {
    
    public Node[] nodes;
    public int iDistance;

    public Edge(Node node1, Node node2, int iInDistance) {
        nodes = new Node[2];
        nodes[0] = node1;
        nodes[1] = node2;

        iDistance = iInDistance;


    }
}