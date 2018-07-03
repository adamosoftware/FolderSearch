using System.Collections.Generic;

namespace FolderSearch.Interfaces
{
	public interface IHierarchy
	{
		string Name { get; }
		IEnumerable<IHierarchy> Children { get; set; }
	}
}