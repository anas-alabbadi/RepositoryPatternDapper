using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Application_DataRepository.DapperDataRepository.Attributes
{
    public partial class SqlDataTypeAttribute:Attribute
    {

        #region Var
        private DbType _SqlDbType;
        #endregion
        #region Ctor
        public SqlDataTypeAttribute(DbType _SqlDbType)
        {
            this._SqlDbType = _SqlDbType;
        }
        #endregion
        #region Prop
        public DbType SqlDbType
        {
            get { return _SqlDbType; }
        }
        #endregion
        #region Func

        #endregion
    }
}
