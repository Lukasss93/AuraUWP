using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRT.Common
{
    public interface IIncrementalSource<T>
    {
        Task<IEnumerable<T>> GetPagedItems(int pageIndex, int pageSize, string param);
    }
}
