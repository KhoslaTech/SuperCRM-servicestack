using System.Runtime.Serialization;

namespace SuperCRM.Request
{
	[DataContract]
	public class PageRequestBase
	{
		private int pageSize = 10;
		[DataMember]
		public int PageSize
		{
			get => pageSize;
			set
			{
				if (value >= 1)
					pageSize = value;
			}
		}

		private int pageNumber = 1;
		[DataMember]
		public int PageNumber
		{
			get => pageNumber;
			set
			{
				if (value >= 1)
					pageNumber = value;
			}
		}

		public int StartIndex => (PageNumber - 1) * PageSize;
	}
}