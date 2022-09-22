using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs
{
   public enum SequenceType
    {
        MODLING_SID, HISTORY_HID,WIP_SID
    }

    public enum TransType
    {
        Create, Update, Delete, SignIn, SignOut,Hold,UnHold,HoldRelease,MoveOut,UserSign,Close
    }
}
