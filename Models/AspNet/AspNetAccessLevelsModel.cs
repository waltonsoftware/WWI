using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WWI.Models
{
	public class AspNetAccessLevelsModel
	{
		public String Id { get; set; }

		public String Name { get; set; }

		public String Description { get; set; }

		public Int32 AccessLevel { get; set; }
	}
}