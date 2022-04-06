using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Dtos
{

	//Key Value Picture View Model with child
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

	public class KVPCategoryVM
    {
		public KVPCategoryVM()
		{
			Childs = new List<KVPCategoryVM>();
		}
		public Guid Key { get; set; }
		public string Value { get; set; }
		public MediaEntity? Image { get; set; }
		public List<KVPCategoryVM> Childs { get; set; }
		public Guid? ParentId { get; set; } = null;
		public string ParentTitle { get; set; } = null;
		public string LanguageId { get; set; } = "fa-IR";
		public CategoryForEnum CategoryFor { get; set; }
	}
}
