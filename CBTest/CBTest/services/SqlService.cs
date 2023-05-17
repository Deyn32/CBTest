using CBTest.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBTest.services
{
    interface SqlService
    {
        List<ParticipantInfo> findAll();
        void SaveParticipantInfo(ParticipantInfo participantInfo);
        void SaveAccount(Account account);
    }
}
