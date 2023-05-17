using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBTest.models;

namespace CBTest.services.impl
{
    public class DataBindingServiceImpl : DataBindingService
    {
        public List<DataBinding> CreateDataBinding(List<ParticipantInfo> participantInfos)
        {
            List<DataBinding> dataBindings = new List<DataBinding>();
            
            foreach(ParticipantInfo participant in participantInfos)
            {
                foreach(Account account in participant.Accounts)
                {
                    DataBinding data = new DataBinding();
                    data.Name = participant.Name;
                    data.Address = participant.Address;
                    data.RegionName = participant.RegionName;
                    data.BIC = participant.Bic;
                    data.ParticipantStatus = participant.Status;
                    data.AccountNumber = account.Number;
                    data.Type = account.Type;
                    data.CBRBIC = account.CbrBic;
                    data.AccountStatus = account.Status;
                    dataBindings.Add(data);
                }
            }

            return dataBindings;
        }
    }
}
