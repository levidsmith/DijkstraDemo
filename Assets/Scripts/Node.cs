//2021 Levi D. Smith

public class Node : System.IComparable {
    public string strName;

    public Node(string strInName) {
        strName = strInName;

    }

    public int CompareTo(object obj) {
        return string.Compare(strName, ((Node)obj).strName);
    }

    public override string ToString() {
        return strName;
    }
}