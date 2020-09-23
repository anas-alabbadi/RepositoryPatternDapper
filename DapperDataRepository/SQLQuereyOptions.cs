using System;
using System.Collections.Generic;
using System.Text;

namespace Application_DataRepository.DapperDataRepository
{
   public partial class SQLQuereyOptions
    {
        public enum _SQLQuereyOptions
        {
            Insert,
            Update,
            Delete,
            GetAll,
            GetByID,
            GetByKeyFilter,
        }
    }
}
