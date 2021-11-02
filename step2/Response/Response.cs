using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;

namespace SuperCRM.Response
{
	[DataContract]
	public class FieldError
	{
		[DataMember]
		public string ErrorCode { get; set; }

		[DataMember]
		public string FieldName { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public Dictionary<string, string> Meta { get; set; }
	}

	[DataContract]
	public class ResponseError
	{
		public ResponseError(string errorCode) : this(errorCode, string.Empty)
		{
		}

		public ResponseError(string errorCode, string message)
		{
			this.ErrorCode = errorCode;
			this.Message = message;
			this.Errors = new List<FieldError>();
		}

		[DataMember]
		public string ErrorCode { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public List<FieldError> Errors { get; set; }
	}

	[DataContract]
	public class BaseResponse
	{
		[DataMember]
		public bool Succeeded => ErrorDetail == null;

		[DataMember]
		public ResponseError ErrorDetail { get; set; }

		public static BaseResponse Error(string code)
		{
			return new BaseResponse { ErrorDetail = new ResponseError(code) };
		}

		public static BaseResponse Error(string code, string message)
		{
			return new BaseResponse { ErrorDetail = new ResponseError(code, message) };
		}
	}

	[DataContract]
	public class BaseRecordResponse<T> : BaseResponse
	{
		[DataMember]
		public T Record { get; set; }
	}

	[DataContract]
	public class BaseListResponse<T> : BaseResponse
	{
		[DataMember]
		public IList<T> Records { get; set; }

		private long? totalCount;
		[DataMember]
		public long? TotalCount
		{
			get =>
				totalCount == null && Records != null
					? Records.Count
					: totalCount;
			set => totalCount = value;
		}
	}

	[DataContract]
	public class BaseIdResponse : BaseResponse
	{
		[DataMember]
		public Guid Id { get; set; }
	}

}