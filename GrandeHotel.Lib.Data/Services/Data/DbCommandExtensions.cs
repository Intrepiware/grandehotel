using System.Data.Common;

namespace GrandeHotel.Lib.Data.Services.Data
{
    public static class DbCommandExtensions
    {
        public static DbParameter CreateParameterWithValue(this DbCommand cmd, string paramName, object value)
        {
            DbParameter param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value;
            cmd.Parameters.Add(param);
            return param;
        }
    }
}
