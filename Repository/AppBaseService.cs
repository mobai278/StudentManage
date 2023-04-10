using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Data;

namespace Repository
{
    public abstract class AppBaseService<T> : BaseService<T> where T : class
    {
    }
}
