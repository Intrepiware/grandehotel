using System;
using System.Collections.Generic;
using System.Text;

namespace GrandeHotel.Lib.Data.Services
{

    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Generate();
    }
}
