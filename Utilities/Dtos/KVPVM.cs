using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Dtos
{
	public class KVPVM
	{
		public KVPVM()
		{
			Childs = new List<KVPVM>();
		}
		public Guid Key { get; set; }
		public string Value { get; set; }
		public MediaEntity? Image { get; set; }
		public List<KVPVM> Childs { get; set; }
	}
}
