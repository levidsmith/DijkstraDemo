//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {
    public List<Text> textOutput;
    public Dropdown dropdownData;
    public Dropdown dropdownStartingNode;

    void Start() {
        
    }

    void Update() {
        
    }

    public void clearOutput(int iIndex) {
        textOutput[iIndex].text = "";
    }

    public void clearAllOutput() {
        foreach (Text t in textOutput) {
            t.text = "";
        }
    }

    public void addOutput(string str, string strColor, int iIndex) {
        textOutput[iIndex].text += "<color=" + strColor + ">" + str + "</color>\n";

    }

    public int getSelectedInput() {
        return  dropdownData.value;
    }

    public void clearStartingNodeOptions() {
        dropdownStartingNode.ClearOptions();

    }

    public void addStartingNodeOptions(List<Node> listInOptions) {
        List<string> listOptions = new List<string>();
        foreach (Node node in listInOptions) {
            listOptions.Add(node.strName);

        }
        dropdownStartingNode.AddOptions(listOptions);

    }

    public void freezeStartingNodeOption() {
        //dropdownStartingNode.enabled = false;
        dropdownStartingNode.interactable = false;
        
    }

    public void enableStartingNodeOption() {
        //dropdownStartingNode.enabled = true;
        dropdownStartingNode.interactable = true;
    }

}