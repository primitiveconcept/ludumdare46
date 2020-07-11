namespace HackThePlanet
{
	using System.Collections.Generic;
	using System.Text;


	public class SqlCommand
	{
		protected List<string> statements;


		#region Constructors
		public SqlCommand()
		{
			this.statements = new List<string>();
		}
		#endregion


		public enum SortOrder
		{
			Ascending,
			Descending
		}


		#region Properties
		/// <summary>
		/// Last command in the command line string.
		/// </summary>
		public string LastStatement
		{
			get
			{
				if (this.statements.Count > 0)
					return this.statements[this.statements.Count - 1];
				return null;
			}
		}


		/// <summary>
		/// Collection of commands the BashCommand contains.
		/// </summary>
		public List<string> Statements
		{
			get { return this.statements; }
		}
		#endregion


		#region Operators
		/// <summary>
		/// Implicit cast of SqlCommand to string object.
		/// </summary>
		/// <param name="sqlCommand">SqlCommand being converted to string.</param>
		/// <returns>Entire commandline string represented by SqlCommand.</returns>
		public static implicit operator string(SqlCommand sqlCommand)
		{
			return sqlCommand.ToString();
		}
		#endregion


		public SqlCommand And(string field)
		{
			this.statements.Add(" AND " + field);
			return this;
		}


		public SqlCommand ContainsText(string pattern)
		{
			return IsLike(pattern);
		}


		public SqlCommand DeleteFrom(string table)
		{
			string statement = "DELETE FROM " + table;
			this.statements.Add(statement);
			return this;
		}


		public SqlCommand From(string table)
		{
			this.statements.Add(" FROM " + table);
			return this;
		}


		/// <summary>
		/// Inserts new rows into an existing table.
		/// </summary>
		/// <param name="table">Table to insert values into.</param>
		/// <param name="columns">Columns list.</param>
		/// <returns></returns>
		public SqlCommand InsertInto(string table, params string[] columns)
		{
			string statement = "INSERT INTO " + table;
			if (columns.Length > 0)
				statement += " ( " + string.Join(", ", columns) + " )";
			this.statements.Add(statement);
			return this;
		}


		public SqlCommand IsBetween(decimal value1, decimal value2)
		{
			this.statements.Add(" BETWEEN " + value1 + " AND " + value2);
			return this;
		}


		public SqlCommand IsEqualTo(string value)
		{
			this.statements.Add(" = '" + value + "'");
			return this;
		}


		public SqlCommand IsEqualTo(decimal value)
		{
			this.statements.Add(" = " + value);
			return this;
		}


		public SqlCommand IsEqualToAll(SqlCommand subquery)
		{
			this.statements.Add(" = ALL (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsEqualToAny(SqlCommand subquery)
		{
			this.statements.Add(" = ANY (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsIn(params decimal[] values)
		{
			StringBuilder statement = new StringBuilder(" IN ( ");
			for (int i = 0; i < values.Length; i++)
			{
				statement.Append(values[i]);
				if (i < values.Length - 1)
					statement.Append(", ");
			}
			statement.Append(" )");
			this.statements.Add(statement.ToString());
			return this;
		}


		public SqlCommand IsIn(params string[] values)
		{
			StringBuilder statement = new StringBuilder(" IN ( ");
			for (int i = 0; i < values.Length; i++)
			{
				statement.Append("'" + values[i] + "'");
				if (i < values.Length - 1)
					statement.Append(", ");
			}
			statement.Append(" )");
			this.statements.Add(statement.ToString());
			return this;
		}


		public SqlCommand IsLessThan(decimal value)
		{
			this.statements.Add(" < " + value);
			return this;
		}


		public SqlCommand IsLessThanAll(SqlCommand subquery)
		{
			this.statements.Add(" < ALL (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsLessThanAny(SqlCommand subquery)
		{
			this.statements.Add(" < ANY (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsLessThanOrEqualTo(decimal value)
		{
			this.statements.Add(" <= " + value);
			return this;
		}


		public SqlCommand IsLessThanOrEqualToAll(SqlCommand subquery)
		{
			this.statements.Add(" <= ALL (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsLessThanOrEqualToAny(SqlCommand subquery)
		{
			this.statements.Add(" <= ANY (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsLike(string pattern)
		{
			this.statements.Add(" LIKE '" + pattern + "'");
			return this;
		}


		public SqlCommand IsMoreThan(decimal value)
		{
			this.statements.Add(" > " + value);
			return this;
		}


		public SqlCommand IsMoreThanAll(SqlCommand subquery)
		{
			this.statements.Add(" > ALL (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsMoreThanAny(SqlCommand subquery)
		{
			this.statements.Add(" > ANY (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsMoreThanOrEqualTo(decimal value)
		{
			this.statements.Add(" >= " + value);
			return this;
		}


		public SqlCommand IsMoreThanOrEqualToAll(SqlCommand subquery)
		{
			this.statements.Add(" >= ALL (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsMoreThanOrEqualToAny(SqlCommand subquery)
		{
			this.statements.Add(" >= ANY (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsNotEqualTo(string value)
		{
			this.statements.Add(" != '" + value + "'");
			return this;
		}


		public SqlCommand IsNotEqualTo(decimal value)
		{
			this.statements.Add(" != " + value);
			return this;
		}


		public SqlCommand IsNotEqualToAll(SqlCommand subquery)
		{
			this.statements.Add(" != ALL (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsNotEqualToAny(SqlCommand subquery)
		{
			this.statements.Add(" != ANY (" + subquery.ToSubquery() + ")");
			return this;
		}


		public SqlCommand IsNotNull()
		{
			this.statements.Add(" IS NOT NULL");
			return this;
		}


		public SqlCommand IsNull()
		{
			this.statements.Add(" IS NULL");
			return this;
		}


		public SqlCommand Join(string table)
		{
			this.statements.Add(" JOIN " + table);
			return this;
		}


		public SqlCommand On(string field)
		{
			this.statements.Add(" ON " + field);
			return this;
		}


		public SqlCommand Or(string field)
		{
			this.statements.Add(" OR " + field);
			return this;
		}


		public SqlCommand OrderBy(
			SortOrder order = SortOrder.Ascending,
			params string[] fields)
		{
			string statement = MultiParameteredStatement("ORDER BY", fields);
			switch (order)
			{
				default:
				case SortOrder.Ascending:
					statement += " ASC";
					break;
				case SortOrder.Descending:
					statement += " DESC";
					break;
			}
			this.statements.Add(statement);
			return this;
		}


		public SqlCommand Select(params string[] fields)
		{
			string statement = MultiParameteredStatement("SELECT", fields);
			this.statements.Add(statement);
			return this;
		}


		public SqlCommand SelectAll()
		{
			this.statements.Add("SELECT *");
			return this;
		}


		public SqlCommand SelectAllDistinct()
		{
			this.statements.Add("SELECT DISTINCT *");
			return this;
		}


		public SqlCommand SelectDistinct(params string[] fields)
		{
			string statement = MultiParameteredStatement("SELECT DISTINCT", fields);
			this.statements.Add(statement);
			return this;
		}


		public SqlCommand Set(Dictionary<string, object> values)
		{
			StringBuilder statement = new StringBuilder(" SET ");
			int numValues = values.Count;
			int i = 0;
			foreach (KeyValuePair<string, object> entry in values)
			{
				string field = entry.Key;
				object value = entry.Value;
				if (IsNumber(value))
					statement.Append(field + "=" + value.ToString());
				else if (value == null)
					statement.Append(field + "=NULL");
				else
					statement.Append(field + "='" + value.ToString() + "'");

				if (i < numValues - 1)
					statement.Append(", ");
				i++;
			}
			this.statements.Add(statement.ToString());
			return this;
		}


		/// <summary>
		/// Output string representation of full SQL statement.
		/// </summary>
		/// <returns>Entire SQL statement string.</returns>
		public override string ToString()
		{
			StringBuilder finalStatement = new StringBuilder();
			foreach (string statement in this.statements)
			{
				finalStatement.Append(statement);
			}

			return finalStatement.ToString();
		}


		/// <summary>
		/// Updates columns of existing rows in the named table with new values.
		/// </summary>
		/// <param name="table">Table to update.</param>
		public SqlCommand Update(string table)
		{
			this.statements.Add("UPDATE " + table);
			return this;
		}


		/// <summary>
		/// Column values to be inserted/updated.
		/// </summary>
		/// <param name="values">Values to insert/update.</param>
		public SqlCommand Values(params object[] values)
		{
			StringBuilder statement = new StringBuilder(" VALUES (");
			int numValues = values.Length;
			for (int i = 0; i < numValues; i++)
			{
				object value = values[i];
				if (IsNumber(value))
					statement.Append(value.ToString());
				else if (value == null)
					statement.Append("NULL");
				else
					statement.Append("'" + value.ToString() + "'");

				if (i < numValues - 1)
					statement.Append(", ");
			}
			statement.Append(" )");
			this.statements.Add(statement.ToString());
			return this;
		}


		public SqlCommand Where(string field)
		{
			this.statements.Add(" WHERE " + field);
			return this;
		}


		private bool IsNumber(object value)
		{
			if (value is double
				|| value is long
				|| value is int
				|| value is decimal
				|| value is float)
				return true;
			return false;
		}


		private bool LastStatementEndsWith(params string[] matchingText)
		{
			if (string.IsNullOrEmpty(this.LastStatement))
				return false;

			foreach (string text in matchingText)
			{
				if (this.LastStatement.EndsWith(text.Trim()))
					return true;
			}

			return false;
		}


		private bool LastStatementStartsWith(params string[] matchingText)
		{
			if (string.IsNullOrEmpty(this.LastStatement))
				return false;

			foreach (string text in matchingText)
			{
				if (this.LastStatement.StartsWith(text.Trim()))
					return true;
			}
			
			return false;
		}


		private string MultiParameteredStatement(string command, params string[] parameters)
		{
			return command + " " + string.Join(",", parameters);
		}


		private string ToSubquery()
		{
			return this.statements
				.ToString()
				.TrimEnd(';');
		}
	}
}