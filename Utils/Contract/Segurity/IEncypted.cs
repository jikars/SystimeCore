using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Segurity;

namespace Utils.Contract.Segurity
{
    public interface  IEncyptedSimetric
    {
        String EncryptString(String TextPlain, String Key, String IV);
        String DecryptString(String TextPlain, String Key, String IV);
    }
}
