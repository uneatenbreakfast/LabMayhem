using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabMayhem
{
    class Scientist : Person
    {
        public Scientist(string imgsrc)  : base(imgsrc)
        {
            characterType = CharacterType.SCIENTIST;
        }
    }
}
