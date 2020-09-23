using System;
namespace Application_DataRepository.DapperDataRepository.Attributes
{
    public class AllowNullAttribute:Attribute
    {
        #region var
        private bool _allowNull
        #endregion
        #region prop
        public bool allowNull => _allowNull;
        #endregion
        #region ctor

        public AllowNullAttribute(bool? allowNull)
        {
            this._allowNull = allowNull??false;
        }
        #endregion
    }
}
