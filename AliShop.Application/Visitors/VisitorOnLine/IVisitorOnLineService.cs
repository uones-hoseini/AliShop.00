using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Application.Visitors.VisitorOnLine
{
    public interface IVisitorOnLineService
    {
        void ConnectUser(string ClientId);
        void DisConnectUser(string ClientId);
        int GetCount();
    }
}
