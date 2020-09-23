using System;
using System.Collections.Generic;
using System.Text;

namespace Application_DataRepository.DapperDataRepository.Attributes
{
  public partial  class ParameterAttribute:Attribute
    {
        #region Var
        private string ParameterName;
        #endregion
        #region Ctor
        public ParameterAttribute(string ParameterName)
        {
            this.ParameterName = ParameterName;
        }
        #endregion
        #region Prop
        public string value
        {
            get { return this.ParameterName; }
        }
        #endregion
        #region Func

        #endregion
    }
}
