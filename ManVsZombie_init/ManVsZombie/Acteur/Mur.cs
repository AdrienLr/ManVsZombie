using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acteur
{
    class Mur : Entite
    {
        private readonly static int vieBase = 10;
        private readonly static string nomBase = "Mur";

        public Mur()
        {
            Identifiant = 0;
            Nom = nomBase;
            VieActuelle = vieBase;
            VieMax = vieBase;
            IsVivant = true;
        }
        public Mur(string nom, int vie)
        {
            Nom = nom;
            VieActuelle = vie;
            VieMax = vie;
            IsVivant = true;
        }
    }
}
