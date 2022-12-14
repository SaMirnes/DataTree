using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTree;

class Node : TextBox
{
    public List<Node> children = new();
    public int heightLevel;
    public (int startX, int endX) reservedSpace; 

    public Node(int heightLevel)
    {
        this.heightLevel = heightLevel;
    }

    /*public Node AddChild(int posX, int posY, int level)
    {
        Node newChild = new(level);
        newChild.Location = new System.Drawing.Point(posX, posY);
        newChild.BackColor = this.BackColor;

        children.Add(newChild);
        return newChild;
    }*/

    public Node AddChild(int spacingBetweenNodes, int spacingVertical, int heightLevel)
    {
        Node newChild = new(heightLevel);
        newChild.BackColor = this.BackColor;
        newChild.Width = this.Width;
        newChild.Height = this.Height;
        newChild.Multiline = true;
        newChild.BorderStyle = BorderStyle.None;
        newChild.TextAlign = HorizontalAlignment.Center;
        children.Add(newChild);
        int posX = 0;

        if (children.Count % 2 == 1) // 1,3,5,7,9 etc...
        {
            posX = this.Location.X - (this.Width + spacingBetweenNodes) * ((children.Count - 1) / 2);
        }
        else // 2,4,6,8 etc...
        {
            posX = this.Location.X + this.Width/2 - spacingBetweenNodes / 2 -this.Width - (this.Width - spacingBetweenNodes) * ((children.Count / 2) - 1);
        }

        foreach (Node node in children)
        {
            node.Location = new System.Drawing.Point(posX, this.Location.Y + spacingVertical);
            posX += this.Width + spacingBetweenNodes;
        }

        return newChild;
    }

    /** Returns the x-location of the left side of the leftmost child node
     *  and and x-location of the right side of the right most child
     *  if a new child were to be added
     */
    public (int,int) NewChildHorizontalSpace()
    {
        if(children.Count == 0)
        {
            return this.reservedSpace;
        }
        else
        {
            return (children[0].reservedSpace.startX, children.Last().reservedSpace.endX);
        }
    }


}

