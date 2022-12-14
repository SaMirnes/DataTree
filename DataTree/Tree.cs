using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTree;

class Tree
{
    public Node rootNode;
    public List<List<Node>> nodes;
    private int _nodeWidth = 200;
    private int _nodeHeight = 100;


    public int nodeWidth{
        get { return _nodeWidth; }
        set
        { 
            _nodeWidth = value;
            foreach (List<Node> list in nodes)
            {
                foreach (Node node in list)
                {
                    node.Location = new Point(node.Location.X + ((node.Width - _nodeWidth) / 2) , node.Location.Y);
                    node.Width = _nodeWidth;
                }
            }
        }
    }
    
    public int nodeHeight
    {
        get { return _nodeHeight; }
        set
        {
            _nodeHeight = value;
            foreach (List<Node> list in nodes)
            {
                foreach (Node node in list)
                {

                    node.Location = new Point(node.Location.X, node.Location.Y + ((node.Height - _nodeHeight)/2) );
                    node.Height = _nodeHeight;
                }
            }
        }
    }
    public Color nodeColor = Color.Blue;
    public int spacingHorizontalSibling;
    public int spacingHorizontalCousins;
    public int spacingVertical;

    public Tree(int rootXPos, int rootYPos)
    {
        spacingHorizontalSibling = nodeWidth/3;
        spacingHorizontalCousins = 2 * nodeWidth / 3;
        spacingVertical = nodeHeight * 2 - nodeHeight / 3;

        rootNode = new(0);
        nodes = new();
        nodes.Add(new List<Node>());
        nodes[0].Add(rootNode);

        rootNode.BackColor = nodeColor;
        rootNode.BorderStyle = BorderStyle.None;
        rootNode.Multiline = true;
        rootNode.Size = new Size(nodeWidth, nodeHeight);
        rootNode.Location = new Point(rootXPos - nodeWidth / 2, rootYPos);
        rootNode.TextAlign = HorizontalAlignment.Center;
    }

    public Node AddChild(Node parent)
    {
        (int, int) horizontalBoundries = parent.NewChildHorizontalSpace();

        if(!NoCollision(horizontalBoundries.Item1, horizontalBoundries.Item2, parent.heightLevel + 1))
        {
            do
            {
                IncreaseSpaceBetweenNodes(parent.heightLevel);
                horizontalBoundries = parent.NewChildHorizontalSpace();

            } while (!NoCollision(horizontalBoundries.Item1, horizontalBoundries.Item2, parent.heightLevel + 1));
        }
        
        Node child = parent.AddChild(spacingHorizontalSibling, spacingVertical, parent.heightLevel + 1);
       
        if (nodes.Count < (parent.heightLevel + 1))
        {
            nodes[parent.heightLevel + 1].Add(child);
        }
        else
        {
            nodes.Add(new List<Node>());
            nodes[parent.heightLevel + 1].Add(child);
        }

        return child;
    }

    /**
     * Increase space between all the nodes of the given height level
     * 
     */
    private void IncreaseSpaceBetweenNodes(int heightLevel)
    {
        // The children must also be adjussted after the space between nodes at given height level is changed
        throw new NotImplementedException();
    }

    /**
     * Return true if area (startX -> endX) at given heightLevel is clear of nodes
     * returns false if there is a node in the space startX -> endX
     */
    private bool NoCollision(int startX, int endX, int heightLevel)
    {
        if (nodes.Count < heightLevel)
        {
            return true;
        }

        foreach( Node n in nodes[heightLevel-1])
        {
            if (n.Location.X + n.Width >= startX)
            {
                if(n.Location.X <= endX)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
