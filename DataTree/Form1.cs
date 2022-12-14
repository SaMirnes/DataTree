using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTree
{
    public partial class Form1 : Form
    {
        Tree tree;
        int windowWidth = 1920;
        int windowHeight = 1080;
        Color nodeColor = Color.FromArgb(255, 0, 102, 204);
        Node selectedNode;

        public Form1()
        {
            this.Width = windowWidth;
            this.Height = windowHeight;

            tree = new(windowWidth/2, windowHeight/10);
            selectedNode = tree.rootNode;
            
            tree.rootNode.Click += new System.EventHandler(this.NodeClick);
            Controls.Add(tree.rootNode);

            InitializeComponent();
        }

        private void NodeClick(object sender, EventArgs e)
        {
            selectedNode = (Node)sender;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void NodeSelected(Node rootNode)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void addNodeButton_Click(object sender, EventArgs e)
        {
            Node newChild = tree.AddChild(selectedNode);
            newChild.Click += new System.EventHandler(this.NodeClick);
            Controls.Add(newChild);
        }

        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numUpDown = (NumericUpDown)sender;

            this.tree.nodeHeight = (int)numUpDown.Value;
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numUpDown = (NumericUpDown)sender;

            this.tree.nodeWidth = (int)numUpDown.Value;
        }
    }
}
