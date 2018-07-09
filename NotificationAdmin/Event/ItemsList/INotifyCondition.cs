using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationAdmin.Event.ItemsList
{
    public interface INotifyCondition
    {
        void ChangeValueItem(String Identifier,String value,String  conector);

        void SelectedItem(String Identifier, Boolean isCheked);
    }

}
