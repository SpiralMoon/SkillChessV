using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Service;

namespace Assets.Model.Impl
{
    public interface ISocketPage
    {
        void SetSocketEvents(NetworkManager networkManager);

        void Invoke(Action action);
    }
}
