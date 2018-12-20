using System.Collections.Generic;

namespace solve.day7
{
	public class TreeNode<TNode>
	{
		private readonly List<TreeNode<TNode>> children = new List<TreeNode<TNode>>();

		public TreeNode(TNode value)
		{
			Value = value;
		}

		public TNode Value { get; }

		public TreeNode<TNode> Parent { get; private set; }

		public IEnumerable<TreeNode<TNode>> Children
		{
			get
			{
				return children;
			}
		}

		public void AddChild(TreeNode<TNode> child)
		{
			child.Parent = this;
			children.Add(child);
		}
	}
}
