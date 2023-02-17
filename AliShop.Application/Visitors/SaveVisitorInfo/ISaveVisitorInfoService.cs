using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Application.Visitors.SaveVisitorInfo
{
    public interface ISaveVisitorInfoService
    {
        void Execute(RequestSaveVisitorInfoDto request);
    }
}
