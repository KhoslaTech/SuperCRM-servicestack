﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuperCRM.Infrastructure;
using System.Runtime.Serialization;
using ASPSecurityKit;
using ASPSecurityKit.Authorization;
using Newtonsoft.Json;

namespace SuperCRM.Models
{
	public enum PermissionKind
	{
		General,
		Instance,
		Dual
	}

	public class FirewallIpRange : IFirewallIpRange
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string IpFrom { get; set; }

		public string IpTo { get; set; }

		public static List<IFirewallIpRange> RangeForWholeOfInternet()
		{
			return new List<IFirewallIpRange>
			{
				new FirewallIpRange{ IpFrom = "0.0.0.0", IpTo = "255.255.255.255", Id = Guid.NewGuid(), Name = "Whole of IPv4" },
				new FirewallIpRange{ IpFrom = "::", IpTo = "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ffff", Id = Guid.NewGuid(), Name = "Whole of IPv6" },
			};
		}

		public static List<IFirewallIpRange> RangeForLocalhost()
		{
			return new List<IFirewallIpRange>
			{
				new FirewallIpRange{ IpFrom = "127.0.0.1", IpTo = "127.0.0.1", Id = Guid.NewGuid(), Name = "Localhost of IPv4" },
				new FirewallIpRange{ IpFrom = "::1", IpTo = "::1", Id = Guid.NewGuid(), Name = "Localhost of IPv6" },
			};
		}
	}

	public class AppUser
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Username { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool Suspended { get; set; }

		public string ParentName { get; set; }
		public Guid? ParentId { get; set; }

		[JsonIgnore]
		public string Password { get; set; }

	}

	/// <summary>
	/// This class is to save bandwidth and memory space. We could have used the real entity – UserPermit – but we don't need to load id and userId while querying for Permissions for a specific user via stored procedure.
	/// This again demonstrates the flexibility ASPSecurityKit gives you.
	/// </summary>
	public class Permit : IPermit<Guid>
	{
		public string PermissionCode { get; set; }
		public Guid? EntityId { get; set; }

		object IPermit.EntityId
		{
			get => this.EntityId;
			set => this.EntityId = (Guid?)value;
		}
	}

	public class PagedResult<T>
	{
		public IList<T> Records { get; }
		public int StartIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; }

		public PagedResult(IList<T> records, int startIndex, int pageSize, int totalCount)
		{
			this.Records = records ?? new List<T>();
			this.StartIndex = startIndex;
			this.PageSize = pageSize;
			this.TotalCount = totalCount;
		}
	}

}