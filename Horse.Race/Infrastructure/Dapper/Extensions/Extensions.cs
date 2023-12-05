using Dapper;
using System.Data;

namespace Infrastructure.Dapper.Extensions;

public static class DapperExtensions
{
    /// <summary>
    /// Used to pass a DataTable as a TableValuedParameter
    /// </summary>
    /// <typeparam name="T">type of enumerbale</typeparam>
    /// <param name="enumerable">The keys to pass</param>
    /// <returns>a custom query parameter</returns>
    public static SqlMapper.ICustomQueryParameter AsSingleTableValuedParameter<T>
        (this IEnumerable<T> ids, string columnName = "Id", string typeName = null, bool allowDuplicates = false)
    {
        columnName = string.IsNullOrEmpty(columnName) ? "Id" : columnName;

        DataTable idTable = new DataTable();
        idTable.Columns.Add(columnName, typeof(T));

        var wrapChar = "'";

        switch (Type.GetTypeCode(typeof(T)))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                wrapChar = string.Empty;
                break;
        }

        if (ids != null)
        {
            foreach (var id in ids)
            {
                if (allowDuplicates || idTable.Select(columnName + "=" + wrapChar + id + wrapChar).Length == 0)
                {
                    idTable.Rows.Add(id);
                }
            }
        }

        return idTable.AsTableValuedParameter(typeName);
    }
}