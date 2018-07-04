using System.Collections.Generic;

namespace FolderSearch.Interfaces
{
	public interface INode
	{
		INode Parent { get; set; }
		string Name { get; set; }
		IEnumerable<INode> Children { get; set; }
	}
}