using System;
using System.Collections.Generic;
using System.Text;

namespace Application_DataRepository.DapperDataRepository.Attributes
{
    public partial class ProcedureAttribute : Attribute
    {
        #region var 
        private string _name;
        #endregion
        #region prop
        public string name { get { return _name; } set { _name = value; } }
        #endregion
        #region MyRegion
        public ProcedureAttribute(string name)
        {
            _name = name;
        }
        #endregion
    }
}
