using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirsToTheThrone.model
{
    public class Fortification
    {
        public FortificationType type;
        public Double defenseBonus;

        private static Dictionary<FortificationType, Fortification> fortifications = new Dictionary<FortificationType,Fortification>();

        private Fortification()
        {
        }

        public static Fortification forType(FortificationType type)
        {
            Fortification fortification;
            if ( fortifications.TryGetValue(type, out fortification) ){
                return fortification;
            }
            fortification = createForType(type);
            fortifications.Add(type, fortification);
            return fortification;
        }

        public static Fortification createForType(FortificationType type)
        {
            Fortification fortification = new Fortification();
            fortification.type = type;
            fortification.defenseBonus = ((int)type) * 0.05 + 0.25;
            return fortification;
        }


    }
}
