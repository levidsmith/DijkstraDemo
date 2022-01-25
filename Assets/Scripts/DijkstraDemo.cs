//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DijkstraDemo : MonoBehaviour {
    public const int INFINITY = 999;
    public List<TextAsset> textInput;
    List<Node> listNodes;
    List<Edge> listEdges;

    Dictionary<Node, int> hashDistances;
    Dictionary<Node, Node> hashPathSource;
    List<Node> listSelected;

    public DisplayManager displaymanager;


    void Start() {
        restart();
        
    }

    void Update() {
        
    }

    public void restart() {
        listNodes = new List<Node>();
        listEdges = new List<Edge>();
        loadData();
        startDemo();

    }

    private void loadData() {
        string strData = textInput[displaymanager.getSelectedInput()].text;
        foreach (string strLine in strData.Split('\n')) {
            Debug.Log("line: " + strLine);
            string[] strValues = strLine.Split(',');

            Node node1 = getOrCreateNode(strValues[0]);

            Node node2 = getOrCreateNode(strValues[1]);

            Edge edge = new Edge(node1, node2, int.Parse(strValues[2]));
            listEdges.Add(edge);
        }

        listNodes.Sort();
        displaymanager.clearStartingNodeOptions();
        displaymanager.addStartingNodeOptions(listNodes);
    }

    private void startDemo() {
        displaymanager.clearAllOutput();
        displaymanager.enableStartingNodeOption();
        
        hashDistances = new Dictionary<Node, int>();
        hashPathSource = new Dictionary<Node, Node>();
        listSelected = new List<Node>();

        displaymanager.addOutput("Nodes", "red", 0);
        foreach (Node n in listNodes) {
            displaymanager.addOutput(n + "", "red", 0);
            hashDistances[n] = INFINITY;
        }

        //set start node to zero distance
        //hashDistances[getOrCreateNode("A")] = 0;
        //hashDistances[listNodes[0]] = 0;
        //hashDistances[getOrCreateNode(displaymanager.dropdownStartingNode.options[displaymanager.dropdownStartingNode.value].text)] = 0;

        displaymanager.addOutput("Edges", "blue", 0);
        foreach (Edge e in listEdges) {
            displaymanager.addOutput(e.nodes[0] + " - " + e.nodes[1] + ": " + e.iDistance, "blue", 0);
        }

        displaymanager.addOutput("Path", "white", 2);


    }

    public void doNextStep() {
        if (listSelected.Count == 0) {
            hashDistances[getOrCreateNode(displaymanager.dropdownStartingNode.options[displaymanager.dropdownStartingNode.value].text)] = 0;
            displaymanager.freezeStartingNodeOption();
        }


        if (listSelected.Count < listNodes.Count) {
            displaymanager.clearOutput(1);

            displaymanager.addOutput("Distances", "red", 1);
            Node nodeLowest = null;
            foreach (Node n in listNodes) {
                if (!listSelected.Contains(n)) {
                    displaymanager.addOutput(n + ": " + getStringValue(hashDistances[n]), "red", 1);
                    if (nodeLowest == null) {
                        nodeLowest = n;
                    } else if (hashDistances[n] < hashDistances[nodeLowest]) {
                        nodeLowest = n;
                    }
                }
            }

            displaymanager.addOutput("Pick " + nodeLowest + "(" + hashDistances[nodeLowest] + ")", "blue", 1);
            listSelected.Add(nodeLowest);
            listSelected.Sort();
            if (hashPathSource.ContainsKey(nodeLowest)) {
                displaymanager.addOutput(hashPathSource[nodeLowest] + " - " + nodeLowest, "white", 2);
            }

            displaymanager.addOutput("Selected", "orange", 1);
            foreach (Node n in listSelected) {
                displaymanager.addOutput("" + n, "orange", 1);
            }

            displaymanager.addOutput("Adjacent (not in selected)", "green", 1);
            foreach (Edge e in listEdges) {
                Node nodeAdjacent = null;
                int iDistance = -1;

                if (e.nodes[0] == nodeLowest) {
                    nodeAdjacent = e.nodes[1];
                    iDistance = e.iDistance;
                } else if (e.nodes[1] == nodeLowest) {
                    nodeAdjacent = e.nodes[0];
                    iDistance = e.iDistance;
                }

                if (nodeAdjacent != null && !listSelected.Contains(nodeAdjacent)) {
                    displaymanager.addOutput(nodeAdjacent + " - " + iDistance, "green", 1);

                    if (hashDistances[nodeLowest] + iDistance < hashDistances[nodeAdjacent]) {
                        hashDistances[nodeAdjacent] = hashDistances[nodeLowest] + iDistance;
                        hashPathSource[nodeAdjacent] = nodeLowest;
                    }

                }
            }

            displaymanager.addOutput("Updated Distances", "yellow", 1);
            foreach (Node n in listNodes) {
                if (!listSelected.Contains(n)) {

                    displaymanager.addOutput(n + ": " + getStringValue(hashDistances[n]), "yellow", 1);
                }
            }

            if (listSelected.Count == listNodes.Count) {
                displaymanager.addOutput("Done!", "cyan", 1);
            }


        }

    }

    private Node getOrCreateNode(string strInName) {
        Node node = null;
        foreach(Node n in listNodes) {
            if (n.strName == strInName) {
                node = n;
            }
        }

        if (node == null) {
            node = new Node(strInName);
            listNodes.Add(node);
        }

        return node;
    }

    private string getStringValue(int iValue) {
        if (iValue >= INFINITY) {
            return "INF";
        }  else {
            return string.Format("{0}", iValue);
        }
    }
}