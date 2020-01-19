using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WWI.Data
{
	public class QueryModel
	{
		public QueryModel()
		{
			Parameters = new List<SqlParameter>();
		}

		public QueryModel(string pSearch)
			: this()
		{
			search = pSearch;
		}

		public QueryModel(Order order)
			: this()
		{
			orders = new List<Order>();
			this.orders.Add(order);
		}

		public QueryModel(int pageSize, int offset)
			: this()
		{
			length = pageSize;
			start = offset;
		}

		// properties are not capital due to json mapping
		public int draw { get; set; }

		public int start { get; set; }

		public int length { get; set; }

		public List<Column> columns { get; set; }

		public string search { get; set; }

		public List<Order> orders { get; set; }

		public List<SqlParameter> Parameters { get; set; }
	}

	public class Column
	{
		public string data { get; set; }

		public string name { get; set; }

		public bool searchable { get; set; }

		public bool orderable { get; set; }

		public Search search { get; set; }
	}

	public class Search
	{
		public string value { get; set; }

		public string regex { get; set; }
	}

	public class Order
	{
		public Order() { }

		public Order(int col, string direction)
		{
			this.column = col;
			this.dir = direction;
		}

		public Order(int col)
		{
			this.column = col;
			this.dir = "asc";
		}

		public int column { get; set; }

		public string dir { get; set; }
	}

}