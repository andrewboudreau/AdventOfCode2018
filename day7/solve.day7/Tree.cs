using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace solve.day7
{
	public class Tree<TNode> : IEnumerable<TreeNode<TNode>>
	{
		IEnumerable<TreeNode<TNode>> Nodes { get; } = new Tree<TNode>();

		public IEnumerator<TreeNode<TNode>> GetEnumerator()
		{
			foreach (var node in Nodes)
			{
				yield return node;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
