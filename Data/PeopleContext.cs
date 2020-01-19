using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WWI.Models;
using WWI.Log4NetExtension;
using WWI.Data;

namespace WWI.Data
{
	public class PeopleContext
	{
		public int TotalRecords { get; set; }

		public PeopleContext() { }
	}
}