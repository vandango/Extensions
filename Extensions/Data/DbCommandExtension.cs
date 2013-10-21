using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Data
{
	public static class DbCommandExtension
	{
		/// <summary>
		/// Adds the parameter.
		/// </summary>
		/// <param name="cmd">The CMD.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static DbParameter AddParameter(this DbCommand cmd, string parameterName, object value)
		{
			DbParameter param = cmd.CreateParameter();
			param.ParameterName = parameterName;
			if(value == null)
			{
				param.Value = DBNull.Value;
			}
			else
			{
				param.Value = value;
			}
			cmd.Parameters.Add(param);

			return param;
		}

		/// <summary>
		/// Adds the parameter.
		/// </summary>
		/// <param name="cmd">The CMD.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="value">The value.</param>
		/// <param name="dbType">Type of the db.</param>
		/// <returns></returns>
		public static DbParameter AddParameter(this DbCommand cmd, string parameterName, object value, DbType dbType)
		{
			DbParameter param = cmd.CreateParameter();
			param.DbType = dbType;
			param.ParameterName = parameterName;
			if(value == null)
			{
				param.Value = DBNull.Value;
			}
			else
			{
				param.Value = value;
			}
			cmd.Parameters.Add(param);

			return param;
		}

		/// <summary>
		/// Adds the parameter.
		/// </summary>
		/// <param name="cmd">The CMD.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="value">The value.</param>
		/// <param name="dbType">Type of the db.</param>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		public static DbParameter AddParameter(this DbCommand cmd, string parameterName, object value, DbType dbType, int size)
		{
			return AddParameter(cmd, parameterName, value, dbType, size, ParameterDirection.Input);
		}

		/// <summary>
		/// Adds the parameter.
		/// </summary>
		/// <param name="cmd">The CMD.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="value">The value.</param>
		/// <param name="dbType">Type of the db.</param>
		/// <param name="size">The size.</param>
		/// <param name="direction">The direction.</param>
		/// <returns></returns>
		public static DbParameter AddParameter(this DbCommand cmd, string parameterName, object value, DbType dbType, int size, ParameterDirection direction)
		{
			DbParameter param = cmd.CreateParameter();
			param.DbType = dbType;
			param.Direction = direction;
			param.ParameterName = parameterName;
			param.Size = size;
			if(value == null)
			{
				param.Value = DBNull.Value;
			}
			else
			{
				param.Value = value;
			}
			cmd.Parameters.Add(param);

			return param;
		}
	}
}
