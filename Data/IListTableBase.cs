using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WWI.Data
{
	public class IListTableBase<T> : IList<T>
	{
		private readonly bool m_IsReadOnly = true;
		private List<T> m_items = new List<T>();
		public List<T> Items
		{
			get { return m_items; }
		}
		public int m_TotalRecords = 0;
		protected int m_current_page = 0;
		protected int m_page_size = 0;
		protected int m_page_offset = 0;

		protected string sql = string.Empty;
		protected DataSet ds = new DataSet();
		protected SqlDataAdapter da = null;
		protected DataTable dt = null;

		protected QueryModel m_ajaxParameters = null;

		public IListTableBase()
		{
			m_page_size = 10;
			m_page_offset = 0;
		}

		public IListTableBase(QueryModel queryModel)
			: this()
		{
			this.queryModel = queryModel;
		}

		public IListTableBase(int pageSize, int pageOffset)
			: this()
		{
			m_page_size = pageSize;
			m_page_offset = pageOffset;
		}


		public T this[int index]
		{
			get
			{
				return m_items[index];
			}
			set
			{
				m_items[index] = value;
			}
		}

		public int Count
		{
			get { return m_items.Count; }
		}

		public bool IsReadOnly
		{
			get { return m_IsReadOnly; }
		}

		public void Add(T item)
		{
			m_items.Add(item);
		}

		public void Clear()
		{
			m_items.Clear();
		}

		public bool Contains(T item)
		{
			return m_items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			m_items.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return m_items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_items.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return m_items.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			m_items.Insert(index, item);
		}

		public bool Remove(T item)
		{
			return m_items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			m_items.RemoveAt(index);
		}

		public int GoToPreviousPage()
		{
			if (CurrentPage > 1)
				CurrentPage--;
			return CurrentPage;
		}

		public int GoToNextPage()
		{
			if (CurrentPage < LastPage)
				CurrentPage++;
			return CurrentPage;
		}

		public int GoToFirstPage()
		{
			CurrentPage = 1;
			return CurrentPage;
		}

		public int GoToLastPage()
		{
			CurrentPage = LastPage;
			return CurrentPage;
		}

		public string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["WWI"].ConnectionString;
		}

		/// <summary>
		/// PageSize - set to -1 for entire table.
		/// </summary>
		//public int PageSize
		//{
		//	get
		//	{
		//		if (m_page_size == 0)
		//			throw new Exception("PageSize is 0!");
		//		return m_page_size;
		//	}
		//	set { m_page_size = value; }
		//}

		//public int PageOffset
		//{
		//	get
		//	{
		//		return m_page_offset;
		//	}
		//	set
		//	{
		//		m_page_offset = value;
		//	}
		//}

		//public string Filter { get; set; }

		public int TotalRecords
		{
			get { return m_TotalRecords; }
			set { m_TotalRecords = value; }
		}

		public int LastPage
		{
			get
			{
				int _ret = 0;
				if (m_TotalRecords > 0)
				{
					_ret = (int)Math.Truncate((decimal)TotalRecords / (decimal)PageSize);
				}
				return _ret;
			}
		}

		public int CurrentPage
		{
			get { return m_current_page; }
			set { m_current_page = value; }
		}

		public QueryModel queryModel
		{
			get { return m_ajaxParameters; }
			set { m_ajaxParameters = value; }
		}

		public int PageSize
		{
			get { return queryModel.length; }
			set { queryModel.length = value; }
		}

		public int PageOffset
		{
			get { return queryModel.start; }
			set { queryModel.start = value; }
		}

		public string Filter
		{
			get { return queryModel.search; }
		}

		public List<Order> Orders
		{
			get { return queryModel.orders; }
		}
	}
}
