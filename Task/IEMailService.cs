using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobControl.Task
{
    public interface IEMailService
    {
        void SendMessage(string emailAddresses, string subject, string message);
    }
}
