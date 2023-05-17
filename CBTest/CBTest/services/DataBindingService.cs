using CBTest.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBTest.services
{
    public interface DataBindingService
    {
        List<DataBinding> CreateDataBinding(List<ParticipantInfo> participantInfo);
    }
}
